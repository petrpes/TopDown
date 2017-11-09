using UnityEngine;

public class InputManager : IInputManager
{
    public static InputManager Instance = new InputManager();

    private InputDictionary _inputDictionary;

    private EmptyInputArguments _emptyInputArguments = new EmptyInputArguments();

    private static KeyboardInputDevice Keyboard = new KeyboardInputDevice();
    private static MouseInputDevice Mouse = new MouseInputDevice();

    private InputDictionary InputDictionary
    {
        get
        {
            if (_inputDictionary == null)
            {
                _inputDictionary = new InputDictionary()
                {
                    {
                        InputAttributesSet.Walk,
                        new ActionInputControl((arguments) => 
                        {
                            Vector3 directionDown = Keyboard.IsHolding(KeyCode.S) ? Vector3.down : Vector3.zero;
                            Vector3 directionUp = Keyboard.IsHolding(KeyCode.W) ? Vector3.up : Vector3.zero;
                            Vector3 directionLeft = Keyboard.IsHolding(KeyCode.A) ? Vector3.left : Vector3.zero;
                            Vector3 directionRight = Keyboard.IsHolding(KeyCode.D) ? Vector3.right : Vector3.zero;
                            Vector3 direction = directionDown + directionUp + directionLeft + directionRight;

                            (arguments as IDirectionInputArguments).Direction = new DirectionVector(direction);
                            return direction != Vector3.zero;
                        })
                    },

                    {
                        InputAttributesSet.WeaponDirection,
                        new ActionInputControl((arguments) => 
                        {
                            Mouse.GetCursorPosition(arguments as IPositionInputArguments);
                            return Mouse.IsCursorMoving();
                        })
                    },

                    {InputAttributesSet.WeaponShoot,
                        new ActionInputControl((arguments) =>
                        {
                            return Mouse.IsHolding(MouseButtons.Left);
                        })
                    }
                };
            }
            return _inputDictionary;
        }
    }

    public bool GetIsControl<A>(InputAttribute attribute, A arguments) where A : class, IInputArguments
    {
        return InputDictionary.GetIsControl(attribute, arguments);
    }

    public bool GetIsControl(InputAttribute attribute)
    {
        return InputDictionary.GetIsControl(attribute, _emptyInputArguments);
    }
}

public enum InputDeviceType
{
    Keyboard,
    Mouse
}

public enum InputType
{
    Press,
    Release,
    Hold,
    Move
}

