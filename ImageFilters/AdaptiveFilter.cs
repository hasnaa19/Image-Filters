using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class AdaptiveFilter
    {
        public static byte[,] filter(byte[,] ImageMatrix, int Ws, string c)
        {
            byte[] a; //O(1)
            byte Zmed;  //O(1)
            byte Zmin;  //O(1)
            byte Zmax;  //O(1)
            byte A1;  //O(1)
            byte A2;  //O(1)
            byte B1;  //O(1)
            byte B2;  //O(1)
            int windowsize;  //O(1)
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)//O(row)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)//O(columns)
                {
                    windowsize = 3;//O(1)

                    while (true)
                    {
                        var arlist1 = new ArrayList();//O(1)
                        for (int ii = i - (windowsize / 2); ii <= i + (windowsize / 2); ii++)//O(windowsize)
                        {

                            if (ii < 0 || ii >= ImageMatrix.GetLength(0))//O(1)
                            {

                                continue;//O(1)
                            }

                            for (int jj = j - (windowsize / 2); jj <= j + (windowsize / 2); jj++)//O(windowsize)
                            {
                                if (jj < 0 || jj >= ImageMatrix.GetLength(1))//O(1)
                                {

                                    continue;//O(1)
                                }

                                arlist1.Add(ImageMatrix[ii, jj]);//O(1)

                            }

                        }
                        a = new byte[arlist1.Count];//O(1)
                        for (int k = 0; k < arlist1.Count; k++)//O(arlist1.Count)
                        {
                            a[k] = (Byte)arlist1[k];//O(1)
                        }

                        if(c == "Counting Sort")//O(1)
                        {
                            a = Sorting.CountingSort(a, a.Length);//O(n + k)
                        }
                        else if (c == "Quick Sort")//O(1)
                        {
                            a = Sorting.quicksort(a, 0, a.Length - 1);//O(N^2)
                        }

                        if(a.Length % 2 == 0 )//O(1)
                        {
                            int x = (a[(a.Length / 2) - 1] + a[(a.Length / 2)]) / 2;  //O(1)
                            Zmed = (byte)x;    //O(1)
                        }
                        else
                        {
                            Zmed = a[(a.Length / 2)];  //O(1)
                        }

                        Zmin = a[0];    //O(1)
                        Zmax = a[a.Length - 1];   //O(1)
                        A1 = (byte)(Zmed - Zmin);   //O(1)
                        A2 = (byte)(Zmax - Zmed);   //O(1)

                        if (A1 <= 0 || A2 <= 0)  //O(1)
                        {
                            windowsize += 2;   //O(1)
                            if (windowsize > Ws)   //O(1)
                            {
                                ImageMatrix[i, j] = Zmed;  //O(1)
                                break;  
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            B1 = (byte)(ImageMatrix[i, j] - Zmin);  //O(1)
                            B2 = (byte)(Zmax - ImageMatrix[i, j]);   //O(1)
                            if (B1 <= 0 || B2 <= 0)  //O(1)
                            {
                                ImageMatrix[i, j] = Zmed;  //O(1)
                            }
                            break;  
                        }

                    }

                }

            }
            return ImageMatrix;  //O(1)
        }
    }
}
