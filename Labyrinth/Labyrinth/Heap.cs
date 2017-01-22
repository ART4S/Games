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

        private void ShiftDown(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;

            if (leftChildIndex < Count && comparer.Compare(heap[index], heap[leftChildIndex]) > 0)
            {
                Swap(index, leftChildIndex);
                ShiftDown(leftChildIndex);
            }

            if (rightChildIndex < Count && comparer.Compare(heap[index], heap[rightChildIndex]) > 0)
            {
                Swap(index, rightChildIndex);
                ShiftDown(rightChildIndex);
            }
        }

        private void ShiftUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            if (index > 0 && comparer.Compare(heap[index], heap[parentIndex]) < 0)
            {
                Swap(index, parentIndex);
                ShiftUp(parentIndex);
            }
        }

        private void Swap(int leftIndex, int rightIndex)
        {
            TValue tempHeap = heap[leftIndex];

            heap[leftIndex] = heap[rightIndex];
            heap[rightIndex] = tempHeap;

            TKey tempKeys = keys[leftIndex];

            keys[leftIndex] = keys[rightIndex];
            keys[rightIndex] = tempKeys;
        }
    }
}
