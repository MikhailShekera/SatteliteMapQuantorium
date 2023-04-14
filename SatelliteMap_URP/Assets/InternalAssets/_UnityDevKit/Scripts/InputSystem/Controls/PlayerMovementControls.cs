//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InternalAssets/_UnityDevKit/Scripts/InputSystem/Controls/PlayerMovementControls.inputactions
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

public partial class @PlayerMovementControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerMovementControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerMovementControls"",
    ""maps"": [
        {
            ""name"": ""Universal"",
            ""id"": ""617a2630-70ed-447f-b92d-bea4098eb0dd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ef77069e-d13b-490a-abe7-b9198dbfa62f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Value"",
                    ""id"": ""9c0006da-6d6b-4529-9293-69368ac2755c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad Movement"",
                    ""id"": ""c3d01f76-f092-46de-95ef-d0622a0256d6"",
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
                    ""id"": ""e8130de0-468f-4f20-a8c0-bb4018f4e238"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8626732b-0939-4791-8a7b-c894c4c2174f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c78d9835-214e-4726-aecc-f165470755f6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9d8c9f37-a8aa-4ce1-bbb6-43f9bb8aeb5f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard Movement"",
                    ""id"": ""7afad123-ebd1-4ce3-bad7-9042e6e59f82"",
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
                    ""id"": ""0732b78b-d69d-49f5-b79c-18883b9a794c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aef8b5c7-c520-4380-b105-7dd2fb2e89fa"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""30afba66-e7f6-4da8-9eec-a1841db47b71"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6cfa5d86-9ce1-4dbd-a014-2a241189429f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6ce95fe4-9609-4501-9172-bbb3db847c3d"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""09351975-3cb2-4b52-812f-b8769ddb02b1"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8d274f13-1328-470e-8058-25573be7b33d"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""799c8626-f781-445f-b6d0-8088ad9ff46e"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""55dc4469-d425-4515-9196-139b594c21ce"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d33cb4bc-9854-4d50-8d5b-dbcb96e25f95"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Movement"",
            ""id"": ""9309bd6f-d807-4b57-af19-12463c2f0375"",
            ""actions"": [
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""302997ae-f3e9-4daa-b3c3-94145bacc97d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""410b4515-ff33-4cb4-99f2-2c069001848a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""82219dca-cef9-4cda-bfdf-7af0a432c493"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TogglePauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""df2e8cc0-c7fc-4d37-bae0-2ec0d80e2503"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleCursorMode"",
                    ""type"": ""Button"",
                    ""id"": ""52becb64-9014-435b-a183-fc0ed656148a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollUI"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d79e0e5-31f1-4ee7-9e0d-3b3a987f8558"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeMapTest"",
                    ""type"": ""Button"",
                    ""id"": ""f01517c5-3ddb-4830-954c-f5895d8e31c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90d8e422-a41e-414b-acca-bef1696d22f5"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Hold(duration=0.01,pressPoint=0.01)"",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40983a78-ac40-4db1-9c25-0bdb03ff2a5d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fa43a89-550a-451d-910f-fbec55760076"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold(duration=0.01,pressPoint=0.01)"",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5ec337d-7c9d-4023-859c-939165139bc4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebcd577f-df90-4953-9eff-4009f655eaa6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b832fbe-3a54-4884-9b76-30af84bade6f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dafc145e-c013-4f87-a30e-33761537b955"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a039ac46-3e2a-484c-8fc7-688ca1df7d47"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd71f28f-093c-4125-83f4-31436f19c8ab"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ToggleCursorMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29c3150d-0adf-4a9e-89ba-dfb31f0113e6"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""ScrollUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae733be7-36bb-4f27-8939-829c24a4ba8d"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMapTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Grabbing"",
            ""id"": ""256bc6d7-7b70-4e3c-9726-be77b50ded84"",
            ""actions"": [
                {
                    ""name"": ""PushPullGrabbedObject"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b201a402-7fb0-4692-8a6d-e7a96ded1b8f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""9a0fa79f-58e9-4240-87f7-2ee189f48e2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Value"",
                    ""id"": ""c057afa9-79e1-4d41-b159-fb1b62162551"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7b2583d-0ec4-4c46-b1a0-04f78ea00c87"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""PushPullGrabbedObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdf00654-d3ed-4608-a24d-cf186a9cc5d7"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""24418345-7378-4ecd-b47a-57e43e4c0196"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""43e951be-5896-47b2-9e5f-61f3f2a09c68"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""863f0b0b-fb4d-4cc5-962e-8802bdf65b9e"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8d4851e5-cba8-4737-a079-98997878c9ba"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c83e7678-b17d-4eb7-91ac-1ed673e2be30"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d0ab5daf-265a-4b43-92e1-1fa24d63668d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Universal
        m_Universal = asset.FindActionMap("Universal", throwIfNotFound: true);
        m_Universal_Move = m_Universal.FindAction("Move", throwIfNotFound: true);
        m_Universal_RotateCamera = m_Universal.FindAction("RotateCamera", throwIfNotFound: true);
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Crouch = m_Movement.FindAction("Crouch", throwIfNotFound: true);
        m_Movement_Sprint = m_Movement.FindAction("Sprint", throwIfNotFound: true);
        m_Movement_Click = m_Movement.FindAction("Click", throwIfNotFound: true);
        m_Movement_TogglePauseMenu = m_Movement.FindAction("TogglePauseMenu", throwIfNotFound: true);
        m_Movement_ToggleCursorMode = m_Movement.FindAction("ToggleCursorMode", throwIfNotFound: true);
        m_Movement_ScrollUI = m_Movement.FindAction("ScrollUI", throwIfNotFound: true);
        m_Movement_ChangeMapTest = m_Movement.FindAction("ChangeMapTest", throwIfNotFound: true);
        // Grabbing
        m_Grabbing = asset.FindActionMap("Grabbing", throwIfNotFound: true);
        m_Grabbing_PushPullGrabbedObject = m_Grabbing.FindAction("PushPullGrabbedObject", throwIfNotFound: true);
        m_Grabbing_Drop = m_Grabbing.FindAction("Drop", throwIfNotFound: true);
        m_Grabbing_RotateCamera = m_Grabbing.FindAction("RotateCamera", throwIfNotFound: true);
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

    // Universal
    private readonly InputActionMap m_Universal;
    private IUniversalActions m_UniversalActionsCallbackInterface;
    private readonly InputAction m_Universal_Move;
    private readonly InputAction m_Universal_RotateCamera;
    public struct UniversalActions
    {
        private @PlayerMovementControls m_Wrapper;
        public UniversalActions(@PlayerMovementControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Universal_Move;
        public InputAction @RotateCamera => m_Wrapper.m_Universal_RotateCamera;
        public InputActionMap Get() { return m_Wrapper.m_Universal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UniversalActions set) { return set.Get(); }
        public void SetCallbacks(IUniversalActions instance)
        {
            if (m_Wrapper.m_UniversalActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_UniversalActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UniversalActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UniversalActionsCallbackInterface.OnMove;
                @RotateCamera.started -= m_Wrapper.m_UniversalActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_UniversalActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_UniversalActionsCallbackInterface.OnRotateCamera;
            }
            m_Wrapper.m_UniversalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
            }
        }
    }
    public UniversalActions @Universal => new UniversalActions(this);

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Crouch;
    private readonly InputAction m_Movement_Sprint;
    private readonly InputAction m_Movement_Click;
    private readonly InputAction m_Movement_TogglePauseMenu;
    private readonly InputAction m_Movement_ToggleCursorMode;
    private readonly InputAction m_Movement_ScrollUI;
    private readonly InputAction m_Movement_ChangeMapTest;
    public struct MovementActions
    {
        private @PlayerMovementControls m_Wrapper;
        public MovementActions(@PlayerMovementControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Crouch => m_Wrapper.m_Movement_Crouch;
        public InputAction @Sprint => m_Wrapper.m_Movement_Sprint;
        public InputAction @Click => m_Wrapper.m_Movement_Click;
        public InputAction @TogglePauseMenu => m_Wrapper.m_Movement_TogglePauseMenu;
        public InputAction @ToggleCursorMode => m_Wrapper.m_Movement_ToggleCursorMode;
        public InputAction @ScrollUI => m_Wrapper.m_Movement_ScrollUI;
        public InputAction @ChangeMapTest => m_Wrapper.m_Movement_ChangeMapTest;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Crouch.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Sprint.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Click.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnClick;
                @TogglePauseMenu.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnTogglePauseMenu;
                @TogglePauseMenu.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnTogglePauseMenu;
                @TogglePauseMenu.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnTogglePauseMenu;
                @ToggleCursorMode.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnToggleCursorMode;
                @ToggleCursorMode.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnToggleCursorMode;
                @ToggleCursorMode.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnToggleCursorMode;
                @ScrollUI.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnScrollUI;
                @ScrollUI.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnScrollUI;
                @ScrollUI.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnScrollUI;
                @ChangeMapTest.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnChangeMapTest;
                @ChangeMapTest.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnChangeMapTest;
                @ChangeMapTest.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnChangeMapTest;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @TogglePauseMenu.started += instance.OnTogglePauseMenu;
                @TogglePauseMenu.performed += instance.OnTogglePauseMenu;
                @TogglePauseMenu.canceled += instance.OnTogglePauseMenu;
                @ToggleCursorMode.started += instance.OnToggleCursorMode;
                @ToggleCursorMode.performed += instance.OnToggleCursorMode;
                @ToggleCursorMode.canceled += instance.OnToggleCursorMode;
                @ScrollUI.started += instance.OnScrollUI;
                @ScrollUI.performed += instance.OnScrollUI;
                @ScrollUI.canceled += instance.OnScrollUI;
                @ChangeMapTest.started += instance.OnChangeMapTest;
                @ChangeMapTest.performed += instance.OnChangeMapTest;
                @ChangeMapTest.canceled += instance.OnChangeMapTest;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Grabbing
    private readonly InputActionMap m_Grabbing;
    private IGrabbingActions m_GrabbingActionsCallbackInterface;
    private readonly InputAction m_Grabbing_PushPullGrabbedObject;
    private readonly InputAction m_Grabbing_Drop;
    private readonly InputAction m_Grabbing_RotateCamera;
    public struct GrabbingActions
    {
        private @PlayerMovementControls m_Wrapper;
        public GrabbingActions(@PlayerMovementControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PushPullGrabbedObject => m_Wrapper.m_Grabbing_PushPullGrabbedObject;
        public InputAction @Drop => m_Wrapper.m_Grabbing_Drop;
        public InputAction @RotateCamera => m_Wrapper.m_Grabbing_RotateCamera;
        public InputActionMap Get() { return m_Wrapper.m_Grabbing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GrabbingActions set) { return set.Get(); }
        public void SetCallbacks(IGrabbingActions instance)
        {
            if (m_Wrapper.m_GrabbingActionsCallbackInterface != null)
            {
                @PushPullGrabbedObject.started -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnPushPullGrabbedObject;
                @PushPullGrabbedObject.performed -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnPushPullGrabbedObject;
                @PushPullGrabbedObject.canceled -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnPushPullGrabbedObject;
                @Drop.started -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnDrop;
                @RotateCamera.started -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_GrabbingActionsCallbackInterface.OnRotateCamera;
            }
            m_Wrapper.m_GrabbingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PushPullGrabbedObject.started += instance.OnPushPullGrabbedObject;
                @PushPullGrabbedObject.performed += instance.OnPushPullGrabbedObject;
                @PushPullGrabbedObject.canceled += instance.OnPushPullGrabbedObject;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
            }
        }
    }
    public GrabbingActions @Grabbing => new GrabbingActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IUniversalActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
    }
    public interface IMovementActions
    {
        void OnCrouch(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnTogglePauseMenu(InputAction.CallbackContext context);
        void OnToggleCursorMode(InputAction.CallbackContext context);
        void OnScrollUI(InputAction.CallbackContext context);
        void OnChangeMapTest(InputAction.CallbackContext context);
    }
    public interface IGrabbingActions
    {
        void OnPushPullGrabbedObject(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
    }
}