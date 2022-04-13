using Core.PlayerInput.Enums;
using System;


namespace Core.PlayerInput.Implementations.PC
{
    public interface IKeyboardMovementMapperService
    {
        PlayerInputState GetAndMapKeyboardMovementInput(ref PlayerInputState result);
    }

    public sealed class KeyboardMovementMapperService : IKeyboardMovementMapperService
    {
        public PlayerInputState GetAndMapKeyboardMovementInput(ref PlayerInputState result)
        {
            ConsoleKey playerInputKey = Console.ReadKey(true).Key;

            switch (playerInputKey)
            {
                case ConsoleKey.Spacebar:
                    result.JumpButtonIsDown = true;
                    break;

                case ConsoleKey.A:
                    result.HorisontalMovement = HORISONTAL_MOVEMENT.LEFT;
                    break;

                case ConsoleKey.D:
                    result.HorisontalMovement = HORISONTAL_MOVEMENT.RIGHT;
                    break;

                case ConsoleKey.W:
                    result.VerticalMovement = VERTICAL_MOVEMENT.UP;
                    break;

                case ConsoleKey.S:
                    result.VerticalMovement = VERTICAL_MOVEMENT.DOWN;
                    break;

                default:
                    break;
            }
            return result;
        }
    }
}
