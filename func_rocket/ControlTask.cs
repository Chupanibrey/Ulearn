using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var d = target - rocket.Location;
			double resAngle;

			if (Math.Abs(d.Angle - rocket.Direction) < 0.5
				|| Math.Abs(d.Angle - rocket.Velocity.Angle) < 0.5)
				resAngle = ((d.Angle - rocket.Direction) + (d.Angle - rocket.Velocity.Angle)) / 2;
			else
				resAngle = d.Angle - rocket.Direction;

			return resAngle < 0 ? Turn.Left : (resAngle > 0 ? Turn.Right : Turn.None);	
		}
	}
}