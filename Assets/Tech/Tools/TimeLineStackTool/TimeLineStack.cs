using System.Collections.Generic;
using UnityEngine;

namespace Tools.TimeLineStackTool
{
    public class TimeLineStack
    {
        public TimeLineStack(Contexts contexts)
        {
            _contexts = contexts;
        }
    
        private readonly Dictionary<int, TimeLineChain> _tails = new Dictionary<int, TimeLineChain>();
        private readonly Contexts _contexts;

        public void Push(TimeLineElement element)
        {
            if (!_tails.ContainsKey(element.EntityId) || _tails[element.EntityId] == null)
            {
                _tails[element.EntityId] = new TimeLineChain(element);
                return;
            }

            var newChain = new TimeLineChain(element);
            newChain.Prev = _tails[element.EntityId];
            _tails[element.EntityId].Next = newChain;

            _tails[element.EntityId] = newChain;
        }

        public TimeLineElement Pop(int entityId, float time)
        {
            if (!_tails.ContainsKey(entityId))
                return null;
        
            var chain = _tails[entityId];
            
            if (chain is null)
                return null;

            if (time > chain.Element.pushTime || Mathf.Abs(time - chain.Element.pushTime) < _contexts.time.tickRate.Value)
                return chain.Element;
            
            while (time < chain.Element.pushTime - _contexts.time.tickRate.Value && chain.Prev != null)
            {
                chain = chain.Prev;
            }

            if (time < chain.Element.pushTime - _contexts.time.tickRate.Value)
            {
                chain = null;
            }
            
            // if (time >= chain.Element.pushTime)
            //     return chain.Element;
            //
            // while (time < chain.Element.pushTime && chain.Prev != null)
            // {
            //     chain = chain.Prev;
            // }
            
            _tails[entityId] = chain;
            return chain?.Element;
        }

        public void Clear(int entityId)
        {
            if (!_tails.ContainsKey(entityId))
                return;
                
            _tails[entityId] = null;
        }

        public TimeLineElement Peek(int entityId)
        {
            return _tails.ContainsKey(entityId) ? _tails[entityId]?.Element : null;
        }
    }
}