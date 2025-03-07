//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/asoliddev - Auto Chess/InputControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""f7373b13-ec9c-49a8-9425-9a0b678c1b99"",
            ""actions"": [
                {
                    ""name"": ""CamMove"",
                    ""type"": ""Value"",
                    ""id"": ""033e2a9e-0696-46fe-9aef-ed0f1704b09e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CamZoom"",
                    ""type"": ""Value"",
                    ""id"": ""dce08143-f133-4022-8f97-64d27126cbb9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PopupNail1"",
                    ""type"": ""Button"",
                    ""id"": ""c0ccbee5-689f-49a8-b1e1-5bba43ced469"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PopupUnnail1"",
                    ""type"": ""Button"",
                    ""id"": ""52bc0365-81f5-4700-8c6e-05e19e19756c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bba10b47-1c06-42c7-a58a-211e2c43e88b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""08f49d6b-ad97-44ca-8983-738864756154"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b63765dd-491a-41aa-aa2b-b8b5870c5fba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c4242a40-7893-4f64-9cf5-458b96458a4c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5ac3b5fc-1242-4573-8dbe-0476e6a1d810"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""30a269f7-032d-4949-954b-fb9279a537ec"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PopupNail1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a011453b-ccde-4f84-ab5f-cccd1c7c21ef"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PopupUnnail1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10f1907e-6a37-4d1e-8739-caad8f4ba07f"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""ba2a913f-6efc-4673-bd3b-c5ab8149f656"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_CamMove = m_GamePlay.FindAction("CamMove", throwIfNotFound: true);
        m_GamePlay_CamZoom = m_GamePlay.FindAction("CamZoom", throwIfNotFound: true);
        m_GamePlay_PopupNail1 = m_GamePlay.FindAction("PopupNail1", throwIfNotFound: true);
        m_GamePlay_PopupUnnail1 = m_GamePlay.FindAction("PopupUnnail1", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_CamMove;
    private readonly InputAction m_GamePlay_CamZoom;
    private readonly InputAction m_GamePlay_PopupNail1;
    private readonly InputAction m_GamePlay_PopupUnnail1;
    public struct GamePlayActions
    {
        private @InputControls m_Wrapper;
        public GamePlayActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CamMove => m_Wrapper.m_GamePlay_CamMove;
        public InputAction @CamZoom => m_Wrapper.m_GamePlay_CamZoom;
        public InputAction @PopupNail1 => m_Wrapper.m_GamePlay_PopupNail1;
        public InputAction @PopupUnnail1 => m_Wrapper.m_GamePlay_PopupUnnail1;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @CamMove.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamMove;
                @CamMove.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamMove;
                @CamMove.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamMove;
                @CamZoom.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamZoom;
                @CamZoom.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamZoom;
                @CamZoom.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamZoom;
                @PopupNail1.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupNail1;
                @PopupNail1.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupNail1;
                @PopupNail1.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupNail1;
                @PopupUnnail1.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupUnnail1;
                @PopupUnnail1.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupUnnail1;
                @PopupUnnail1.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPopupUnnail1;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CamMove.started += instance.OnCamMove;
                @CamMove.performed += instance.OnCamMove;
                @CamMove.canceled += instance.OnCamMove;
                @CamZoom.started += instance.OnCamZoom;
                @CamZoom.performed += instance.OnCamZoom;
                @CamZoom.canceled += instance.OnCamZoom;
                @PopupNail1.started += instance.OnPopupNail1;
                @PopupNail1.performed += instance.OnPopupNail1;
                @PopupNail1.canceled += instance.OnPopupNail1;
                @PopupUnnail1.started += instance.OnPopupUnnail1;
                @PopupUnnail1.performed += instance.OnPopupUnnail1;
                @PopupUnnail1.canceled += instance.OnPopupUnnail1;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    public struct UIActions
    {
        private @InputControls m_Wrapper;
        public UIActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGamePlayActions
    {
        void OnCamMove(InputAction.CallbackContext context);
        void OnCamZoom(InputAction.CallbackContext context);
        void OnPopupNail1(InputAction.CallbackContext context);
        void OnPopupUnnail1(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
    }
}
