using CharacterSystem.Player.ECM.Scripts.Fields;

namespace CharacterSystem.Player
{
    public interface IInputControlling
    {
        public void SendInputData(InputData data);
    }
}