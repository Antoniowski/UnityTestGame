//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/PlayerInputController.inputactions
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

public partial class @PlayerInputController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputController"",
    ""maps"": [
        {
            ""name"": ""CharacterInputController"",
            ""id"": ""73b02cb1-be14-42d0-a37a-dc9baf2444e9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""efcd71f1-669a-4152-be20-d580efc1fff6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""5fb07c67-7abd-432f-a930-0884a282e47f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""6e0ef512-14df-4fd9-8aae-97f134f7fa67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""99c252e4-be3c-4673-92c1-4579297c28d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c8113c93-b606-4136-aa82-f7ed02fc72d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b276a12a-13ef-4ff2-b09b-deed4ac0bb19"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7ffa62e1-ebb9-4a53-acd5-9ae38b818926"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""626d57f9-28a0-4057-83fb-a1e0e2a77aaa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""24f4828a-b128-4550-8206-6427a8892dfc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""16488811-af75-45bf-bbe1-8de342ffae07"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8055217e-bfc6-4d76-bbf7-06edc17e886a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""caf8fc4e-269f-4a81-80f4-582fbf6339ea"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0259d8df-986d-4fb1-9f62-5eae83e1a510"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Hold(duration=0.25)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ecee32-5fd5-4b48-99ea-e0c4eb08ff42"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b460b59-b296-4522-a71a-7ff20eb49b0a"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbd85c8a-cbb6-4a8d-aafc-d15efe206b5c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec8f7571-b6c3-4e18-aeeb-48e948de91cb"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eab1b137-1eab-4978-bc36-4328fd5b8536"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d80eeb8f-b051-4511-8e49-f68057c34f6c"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterInputController
        m_CharacterInputController = asset.FindActionMap("CharacterInputController", throwIfNotFound: true);
        m_CharacterInputController_Move = m_CharacterInputController.FindAction("Move", throwIfNotFound: true);
        m_CharacterInputController_Run = m_CharacterInputController.FindAction("Run", throwIfNotFound: true);
        m_CharacterInputController_Dodge = m_CharacterInputController.FindAction("Dodge", throwIfNotFound: true);
        m_CharacterInputController_Interact = m_CharacterInputController.FindAction("Interact", throwIfNotFound: true);
        m_CharacterInputController_Attack = m_CharacterInputController.FindAction("Attack", throwIfNotFound: true);
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

    // CharacterInputController
    private readonly InputActionMap m_CharacterInputController;
    private ICharacterInputControllerActions m_CharacterInputControllerActionsCallbackInterface;
    private readonly InputAction m_CharacterInputController_Move;
    private readonly InputAction m_CharacterInputController_Run;
    private readonly InputAction m_CharacterInputController_Dodge;
    private readonly InputAction m_CharacterInputController_Interact;
    private readonly InputAction m_CharacterInputController_Attack;
    public struct CharacterInputControllerActions
    {
        private @PlayerInputController m_Wrapper;
        public CharacterInputControllerActions(@PlayerInputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterInputController_Move;
        public InputAction @Run => m_Wrapper.m_CharacterInputController_Run;
        public InputAction @Dodge => m_Wrapper.m_CharacterInputController_Dodge;
        public InputAction @Interact => m_Wrapper.m_CharacterInputController_Interact;
        public InputAction @Attack => m_Wrapper.m_CharacterInputController_Attack;
        public InputActionMap Get() { return m_Wrapper.m_CharacterInputController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterInputControllerActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterInputControllerActions instance)
        {
            if (m_Wrapper.m_CharacterInputControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnRun;
                @Dodge.started -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnDodge;
                @Interact.started -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterInputControllerActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_CharacterInputControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public CharacterInputControllerActions @CharacterInputController => new CharacterInputControllerActions(this);
    public interface ICharacterInputControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
