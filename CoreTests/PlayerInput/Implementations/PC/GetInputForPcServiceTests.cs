using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.PlayerInput.Implementations.PC;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Core.PlayerInput.Enums;

namespace Core.PlayerInput.Implementations.PC.Tests
{
    /// <summary>
    /// Hjælpeklasse til Unit Tests
    /// </summary>
    internal sealed class GetInputForPcServiceTests_Helper
    {
        internal Mock<IGetMouseInputService> getMouseInputService;
        internal Mock<IKeyboardMovementMapperService> keyboardMovementMapperService;
        internal GetInputForPcService GetService()
        {
            this.getMouseInputService = new Mock<IGetMouseInputService>();
            this.keyboardMovementMapperService = new Mock<IKeyboardMovementMapperService>();

            return new GetInputForPcService(
                this.getMouseInputService.Object,
                this.keyboardMovementMapperService.Object);

        }
    }


    /// <summary>
    /// Unit Tests som tester  PC-implementationen <see cref="GetInputForPcService"/> af <see cref="IGetInput"/>
    /// </summary>
    [TestClass()]
    public class GetInputForPcServiceTests
    {
        [TestMethod()]
        public void GetInputTest_NoInput()
        {
            //arrange
            GetInputForPcServiceTests_Helper helper = new GetInputForPcServiceTests_Helper();
            GetInputForPcService service = helper.GetService();

            //act
            PlayerInputState result = service.GetInput();
            PlayerInputState expected = default; //Enum.NONE, false and 0's

            //assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(false, result.AttackButtonIsDown);
            Assert.AreEqual(false, result.JumpButtonIsDown);
            Assert.AreEqual(VERTICAL_MOVEMENT.NONE, result.VerticalMovement);
            Assert.AreEqual(VERTICAL_MOVEMENT.NONE, result.VerticalMovement);
        }

        [TestMethod()]
        public void GetInputTest_MouseInput_EqAttack()
        {
            //arrange
            GetInputForPcServiceTests_Helper helper = new GetInputForPcServiceTests_Helper();
            GetInputForPcService service = helper.GetService();

            //MOCKING! https://stackoverflow.com/a/2666006
            //GetInputForPcServicen's IGetMouseInputService object vil nu lade som om at den returner true!
            helper.getMouseInputService.Setup(x =>
                x.IsLeftClickDown())
                .Returns(true);
            //Og hvis klassen tror at spilleren trykker på leftClick, burde spilleren angribe

            //act
            PlayerInputState result = service.GetInput();

            //assert
            Assert.AreEqual(true, result.AttackButtonIsDown);
        }
    }
}