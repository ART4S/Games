using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    class Heap
    {
        private readonly List<int> heap;
        private readonly List<Point> keys;

        public int Count { get { return heap.Count; } }

        public Heap()
        {
            heap = new List<int>();
            keys = new List<Point>();
        }

        public bool Any()
        {
            return heap.Any();
        }

        public void Add(Point key, int value)
        {
            heap.Add(value);
            keys.Add(key);   
            ShiftUp(Count - 1);
        }

        public Point Pop()
        {
            Point result = keys[0];

            heap[0] = heap[Count - 1];
            keys[0] = keys[Count - 1];

            heap.RemoveAt(Count - 1);
            keys.RemoveAt(Count);

            ShiftDown(0);

            return result;
        }

        private void ShiftDown(int i)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;

            if (leftChild < Count && heap[i] > heap[leftChild])
            {
                Swap(i, leftChild);
                ShiftDown(leftChild);
            }

            if (rightChild < Count && heap[i] > heap[rightChild])
            {
                Swap(i, rightChild);
                ShiftDown(rightChild);
            }
        }

        private void ShiftUp(int i)
        {
            int parent = (i - 1) / 2;

            if (i > 0 && heap[i] < heap[parent])
            {
                Swap(i, parent);
                ShiftUp(parent);
            }
        }

        private void Swap(int i, int j)
        {
            int tempHeap = heap[i];

            heap[i] = heap[j];
            heap[j] = tempHeap;

            Point tempKeys = keys[i];

            keys[i] = keys[j];
            keys[j] = tempKeys;
        }
    }
}
