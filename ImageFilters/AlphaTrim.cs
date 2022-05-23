using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class AlphaTrim
    {
        public static byte[,] alphaTrim_Counting(byte[,] ImageMatrix, int windowSize, int trimValue)//O(n*m^3) or O(n^3 *m)
        {

            byte[,] result = new byte[ImageMatrix.GetLength(0), ImageMatrix.GetLength(1)];//O(1)
            int limit = windowSize / 2;//O(1)
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)     //O(rows)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)     //O(columns)
                {
                    //__________GETTING WINDOW____________
                    byte[] window = new byte[windowSize * windowSize];     //O(1)
                    int window_index = 0; //O(1)

                    for (int ii = i - limit; ii <= i + limit; ii++)   // O(windowSize)
                    {

                        if (ii < 0 || ii >= ImageMatrix.GetLength(0)) //O(1)
                        {

                            continue; //O(1)
                        }

                        for (int jj = j - limit; jj <= j + limit; jj++)  //O(windowSize)
                        {
                            if (jj < 0 || jj >= ImageMatrix.GetLength(1)) //O(1)
                            {

                                continue; //O(1)
                            }

                            window[window_index] = ImageMatrix[ii, jj]; //O(1)
                            window_index++; //O(1)
                        }
                    }

                    //________SORTING WINDOW USING COUNT SORT_________

                    Sorting.CountingSort(window, window.Length);   //O(n+k)

                    //___________TRIM & CALC AVERAGE__________
                    int sum = 0; //O(1)
                    for (int x = trimValue; x < window.Length - trimValue; x++)//O(windowSize)^2    omega(1)
                    {
                        sum += window[x];   //O(1)
                    }
                    byte avg = (byte)(sum / (window.Length - (trimValue * 2)));//O(1)
                    //_________UPDATE RESULT MATRIX___________
                    result[i, j] = avg;//O(1)
                }
            }
            return result;//O(1)
        }


        public static byte[,] alphaTrim_KTH(byte[,] ImageMatrix, int windowSize, int trimValue)
        {

            byte[,] result = new byte[ImageMatrix.GetLength(0), ImageMatrix.GetLength(1)];   //O(1)
            int k = trimValue;     //O(1)
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)           //O(rows)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)           //O(columns)
                {

                    int Sum = 0;  //O(1)
                    int counter = 0;    //O(1)
                    byte[] window = new byte[windowSize * windowSize];   //O(1)
                    int window_index = 0;  //O(1)

                    for (int ii = i - (windowSize / 2); ii <= i + (windowSize / 2); ii++)  // O(windowSize)
                    {

                        if (ii < 0 || ii >= ImageMatrix.GetLength(0))   //O(1)
                        {

                            continue;  //O(1)
                        }

                        for (int jj = j - (windowSize / 2); jj <= j + (windowSize / 2); jj++) // O(windowSize)
                        {
                            if (jj < 0 || jj >= ImageMatrix.GetLength(1))   //O(1)
                            {

                                continue;  //O(1)
                            }

                            window[window_index] = ImageMatrix[ii, jj];  //O(1)
                            window_index++;  //O(1)
                        }
                    }

                    //___Kth element____


                    int Largest = Sorting.KthElement(window, window.Length, k);  //O(N + K LOG N)
                    int kk = window.Length - (k - 1);  //O(1)
                    int Smallest = Sorting.KthElement(window, window.Length, kk);   //O(N + K LOG N)


                    for (int y = 0; y < window.Length; y++) //O(window.length)
                    {
                        if (window[y] < Largest && window[y] > Smallest)  //O(1)
                        {

                            Sum += window[y];  //O(1)
                            counter++;   //O(1)

                        }

                    }
                    if (counter == 0)  //O(1)
                        counter = 1; //O(1)

                    byte mean = (byte)(Sum / counter); //O(1)
                   result[i, j] = mean;  //O(1)
                }
            }

            return result;  //O(1)
        }

    }
}