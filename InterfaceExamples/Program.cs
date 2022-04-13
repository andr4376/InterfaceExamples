
#define PC_BUILD
//#define PS5_BUILD
//#define XBOX360_BUILD

using Core.Player;
using Core.PlayerInput;
using Core.PlayerInput.Implementations.PC;
using System;

namespace InterfaceExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = null;
#if PC_BUILD
            player = new Player(
               getInput: new GetInputForPcService(
                           getMouseInputService: new GetMouseInputService(),
                           keyboardMovementMapperService: new KeyboardMovementMapperService()
                   )
               );

#elif PS5_BUILD
            player = new Player(getInput: new GetInputPs5());
#elif XBOX360_BUILD
            player = new Player(getInput: new GetInputXbox360());
#endif

            if (player is null)
                throw new InvalidOperationException("Kunne ikke finde en platform at bygge spilleren til :(");

            while (true)
            {
                player.Update();
            }
        }
    }
}
