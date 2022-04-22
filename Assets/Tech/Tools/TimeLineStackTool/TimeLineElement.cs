namespace Tools.TimeLineStackTool
{
    public abstract class TimeLineElement
    {
        public TimeLineElement(float time)
        {
            pushTime = time;
        }
    
        public float pushTime { get; }
    }
}