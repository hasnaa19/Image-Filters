using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class MaxHeap
    {
        public byte[] arr; //heap array will be used to build complete binary tree/heap
        public int capacity; // maximum size 
        public int heap_size; // current number of elements

        //-------------------------------------------------------------------------------------------------

        //                                     Class Cuntructor
        public MaxHeap(byte[] a, int size) //build "heap" == "complete binary tree" , then arranges it by heapify
        {
            arr = a;
            heap_size = size;
            for (int i = (heap_size - 1) / 2; i >= 0; i--) // O(n-1/2)
                maxHeapify(i); // O(log n)

        } //  O(n/2 * log(n)) = O(n)
          //-------------------------------------------------------------------------------------------------
        public int Parent(int i) { return (i - 1) / 2; } //O (1)
        public int leftChild(int i) { return (2 * i + 1); } //O (1)
        public int rightChild(int i) { return (2 * i + 2); } //O (1)

        //                             e.g.   i= 0 is parent of i=1 and i=2 
        //                                     parent(2)= (2-1)/2 = 0
        //-------------------------------------------------------------------------------------------------

        public byte topMax() // Returns maximum element which is root of the tree/ super parent
        {
            return arr[0];
        } //O (1)

        //-------------------------------------------------------------------------------------------------
        public void maxHeapify(int pos)// checks if parent > left & right child in a specifc position  
        {
            int left = leftChild(pos);
            int right = rightChild(pos);
            int max = pos;
            if (left < heap_size && arr[left] > arr[pos])
                max = left;
            if (right < heap_size && arr[right] > arr[max])
                max = right;
            if (max != pos)
            {
                byte temp = arr[pos];
                arr[pos] = arr[max];
                arr[max] = temp;
                maxHeapify(max);  // O (Log n)
            }

        } // O (Log n)
          //                    worst case : position = zero aka super parent/ root
          //                    heapify checks on each parent node of 2 childs thus it checks on 
          //                     number of nodes/elements [n] divided by 2 i.e. O (Log n)
          //                      which is height of the tree or number of branches/levels.
          //-------------------------------------------------------------------------------------------------
        public int popMax() // get and remove root
        {
            if (heap_size == 0) //O (1)
                return -1; //indicates failure

            int root = arr[0];
            if (heap_size > 1)
            {
                arr[0] = arr[heap_size - 1];
                maxHeapify(0);// O( log n)

            }
            heap_size--;
            return root;

        } // O(log n)

        //-------------------------------------------------------------------------------------------------
    }
}