namespace Tools.TimeLineStackTool
{
    public class TimeLineStack
    {
        private TimeLineChain _tail;

        public void Push(TimeLineElement element)
        {
            if (_tail == null)
            {
                _tail = new TimeLineChain(element);
                return;
            }
            
            var newChain = new TimeLineChain(element);
            newChain.Prev = _tail;
            _tail.Next = newChain;

            _tail = newChain;
        }

        public TimeLineElement Pop(float time)
        {
            var chain = _tail;
            
            if (chain is null)
                return null;

            if (time >= chain.Element.pushTime)
                return chain.Element;

            while (time < chain.Element.pushTime && chain.Prev != null)
            {
                chain = chain.Prev;
            }
            
            _tail = chain.Prev;
            return chain.Element;
        }

        public TimeLineElement Peek()
        {
            return _tail?.Element;
        }
    }
}