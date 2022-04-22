namespace Tools.TimeLineStackTool
{
    public class TimeLineChain
    {
        public TimeLineChain Prev;
        public readonly TimeLineElement Element;
        public TimeLineChain Next;
    
        public TimeLineChain(TimeLineElement element)
        {
            Element = element;
        }
    }
}