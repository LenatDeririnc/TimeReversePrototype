namespace ECS
{
    public abstract class BaseEcsManager
    {
        protected Contexts Contexts;
    
        public Entitas.Systems Systems;
        public Entitas.Systems FixedSystems;
        public Entitas.Systems LateSystems;
        
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