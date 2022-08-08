namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static int Wall = 3;
		public static int AspectRatio;

		public static void MoveOut(Robot robot, int width, int height)
		{
			if (width>height && robot.Finished == false)
            {
				AspectRatio = (width - Wall) / (height - Wall);
				Move(robot, AspectRatio, Direction.Right, Direction.Down);
				MoveOut(robot, width, height);
			}
			if (width < height && robot.Finished == false)
			{
				AspectRatio = (height - Wall) / (width - Wall);
				Move(robot, AspectRatio, Direction.Down, Direction.Right);
				MoveOut(robot, width, height);
			}
		}

		public static void Move(Robot robot, int aspectRatio, Direction dfirst, Direction dsecond)
		{
			if (robot.Finished == false)
				for (int i = 0; i < aspectRatio; i++)
					robot.MoveTo(dfirst);
			if(robot.Finished == false)
				robot.MoveTo(dsecond);
		}
	}
}