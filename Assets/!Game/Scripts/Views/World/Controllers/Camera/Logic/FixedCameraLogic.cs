using Project.Views.Controllers;
using Project.Views.World;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Views.Controllers
{
    [CreateAssetMenu(fileName = "FixedCameraLogic", menuName = "ScriptableObjects/CameraLogic/FixedCameraLogic", order = 1)]
    public class FixedCameraLogic : CameraLogic
    {
        [SerializeField]
        public Vector3 position;

        [SerializeField]
        public float orthographicSize = 0.0f;

        public override void InitCamera(CameraController camera, WorldView view)
        {
            camera.Settings.targetPosition = position;
            camera.Settings.targetOrthographicSize = orthographicSize;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FixedCameraLogic))]
public class FixedCamerLogicEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FixedCameraLogic fixedCameraLogic = (FixedCameraLogic)target;
        if (GUILayout.Button("Copy Main Camera"))
        {
            fixedCameraLogic.position = Camera.main.transform.position;
            fixedCameraLogic.orthographicSize = Camera.main.orthographicSize;
        }
    }
}
#endif
