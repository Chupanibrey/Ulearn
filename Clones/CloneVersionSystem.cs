using System;
using System.Collections.Generic;

namespace Clones
{
	public class IClone
    {
        public int Number { get; set; }
		public Stack<int> Learned { get; set; }
        public Stack<int> Canceled { get; set; }

        public IClone(int number)
        {
            Number = number;
            Learned = new Stack<int>();
            Canceled = new Stack<int>();
        }

        public IClone(int number, IClone otherClone)
        {
            this.Number = number;
            this.Learned = new Stack<int>(otherClone.Learned);
            this.Canceled = new Stack<int>(otherClone.Canceled);
        }

        public string Learn (int numberProgram)
        {
            Learned.Push(numberProgram);
            return null;
        }

        public string Rollback()
        {
            Canceled.Push(Learned.Pop());
            return null;
        }

        public string Relearn()
        {
            Learned.Push(Canceled.Pop());
            return null;
        }

        public string Check()
        {
            try
            {
                return Learned.Check().ToString();
            }
            catch
            {
                return "basic";
            }
        }
    }

	public class CloneVersionSystem : ICloneVersionSystem
	{
        public int CloneCount { get; set; }
        public List<IClone> AllClone;

        public CloneVersionSystem()
        {
            CloneCount = 0;
            AllClone = new List<IClone>() { new IClone(++CloneCount) };
        }

        public string Clone(int numberClone)
        {
            AllClone.Add(new IClone(++CloneCount,AllClone[numberClone]));
            return null;
        }

        public string Execute(string query)
		{
            var commands = query.Split(' ');

            switch(commands[0])
            {
                case "learn":
                    return AllClone[int.Parse(commands[1]) - 1].Learn(int.Parse(commands[2]));

                case "rollback":
                    return AllClone[int.Parse(commands[1]) - 1].Rollback();

                case "relearn":
                    return AllClone[int.Parse(commands[1]) - 1].Relearn();

                case "clone":
                    return Clone(int.Parse(commands[1]) - 1);

                default:
                    return AllClone[int.Parse(commands[1]) - 1].Check();
            }
        }
	}

    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Previous { get; set; }

        public StackItem(T value)
        {
            Value = value;
            Previous = null;
        }

        public StackItem(StackItem<T> itemClone)
        {
            this.Value = itemClone.Value;
            this.Previous = itemClone.Previous;
        }
    }

    public class Stack<T>
    {
        StackItem<T> up;

        public Stack(Stack<T> cloneStack)
        {
            if (cloneStack.up != null)
            {
                this.up = new StackItem<T>(cloneStack.up);
            }
        }

        public Stack()
        {
            up = null;
        }

        public void Push(T value)
        {
            if (up == null)
                up = new StackItem<T>(value);
            else
            {
                var item = new StackItem<T>(value);
                item.Previous = up;
                up = item;
            }
        }

        public T Pop()
        {
            if (up == null) throw new InvalidOperationException();
            var result = up.Value;
            up = up.Previous;
            return result;
        }

        public T Check()
        {
            if (up == null) throw new InvalidOperationException();
            return up.Value;
        }
    }
}