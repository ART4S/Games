using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    class Heap<TKey, TValue>
    {
        private readonly List<TKey> keys;
        private readonly List<TValue> heap;
        private readonly Comparer<TValue> comparer;

        public int Count { get { return heap.Count; } }

        public Heap()
        {
            keys = new List<TKey>();
            heap = new List<TValue>();
            comparer = Comparer<TValue>.Default;
        }

        public bool Any()
        {
            return heap.Any();
        }

        public void Add(TKey key, TValue value)
        {
            heap.Add(value);
            keys.Add(key);  
             
            ShiftUp(Count - 1);
        }

        public TKey Pop()
        {
            TKey result = keys[0];

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

            if (leftChild < Count && comparer.Compare(heap[i], heap[leftChild]) > 0)
            {
                Swap(i, leftChild);
                ShiftDown(leftChild);
            }

            if (rightChild < Count && comparer.Compare(heap[i], heap[rightChild]) > 0)
            {
                Swap(i, rightChild);
                ShiftDown(rightChild);
            }
        }

        private void ShiftUp(int i)
        {
            int parent = (i - 1) / 2;

            if (i > 0 && comparer.Compare(heap[i], heap[parent]) < 0)
            {
                Swap(i, parent);
                ShiftUp(parent);
            }
        }

        private void Swap(int i, int j)
        {
            TValue tempHeap = heap[i];

            heap[i] = heap[j];
            heap[j] = tempHeap;

            TKey tempKeys = keys[i];

            keys[i] = keys[j];
            keys[j] = tempKeys;
        }
    }
}
