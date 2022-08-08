using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
	public class ReadonlyBytes: IEnumerable<byte>
    {
        byte[] Bytes { get; }
        public int Length { get; }
        int hash = -1;

        public ReadonlyBytes(params byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException();
            else
            {
                Bytes = new byte[bytes.Length];
                bytes.CopyTo(Bytes, 0);
                Length = Bytes.Length;
            }
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return Bytes[index];
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes b) || 
                obj.GetType() != typeof(ReadonlyBytes) || 
                Bytes.Length != b.Bytes.Length) return false;

            for(int i = 0; i < Bytes.Length; i++)
                if (Bytes[i] != b.Bytes[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (hash != -1) return hash;

                hash = 0;

                for (int i = 0; i < Length; i++)
                {
                    hash *= 1023;
                    hash += Bytes[i];
                }

                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", Bytes));
        }

        public IEnumerator<byte> GetEnumerator()
        {
            foreach(var b in Bytes)
                yield return b;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}