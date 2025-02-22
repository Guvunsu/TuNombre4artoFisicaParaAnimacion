//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scenes/Billiards/InputActions/IA_Blliards.inputactions
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

namespace Gavryk.Physics.Billiard
{
    public partial class @IA_Blliards: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @IA_Blliards()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""IA_Blliards"",
    ""maps"": [
        {
            ""name"": ""Billiards"",
            ""id"": ""03067473-00a3-4140-baff-4db54bb2d23b"",
            ""actions"": [
                {
                    ""name"": ""Mouse ClickRight"",
                    ""type"": ""Button"",
                    ""id"": ""5b19a40b-b094-4552-b4ac-a4619ba77e72"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Keyboard SpaceBar"",
                    ""type"": ""Button"",
                    ""id"": ""fe88f501-8c10-4ed4-9cc3-d88c982a003d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""869eeaa4-b09c-40d2-b01c-76c690c5cc0f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Control"",
                    ""action"": ""Mouse ClickRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6797a45c-9860-435b-9b3e-3b88c060e103"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Control"",
                    ""action"": ""Keyboard SpaceBar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Control"",
            ""bindingGroup"": ""Control"",
            ""devices"": []
        },
        {
            ""name"": ""New Control Scheme"",
            ""bindingGroup"": ""New Control Scheme"",
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
            // Billiards
            m_Billiards = asset.FindActionMap("Billiards", throwIfNotFound: true);
            m_Billiards_MouseClickRight = m_Billiards.FindAction("Mouse ClickRight", throwIfNotFound: true);
            m_Billiards_KeyboardSpaceBar = m_Billiards.FindAction("Keyboard SpaceBar", throwIfNotFound: true);
        }

        ~@IA_Blliards()
        {
            UnityEngine.Debug.Assert(!m_Billiards.enabled, "This will cause a leak and performance issues, IA_Blliards.Billiards.Disable() has not been called.");
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

        // Billiards
        private readonly InputActionMap m_Billiards;
        private List<IBilliardsActions> m_BilliardsActionsCallbackInterfaces = new List<IBilliardsActions>();
        private readonly InputAction m_Billiards_MouseClickRight;
        private readonly InputAction m_Billiards_KeyboardSpaceBar;
        public struct BilliardsActions
        {
            private @IA_Blliards m_Wrapper;
            public BilliardsActions(@IA_Blliards wrapper) { m_Wrapper = wrapper; }
            public InputAction @MouseClickRight => m_Wrapper.m_Billiards_MouseClickRight;
            public InputAction @KeyboardSpaceBar => m_Wrapper.m_Billiards_KeyboardSpaceBar;
            public InputActionMap Get() { return m_Wrapper.m_Billiards; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BilliardsActions set) { return set.Get(); }
            public void AddCallbacks(IBilliardsActions instance)
            {
                if (instance == null || m_Wrapper.m_BilliardsActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_BilliardsActionsCallbackInterfaces.Add(instance);
                @MouseClickRight.started += instance.OnMouseClickRight;
                @MouseClickRight.performed += instance.OnMouseClickRight;
                @MouseClickRight.canceled += instance.OnMouseClickRight;
                @KeyboardSpaceBar.started += instance.OnKeyboardSpaceBar;
                @KeyboardSpaceBar.performed += instance.OnKeyboardSpaceBar;
                @KeyboardSpaceBar.canceled += instance.OnKeyboardSpaceBar;
            }

            private void UnregisterCallbacks(IBilliardsActions instance)
            {
                @MouseClickRight.started -= instance.OnMouseClickRight;
                @MouseClickRight.performed -= instance.OnMouseClickRight;
                @MouseClickRight.canceled -= instance.OnMouseClickRight;
                @KeyboardSpaceBar.started -= instance.OnKeyboardSpaceBar;
                @KeyboardSpaceBar.performed -= instance.OnKeyboardSpaceBar;
                @KeyboardSpaceBar.canceled -= instance.OnKeyboardSpaceBar;
            }

            public void RemoveCallbacks(IBilliardsActions instance)
            {
                if (m_Wrapper.m_BilliardsActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IBilliardsActions instance)
            {
                foreach (var item in m_Wrapper.m_BilliardsActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_BilliardsActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public BilliardsActions @Billiards => new BilliardsActions(this);
        private int m_ControlSchemeIndex = -1;
        public InputControlScheme ControlScheme
        {
            get
            {
                if (m_ControlSchemeIndex == -1) m_ControlSchemeIndex = asset.FindControlSchemeIndex("Control");
                return asset.controlSchemes[m_ControlSchemeIndex];
            }
        }
        private int m_NewControlSchemeSchemeIndex = -1;
        public InputControlScheme NewControlSchemeScheme
        {
            get
            {
                if (m_NewControlSchemeSchemeIndex == -1) m_NewControlSchemeSchemeIndex = asset.FindControlSchemeIndex("New Control Scheme");
                return asset.controlSchemes[m_NewControlSchemeSchemeIndex];
            }
        }
        public interface IBilliardsActions
        {
            void OnMouseClickRight(InputAction.CallbackContext context);
            void OnKeyboardSpaceBar(InputAction.CallbackContext context);
        }
    }
}
