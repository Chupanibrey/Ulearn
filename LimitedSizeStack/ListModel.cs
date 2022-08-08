using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public Tuple<
            LimitedSizeStack<TItem>, 
            LimitedSizeStack<int>, 
            LimitedSizeStack<string>> 
            BasketStack { get; set; }
        public int Limit;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            BasketStack = Tuple.Create(
                new LimitedSizeStack<TItem>(limit), 
                new LimitedSizeStack<int>(limit), 
                new LimitedSizeStack<string>(limit));
        }

        public void AddItem(TItem item)
        {
            if (Limit != 0)
            {
                Items.Add(item);
                BasketStack.Item1.Push(item);
                BasketStack.Item2.Push(Items.IndexOf(item));
                BasketStack.Item3.Push("Add");
                Limit--;
            }
        }

        public void RemoveItem(int index)
        {
            BasketStack.Item1.Push(Items[index]);
            BasketStack.Item2.Push(index);
            BasketStack.Item3.Push("Remove");
            Items.RemoveAt(index);
            Limit--;
        }

        public bool CanUndo()
        {
            return BasketStack.Item1.Count != 0;
        }

        public void Undo()
        {
            if (BasketStack.Item3.Pop() == "Add")
            {
                Items.Remove(BasketStack.Item1.Pop());
                BasketStack.Item2.Pop();
                Limit++;
            }
            else
            {
                Items.Insert(BasketStack.Item2.Pop(), BasketStack.Item1.Pop());
                Limit++;
            }
        }
    }
}