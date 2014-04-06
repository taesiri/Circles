namespace Assets.Scripts
{
    public class RingBehaviour<T>
    {
        private T[] _innerArray;

        public RingBehaviour(T[] array)
        {
            _innerArray = new T[array.Length];
        }
    }

    public class RingIndexHelper
    {
        private readonly int _length = -1;

        public RingIndexHelper(int lengthOfArray)
        {
            _length = lengthOfArray;
        }

        public int GetNext(int currentIndex)
        {
            return currentIndex + 1 < _length ? currentIndex + 1 : 0;
        }

        public int GetPrev(int currentIndex)
        {
            return currentIndex == 0 ? _length - 1 : currentIndex - 1;
        }

        public int[] GenerateFullCycle(int startIndex)
        {
            var indexArray = new int[_length];
            int lastIndex = startIndex;

            for (int i = 0; i < _length; i++)
            {
                indexArray[i] = lastIndex;
                lastIndex = GetNext(lastIndex);
            }


            return indexArray;
        }
    }
}