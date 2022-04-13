using Core.PlayerInput.Enums;
using System;

namespace Core.PlayerInput.Implementations.PC
{
    public class GetInputForPcService : IGetInput
    {
        private readonly IGetMouseInputService getMouseInputService;
        private readonly IKeyboardMovementMapperService keyboardMovementMapperService;

        public GetInputForPcService(
            IGetMouseInputService getMouseInputService,
            IKeyboardMovementMapperService keyboardMovementMapperService
            )
        {
            this.getMouseInputService = getMouseInputService ?? throw new ArgumentNullException(nameof(getMouseInputService));
            this.keyboardMovementMapperService = keyboardMovementMapperService ?? throw new ArgumentNullException(nameof(keyboardMovementMapperService));
        }

        public PlayerInputState GetInput()
        {
            PlayerInputState result = default;

            result.AttackButtonIsDown = this.getMouseInputService.IsLeftClickDown();

            result = keyboardMovementMapperService.GetAndMapKeyboardMovementInput(ref result);

            return result;
        }
    }
}
