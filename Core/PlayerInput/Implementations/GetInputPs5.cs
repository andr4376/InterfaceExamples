
namespace Core.PlayerInput
{
    public  class GetInputPs5 : IGetInput
    {
        public PlayerInputState GetInput()
        {
            //bare så tråden ikke går amok, i while loopet :)
            System.Threading.Thread.Sleep(1_000);
            return default;
        }
    }
}
