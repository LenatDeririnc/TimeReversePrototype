namespace ECS.EcsManagers
{
    public abstract class BaseEcsManager
    {
        protected readonly Contexts Contexts;
    
        public readonly Entitas.Systems Systems;
        public readonly Entitas.Systems FixedSystems;
        public readonly Entitas.Systems LateSystems;
        
        public BaseEcsManager(Contexts contexts)
        {
            Contexts = contexts; 
        
            Systems = new Entitas.Systems();
            FixedSystems = new Entitas.Systems();
            LateSystems = new Entitas.Systems();
        }
    }
    
    public enum EcsManagerType
    {
        GameEcsManager,
        TestUnitEcsManager,
    }
}