namespace ECS.Mono
{
    public class MonoGameEntity : MonoProvider
    {
        protected GameEntity Entity;

        protected virtual void Awake()
        {
            Entity = Contexts.game.CreateEntity();
        }
    }
}