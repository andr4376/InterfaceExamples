using Core.PlayerInput.Enums;

namespace Core.PlayerInput
{
    public  interface IGetInput
    {
        PlayerInputState GetInput();
    }

    public struct PlayerInputState
    {
        public bool JumpButtonIsDown { get; set; }
        public bool AttackButtonIsDown { get; set; }
        public HORISONTAL_MOVEMENT HorisontalMovement { get; set; }
        public VERTICAL_MOVEMENT VerticalMovement { get; set; }

        public override string ToString()
        {
            return $"jump:{JumpButtonIsDown}\nattack:{AttackButtonIsDown}\n{HorisontalMovement}\n{VerticalMovement}";
        }
    }

}
