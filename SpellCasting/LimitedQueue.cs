using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.SpellCasting
{
    class LimitedQueue<T> : Queue<T>
    {
        public int limit { get; set; }
        public T recentDequeue;
        

        //set limit at instantiation
        public LimitedQueue(int limit)
        {
            this.limit = limit;
        }

        //When enqueing, if I need to dequeue something first it will happen. Else enqueue.

        public void LimitedEnqueue(T input)
        {
            while(Count > limit)
            {
                recentDequeue = Dequeue();
            }
            base.Enqueue(input);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Purge()
        {
            for(int i = 0; i <= Count; i++)
            {
                try
                {
                    Dequeue();
                }
                catch(Exception e)
                {
                    return;
                }
            }
        }

    }
}
