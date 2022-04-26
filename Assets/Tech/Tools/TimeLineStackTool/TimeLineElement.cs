namespace Tools.TimeLineStackTool
{
    public abstract class TimeLineElement
    {
        public TimeLineElement(int entityId, float time)
        {
            EntityId = entityId;
            pushTime = time;
        }
    
        public readonly int EntityId;
        public float pushTime { get; }
    }
}