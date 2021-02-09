// GENERATED AUTOMATICALLY FROM 'Assets/!Game/Runtime/Prefabs/Core/Resources/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Core.Managers
{
    public class @InputManager : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputManager()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""028fdfb6-f3bb-4b6c-832d-ed5e29fe1711"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""52ed9723-aa6a-4f20-9d09-33839cba434c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d7263500-9cc7-489b-8796-50c02bb76e50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""49eaa5bc-e02c-49ad-a031-6d4f8d9e65a7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action1"",
                    ""type"": ""Button"",
                    ""id"": ""6197d555-e608-4101-98e9-e6df23e7c05f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action2"",
                    ""type"": ""Button"",
                    ""id"": ""92e27aa4-5718-4341-a322-d43cb46f607a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectItem"",
                    ""type"": ""Value"",
                    ""id"": ""2b174aaf-b166-4926-8be6-b8d0ad550870"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""8e6d6d57-e117-487d-bc2a-4b0b2ec31301"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""ff3a0bef-da32-4520-9312-3bec78c56869"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8f2ab31f-47dd-4242-b42c-55aadcae4bf7"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold(duration=0.001,pressPoint=0.001)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""23fdbed0-b3e6-4f05-bdb8-44285b6dcbd6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""83db9bc9-6108-4eed-8fa0-1622c4792794"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92547901-2c2d-473e-a503-946650da41de"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1931ffc9-55fd-42cc-83c0-aa568479b1ce"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""dfbecf9e-3855-436b-a933-294024654f9c"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold(duration=0.001,pressPoint=0.001)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7f0b9a9c-8a3e-4dc7-af23-03b979b93bfc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5381179c-7b3a-43da-aeb7-e43ab49f00a0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e81387b5-c535-43f4-9f1e-4e45f388e083"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cf6cf9ef-69bd-4490-9263-46657e85d6fe"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9079144c-f675-497e-b679-bd14f52a7c94"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6dd2187-4327-4db2-9274-c274dc4bc9c2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beab01ae-7790-4ca2-90f0-d3217c571259"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b448fc8-139d-411d-8d36-e6d639af5af0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ec22827-61d1-462f-acd0-77e1602ffe1a"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35cd3d9d-22b1-46d1-bdc5-89e3693f4c0a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9415c16-b2a4-4cd0-8c83-1cc3bca68f71"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Desktop"",
            ""bindingGroup"": ""Desktop"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
            m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
            m_Player_Action1 = m_Player.FindAction("Action1", throwIfNotFound: true);
            m_Player_Action2 = m_Player.FindAction("Action2", throwIfNotFound: true);
            m_Player_SelectItem = m_Player.FindAction("SelectItem", throwIfNotFound: true);
            m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
            m_Player_Map = m_Player.FindAction("Map", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_Interact;
        private readonly InputAction m_Player_Aim;
        private readonly InputAction m_Player_Action1;
        private readonly InputAction m_Player_Action2;
        private readonly InputAction m_Player_SelectItem;
        private readonly InputAction m_Player_Menu;
        private readonly InputAction m_Player_Map;
        public struct PlayerActions
        {
            private @InputManager m_Wrapper;
            public PlayerActions(@InputManager wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @Interact => m_Wrapper.m_Player_Interact;
            public InputAction @Aim => m_Wrapper.m_Player_Aim;
            public InputAction @Action1 => m_Wrapper.m_Player_Action1;
            public InputAction @Action2 => m_Wrapper.m_Player_Action2;
            public InputAction @SelectItem => m_Wrapper.m_Player_SelectItem;
            public InputAction @Menu => m_Wrapper.m_Player_Menu;
            public InputAction @Map => m_Wrapper.m_Player_Map;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                    @Action1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction1;
                    @Action1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction1;
                    @Action1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction1;
                    @Action2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction2;
                    @Action2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction2;
                    @Action2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction2;
                    @SelectItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                    @SelectItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                    @SelectItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                    @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                    @Map.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                    @Map.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                    @Map.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                    @Aim.started += instance.OnAim;
                    @Aim.performed += instance.OnAim;
                    @Aim.canceled += instance.OnAim;
                    @Action1.started += instance.OnAction1;
                    @Action1.performed += instance.OnAction1;
                    @Action1.canceled += instance.OnAction1;
                    @Action2.started += instance.OnAction2;
                    @Action2.performed += instance.OnAction2;
                    @Action2.canceled += instance.OnAction2;
                    @SelectItem.started += instance.OnSelectItem;
                    @SelectItem.performed += instance.OnSelectItem;
                    @SelectItem.canceled += instance.OnSelectItem;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @Map.started += instance.OnMap;
                    @Map.performed += instance.OnMap;
                    @Map.canceled += instance.OnMap;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        private int m_DesktopSchemeIndex = -1;
        public InputControlScheme DesktopScheme
        {
            get
            {
                if (m_DesktopSchemeIndex == -1) m_DesktopSchemeIndex = asset.FindControlSchemeIndex("Desktop");
                return asset.controlSchemes[m_DesktopSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnAim(InputAction.CallbackContext context);
            void OnAction1(InputAction.CallbackContext context);
            void OnAction2(InputAction.CallbackContext context);
            void OnSelectItem(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnMap(InputAction.CallbackContext context);
        }
    }
}
