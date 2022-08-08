using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStackItem<T>
    {
        public T Value { get; set; }
        public LimitedSizeStackItem<T> Next { get; set; }
        public LimitedSizeStackItem<T> Previous { get; set; }
    }

    public class LimitedSizeStack<T>
    {
        LimitedSizeStackItem<T> up;
        LimitedSizeStackItem<T> bottom;


        int Limit { get; set; }
        int MaxSize { get; set; }

        public LimitedSizeStack(int limit)
        {
            Limit = limit;
            MaxSize = limit;
        }

        public void Push(T value)
        {
            if (MaxSize != 0)
            {
                if (up == null)
                {
                    bottom = up = new LimitedSizeStackItem<T> { Value = value, Next = null, Previous = null };
                    Limit--;
                }
                else if (Limit == 0)
                {
                    bottom = bottom.Next;
                    bottom.Previous = null;
                    var item = new LimitedSizeStackItem<T> { Value = value, Next = null, Previous = null };
                    item.Previous = up;
                    up.Next = item;
                    up = item;
                }
                else
                {
                    var item = new LimitedSizeStackItem<T> { Value = value, Next = null, Previous = null };
                    item.Previous = up;
                    up.Next = item;
                    up = item;
                    Limit--;
                }
            }
        }
    
        public T Pop()
        {
            if (up == null) throw new InvalidOperationException();
            var result = up.Value;
            up = up.Previous;
            Limit++;
            if (up == null)
                bottom = null;
            return result;
        }

        public int Count
        {
            get
            {
                return MaxSize - Limit;
            }
        }
    }
}