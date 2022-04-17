namespace Tools.TimeLineStackTool
{
    public class TimeLineStack
    {
        private TimeLineElement _tail;

        public void Push(TimeLineElement element)
        {
            if (_tail == null)
            {
                _tail = element;
                return;
            }
        
            _tail.Next = element;
            element.Prev = _tail;
            
            _tail = element;
        }

        public TimeLineElement Pop(float time)
        {
            var element = _tail;
            
            if (element is null)
                return null;
                
            if (time >= element.pushTime)
                return element;

            while (time < element.pushTime && element.Prev != null)
            {
                element = element.Prev;
            }
            
            _tail = element.Prev;
            return element;
        }

        public TimeLineElement Peek()
        {
            return _tail;
        }
    }
}