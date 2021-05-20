using System.Collections.Generic;
using Project.Views.World;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Views.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CameraLogic defaultCameraLogic;

        [SerializeField]
        private CameraSettings settings;

        private new Camera camera;
        private Stack<CameraLogic> cameraLogicStack;

        private WorldView _view;

        private Vector3 velocity = Vector3.zero;
        private float zoomSpeed = 0.0f;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public float OrthographicSize
        {
            get => camera.orthographicSize;
            set => camera.orthographicSize = value;
        }

        public float Aspect
        {
            get => camera.aspect;
            set => camera.aspect = value;
        }

        public CameraSettings Settings
        {
            get => settings;
            set => settings = value;
        }

        void Awake()
        {
            camera = GetComponent<Camera>();
            Assert.IsNotNull(camera, "CameraController.Awake CameraController expects a camera component");

            cameraLogicStack = new Stack<CameraLogic>();
        }

        void Start()
        {
            PushCameraLogic(defaultCameraLogic);
        }

        public void Init(WorldView view)
        {
            _view = view;
        }

        void OnApplicationQuit()
        {
            cameraLogicStack = null;
            camera = null;
        }

        void LateUpdate()
        {
            cameraLogicStack.Peek().UpdateCamera(this, _view);
        }

        void FixedUpdate()
        {
            Settings.targetPosition.z = Position.z;
            Position = Vector3.SmoothDamp(Position, Settings.targetPosition, ref velocity, Settings.smoothTime);
            OrthographicSize = Mathf.SmoothDamp(OrthographicSize, Settings.targetOrthographicSize, ref zoomSpeed, Settings.zoomSmoothTime);

            Assert.IsTrue(Settings.targetOrthographicSize > 0, "CameraController.FixedUpdate Target Orthographic size is less than or equal to 0 Check default orthographic size settings");
        }

        /// <summary>
        /// Pushes a new camera logic
        /// </summary>
        /// <param name="cameraLogic">Camera Logic to push</param>
        public void PushCameraLogic(CameraLogic cameraLogic)
        {
            cameraLogicStack.Push(cameraLogic);
            cameraLogicStack.Peek().InitCamera(this, _view);
        }

        /// <summary>
        /// Pops camera logic off the stack. If you provide the camera logic, will do runtime check if cameralogic was indeed on top of stack
        /// </summary>
        /// <param name="cameraLogic">Camera Logic to check</param>
        public void PopCameraLogic(CameraLogic cameraLogic = null)
        {
            if (cameraLogicStack.Peek() == defaultCameraLogic)
            {
                return;
            }
            cameraLogicStack.Pop();
            cameraLogicStack.Peek().InitCamera(this, _view);
        }

        public Vector2 ClampWorldPositionInsideCamera2D(Vector2 worldPosition)
        {
            Assert.IsTrue(camera.orthographic, "CameraManager ClampWorldPositionInsideScreen2D expects camera to be orthographic");

            float halfHeight = OrthographicSize;
            float halfWidth = OrthographicSize * Aspect;

            Vector2 localPos = worldPosition - (Vector2)Position;
            localPos.y = Mathf.Clamp(localPos.y, -halfHeight, halfHeight);
            localPos.x = Mathf.Clamp(localPos.x, -halfWidth, halfWidth);

            return (Vector2)Position + localPos;
        }

        public Vector3 GetWorldMousePosition()
        {
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
