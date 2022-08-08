using System;
using System.Text;

namespace hashes
{
	public class GhostsTask :
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
		IMagic
	{
		Vector vector = new Vector(2, 3);
		Segment segment = new Segment(new Vector(0, 0), new Vector(2, 3));
		Cat cat = new Cat("kuzya", "simple", new DateTime(2017, 05, 23));
		Robot robot = new Robot("007");
		private byte[] b = new byte[] { 0, 1, 2, 3 };
		Document document;

		public void DoMagic()
		{
			vector.Add(new Vector(1, 1));
			segment.End.Add(new Vector(1, 1));
			cat.Rename("break");
			Robot.BatteryCapacity += 100;
			b[0] = 10;
		}

		public GhostsTask()
        {
			document = new Document("book", Encoding.UTF8, b);
		}

		Vector IFactory<Vector>.Create()
		{
			return vector;
		}

		Segment IFactory<Segment>.Create()
		{
			return segment;
		}

		Document IFactory<Document>.Create()
		{
			return document;
		}

		Cat IFactory<Cat>.Create()
		{
			return cat;
		}

		Robot IFactory<Robot>.Create()
		{
			return robot;
		}
	}
}