using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public CameraLogic DefaultCameraLogic;

    [HideInInspector]
    public CameraControllerSettings CurrentSettings;
    public CameraControllerSettings DefaultSettings;

    private CameraControllerLogic logic;
    private Camera attachedCamera;
    private Vector3 velocity = Vector3.zero;
    private float zoomSpeed = 0.0f;
 
    /// <summary>
    /// Pushes a new camera logic
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to push</param>
    public void PushCameraLogic(CameraLogic cameraLogic)
    {
        logic.PushCameraLogic(cameraLogic, CurrentSettings);
    }

    /// <summary>
    /// Pops camera logic off the stack. If you provide the camera logic, will do runtime check if cameralogic was indeed on top of stack
    /// </summary>
    /// <param name="cameraLogic">Camera Logic to check</param>
    public void PopCameraLogic(CameraLogic cameraLogic = null)
    {
        logic.PopCameraLogic(cameraLogic, out CurrentSettings);
    }

    public Vector2 ClampWorldPositionInsideCamera2D(Vector2 position)
    {
        Camera camera = attachedCamera;
        Vector2 camPos = camera.transform.position;
        float aspect = camera.aspect;
        Assert.IsTrue(camera.orthographic, "CameraManager ClampWorldPositionInsideScreen2D expects camera to be orthographic");
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.orthographicSize * aspect;

        Vector2 localPos = position - camPos;
        localPos.y = Mathf.Clamp(localPos.y, -halfHeight, halfHeight);
        localPos.x = Mathf.Clamp(localPos.x, -halfWidth, halfWidth);

        return camPos + localPos;
    }

    void Awake()
    {
        attachedCamera = GetComponent<Camera>();
        Assert.IsNotNull(attachedCamera, "CameraController.Awake CameraController expects an attached camera");

        Assert.IsNull(Common.Game.Instance.World.Camera, "CameraController.Awake More than one registered Camera");
        Common.Game.Instance.World.Camera = this;

        logic = new CameraControllerLogic(this);
    }

    void Start()
    {
        CurrentSettings = DefaultSettings;
        PushCameraLogic(DefaultCameraLogic);
    }

    void OnApplicationQuit()
    {
        logic = null;
        Common.Game.Instance.World.Camera = null;
    }

    void LateUpdate()
    {
        logic.LateUpdate();
    }

    void FixedUpdate()
    {
        CurrentSettings.TargetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, CurrentSettings.TargetPosition, ref velocity, CurrentSettings.SmoothTime);
        attachedCamera.orthographicSize = Mathf.SmoothDamp(attachedCamera.orthographicSize, CurrentSettings.TargetOrthographicSize, ref zoomSpeed, CurrentSettings.ZoomSmoothTime);

        Assert.IsTrue(CurrentSettings.TargetOrthographicSize > 0, "CameraController.FixedUpdate Target Orthographic size is less than or equal to 0 Check default orthographic size settings");
    
    }

}
