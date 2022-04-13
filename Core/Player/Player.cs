using Core.PlayerInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Player
{
    public sealed class Player
    {
        /// <summary>
        /// Reads Player Input
        /// </summary>
        private readonly IGetInput getInput;

        public PlayerInputState playerInput;

        public Player(IGetInput getInput)
        {
            this.getInput = getInput ?? throw new ArgumentNullException(nameof(getInput));
        }

        public void Update()
        {
            //Hent input (lige meget hvilken implementation)
            playerInput = this.getInput.GetInput();

            Console.Clear();
            Console.WriteLine(playerInput.ToString());
        }

    }
}
