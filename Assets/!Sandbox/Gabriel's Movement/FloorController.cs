using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace DefaultNamespace
{
    [Serializable]
    public struct TilemapByFloor
    {
        public FloorType type;
        public Tilemap tilemap;
    }

    public class FloorController : MonoBehaviour
    {
        [SerializeField]
        private FloorType currentFloor;

        [SerializeField]
        private TilemapByFloor[] floors;
        private Dictionary<FloorType, GameObject> _tilemapsByFloor;

        private void Start()
        {
            _tilemapsByFloor = new Dictionary<FloorType, GameObject>();
            for (int i = 0; i < floors.Length; i++)
            {
                var floorType = floors[i].type;
                var tilemap = floors[i].tilemap.gameObject;
                
                tilemap.SetActive(false);
                _tilemapsByFloor[floorType] = tilemap;
            }
            _tilemapsByFloor[currentFloor].SetActive(true);
        }

        public void SetFloor(FloorType floor)
        {
            _tilemapsByFloor[currentFloor].SetActive(false);
            _tilemapsByFloor[floor].SetActive(true);

            currentFloor = floor;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FloorController))]
    public class FloorControllerEditor : Editor
    {
        private SerializedProperty _currentFloor;
        private SerializedProperty _floors;

        private ReorderableList _list;

        private void OnEnable()
        {
            _currentFloor = serializedObject.FindProperty("currentFloor");
            _floors = serializedObject.FindProperty("floors");

            _list = new ReorderableList(serializedObject, _floors, true, true, true, true);
            _list.drawHeaderCallback = DrawHeader;
            _list.drawElementCallback = DrawListItems;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Floors");
        }
        
        private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = _list.serializedProperty.GetArrayElementAtIndex(index);
            float height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 50, height),
                element.FindPropertyRelative("type"),
                GUIContent.none);
            
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y, 150, height),
                element.FindPropertyRelative("tilemap"),
                GUIContent.none);
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_currentFloor);
            _list.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
