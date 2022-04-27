using ECS.Mono;

namespace CharacterSystem
{
    public class BotCharacter : MonoGameObjectEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Entity.isCharacter = true;
        }
    }
}