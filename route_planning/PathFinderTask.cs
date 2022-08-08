using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var shortDistance = double.MaxValue;
			var allOrder = new int[checkpoints.Length];
			var shortOrder = new int[checkpoints.Length];
			MakePermutations(checkpoints, allOrder, 1, ref shortOrder, ref shortDistance);
			return shortOrder;
		}

		private static int[] MakePermutations(Point[] checkpoints, int[] allOrder, int position,
            ref int[] shortOrder, ref double shortDistance)
        {
            var currentOrder = new int[position];
            Array.Copy(allOrder, currentOrder, position);
            var pathLength = PointExtensions.GetPathLength(checkpoints, currentOrder);

            if (pathLength < shortDistance)
            {
                if (position == allOrder.Length)
                {
                    shortDistance = pathLength;
                    shortOrder = (int[])allOrder.Clone();
                    return allOrder;
                }


                for (int i = 1; i < allOrder.Length; i++)
                {
                    var index = Array.IndexOf(allOrder, i, 0, position);
                    if (index != -1)
                        continue;
                    allOrder[position] = i;
                    MakePermutations(checkpoints, allOrder, position + 1, ref shortOrder, ref shortDistance);
                }
            }

            return allOrder;
        }
    }
}