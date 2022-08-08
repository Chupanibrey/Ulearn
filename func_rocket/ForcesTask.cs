using System;
using System.Drawing;

namespace func_rocket
{
	public class ForcesTask
	{
		public static RocketForce GetThrustForce(double forceValue)
		{
			return r => new Vector(Math.Cos(r.Direction) * forceValue, Math.Sin(r.Direction) * forceValue);
		}

		public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
		{
			return r => gravity(spaceSize, r.Location);
		}

		public static RocketForce Sum(params RocketForce[] forces)
		{
			return r =>
			{
				var resForce = Vector.Zero;
				foreach (var f in forces)
					resForce += f(r);
				return resForce;
			};
		}
	}
}