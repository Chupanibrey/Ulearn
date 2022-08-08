using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public static int X, Y = 0;
        public static int Dx, Dy = 0;
        public CreatureCommand Act(int x, int y)
        {
            X = x;
            Y = y;

            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Left:
                    Dy = 0;
                    Dx = -1;
                    break;

                case System.Windows.Forms.Keys.Up:
                    Dy = -1;
                    Dx = 0;
                    break;

                case System.Windows.Forms.Keys.Right:
                    Dy = 0;
                    Dx = 1;
                    break;

                case System.Windows.Forms.Keys.Down:
                    Dy = 1;
                    Dx = 0;
                    break;

                default:
                    Dy = 0;
                    Dx = 0;
                    break;
            }

            if (!(x + Dx >= 0 && x + Dx < Game.MapWidth &&
                y + Dy >= 0 && y + Dy < Game.MapHeight))
            {
                Dy = 0;
                Dx = 0;
            }

            if (Game.Map[x + Dx, y + Dy] != null)
            {
                if (Game.Map[x + Dx, y + Dy].ToString() == "Digger.Sack")
                {
                    Dy = 0;
                    Dx = 0;
                }
            }

            return new CreatureCommand() { DeltaX = Dx, DeltaY = Dy };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            if (neighbor == "Digger.Gold")
                Game.Scores += 10;
            if (neighbor == "Digger.Sack" ||
                neighbor == "Digger.Monster")
            {

                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        private int counter = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null ||
                    (counter > 0 && (map.ToString() == "Digger.Player" ||
                    map.ToString() == "Digger.Monster")))
                {
                    counter++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }

            if (counter > 1)
            {
                counter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            counter = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return (neighbor == "Digger.Player" ||
               neighbor == "Digger.Monster");
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {

        public CreatureCommand Act(int x, int y)
        {
            int dx = 0;
            int dy = 0;

            if (IsPlayerAlive())
            {
                if (Player.X == x)
                {
                    if (Player.Y < y) dy = -1;
                    else if (Player.Y > y) dy = 1;
                }

                else if (Player.Y == y)
                {
                    if (Player.X < x) dx = -1;
                    else if (Player.X > x) dx = 1;
                }
                else
                {
                    if (Player.X < x) dx = -1;
                    else if (Player.X > x) dx = 1;
                }
            }
            else return Stay();

            if (!(x + dx >= 0 && x + dx < Game.MapWidth &&
                y + dy >= 0 && y + dy < Game.MapHeight))
                return Stay();

            var map = Game.Map[x + dx, y + dy];
            if (map != null)
                if (map.ToString() == "Digger.Terrain" ||
                    map.ToString() == "Digger.Sack" ||
                    map.ToString() == "Digger.Monster")
                    return Stay();
            return new CreatureCommand() { DeltaX = dx, DeltaY = dy };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var neighbor = conflictedObject.ToString();
            return (neighbor == "Digger.Sack" ||
            neighbor == "Digger.Monster");
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        static private CreatureCommand Stay()
        {

            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        static private bool IsPlayerAlive()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    var map = Game.Map[i, j];
                    if (map != null)
                    {
                        if (map.ToString() == "Digger.Player")
                        {
                            Player.X = i;
                            Player.Y = j;
                            return true;
                        }
                    }
                }
            return false;
        }
    }
}