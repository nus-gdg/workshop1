using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common.Logic
{
    public static class ScriptableObjectExtensions
    {
#if UNITY_EDITOR

        [MenuItem("CONTEXT/ScriptableObject/Fix")]
        public static void FixMissingScripts(MenuCommand command)
        {
            ((ScriptableObject)command.context).FixMissingScripts();
        }

        public static bool HasMissingScripts<T>(this T instance) where T : ScriptableObject
        {
            //Check for missing scripts
            var path = AssetDatabase.GetAssetPath(instance);
            var scripts = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

            foreach (var script in scripts)
            {
                if (script == null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Function used to remove a sub-asset that is missing the script reference
        /// </summary>
        /// <param name="instance">The main asset that holds the sub-asset</param>
        public static void FixMissingScripts<T>(this T instance) where T : ScriptableObject
        {
            if (!HasMissingScripts(instance))
            {
                return;
            }

            //Create a new instance of the object to delete
            ScriptableObject newInstance = ScriptableObject.CreateInstance<T>();

            //Copy the original content to the new instance
            EditorUtility.CopySerialized(instance, newInstance);
            newInstance.name = instance.name;

            string instancePath = AssetDatabase.GetAssetPath(instance);
            string clonePath = instancePath.Replace(".asset", "CLONE.asset");

            //Create the new asset on the project files
            AssetDatabase.CreateAsset(newInstance, clonePath);
            AssetDatabase.ImportAsset(clonePath);

            //Unhide sub-assets
            var subAssets = AssetDatabase.LoadAllAssetsAtPath(instancePath);
            HideFlags[] flags = new HideFlags[subAssets.Length];
            for (int i = 0; i < subAssets.Length; i++)
            {
                //Ignore the "corrupt" one
                if (subAssets[i] == null)
                    continue;

                //Store the previous hide flag
                flags[i] = subAssets[i].hideFlags;
                subAssets[i].hideFlags = HideFlags.None;
                EditorUtility.SetDirty(subAssets[i]);
            }

            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssets();

            //Reparent the subAssets to the new instance
            foreach (var subAsset in AssetDatabase.LoadAllAssetRepresentationsAtPath(instancePath))
            {
                //Ignore the "corrupt" one
                if (subAsset == null)
                    continue;

                //We need to remove the parent before setting a new one
                AssetDatabase.RemoveObjectFromAsset(subAsset);
                AssetDatabase.AddObjectToAsset(subAsset, newInstance);
            }

            //Import both assets back to unity
            AssetDatabase.ImportAsset(instancePath);
            AssetDatabase.ImportAsset(clonePath);

            //Reset sub-asset flags
            for (int i = 0; i < subAssets.Length; i++)
            {
                //Ignore the "corrupt" one
                if (subAssets[i] == null)
                    continue;

                subAssets[i].hideFlags = flags[i];
                EditorUtility.SetDirty(subAssets[i]);
            }

            EditorUtility.SetDirty(newInstance);
            AssetDatabase.SaveAssets();

            //Here's the magic. First, we need the system path of the assets
            string globalToDeletePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), instancePath);
            string globalClonePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), clonePath);

            //We need to delete the original file (the one with the missing script asset)
            //Rename the clone to the original file and finally
            //Delete the meta file from the clone since it no longer exists

            File.Delete(globalToDeletePath);
            File.Delete(globalClonePath + ".meta");
            File.Move(globalClonePath, globalToDeletePath);

            AssetDatabase.Refresh();
        }

#endif
    }
}
