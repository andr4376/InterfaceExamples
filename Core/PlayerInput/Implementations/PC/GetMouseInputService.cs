using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.PlayerInput.Implementations.PC
{
    public interface IGetMouseInputService
    {
        bool IsLeftClickDown();
    }

    public sealed class GetMouseInputService : IGetMouseInputService
    {
        [DllImport("user32.dll")]
        private static extern bool GetAsyncKeyState(int button);

        public bool IsLeftClickDown()
        {
            return IsMouseButtonPressed(MouseButton.LeftMouseButton);
        }

        private bool IsMouseButtonPressed(MouseButton button)
        {
            return GetAsyncKeyState((int)button);
        }
    }
}
