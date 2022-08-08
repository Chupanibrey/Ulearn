namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			Move(robot, width, Direction.Right);
			Move(robot, width, Direction.Down);
			Move(robot, width, Direction.Left);

			if (robot.Finished == false)
			{
				Move(robot, width, Direction.Down);
				MoveOut(robot, width, height);
			}
		}

		public static void Move(Robot robot, int step, Direction side)
		{
			int wall = 3;
			if (side == Direction.Down) step = 5;
			for (int i = 0; i < step - wall; i++) 
				robot.MoveTo(side);
		}
	}
}