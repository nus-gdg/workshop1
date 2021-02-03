using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "FixedCameraLogic", menuName = "ScriptableObjects/CameraLogic/FixedCameraLogic", order = 1)]
public class FixedCameraLogic : CameraLogic
{
    public Vector3 Position;
    public float OrthographicSize = 0.0f;

    public override void OnPush(CameraController controller)
    {
        base.OnPush(controller);
        controller.CurrentProperties.TargetPosition = Position;
        controller.CurrentProperties.TargetOrthographicSize = OrthographicSize;
    }
}

[CustomEditor(typeof(FixedCameraLogic))]
public class FixedCamerLogicEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FixedCameraLogic fixedCameraLogic = (FixedCameraLogic)target;
        if (GUILayout.Button("Copy Main Camera"))
        {
            fixedCameraLogic.Position = Camera.main.transform.position;
            fixedCameraLogic.OrthographicSize = Camera.main.orthographicSize;
        }
    }
}
