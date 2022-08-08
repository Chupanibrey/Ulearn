namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			for (int i = 0; robot.Finished != true; i++)
			{
				Step(robot, width, height, i);
			}
        }

        public static void Step(Robot robot, int width, int height, int i)
		{
			int wall = 3;
			if (i < width - wall) robot.MoveTo(Direction.Right);
			if (i < height - wall) robot.MoveTo(Direction.Down);
		}
	}
}