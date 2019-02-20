using System;
					
public class Program
{
	
	static string matrixA = "1 2 3, 4 5 6, 7 8 9";
        static string matrixB = "2 4 6, 8 1 3, 5 7 9";

        static int[,] mxArA;
        static int[,] mxArB;



        public static void Main()
        {
            Console.WriteLine("Hello World\n");

             matrixA = "0 2 1, 0 1 -1 , 1 0 1";
            matrixB = "2 1 -1, -1 0 1, 0 -2 0";

            // matrixA = "4 -2, -3 1";
            // matrixB = "7, 5";

            mxArA = stringTo2DArray(matrixA);
            Print2DArray(mxArA);
            Console.WriteLine("");
            /*
            mxArB = stringTo2DArray(matrixB);
            Print2DArray(mxArB);
            Console.WriteLine("");
            */
            /*
            int[,] result = multiplyMatrices(mxArA, mxArB);

            Console.WriteLine("");
            Print2DArray(result);
            */
            // Console.WriteLine("\ndeterminant: {0}\n", GetDeterminant2by2(mxArA));
            Console.WriteLine("\ndeterminant: {0}\n", GetDeterminantXbyX(mxArA));

            // Print2DArray(GetMatrixOfInverse2by2(mxArA));
        }


        static int[,] GetMatrixOfInverse2by2(int[,] mxAr)
        {
            int[,] mx2 = new int[2, 2];

            mx2[0, 0] = mxAr[1, 1];
            mx2[1, 1] = mxAr[0, 0];
            mx2[0, 1] = mxAr[0, 1] * -1;
            mx2[1, 0] = mxAr[1, 0] * -1;

            return mx2;
        }

        static int GetDeterminantXbyX(int[,] mxAr)
        {
            // if not square return null
            if (mxAr.GetLength(0) != mxAr.GetLength(1))
            {
                return -1;
            }

            if ( mxAr.GetLength(1) == 2 )
            {
                return GetDeterminant2by2(mxAr);
            }

            // matrix is greater than 2 x 2
            // go through top row 
            // get smaller matrix for each
            // get det of each smaller matrix
            // add on to total if even, subtract from total if odd
            int totalDet = 0;
            for (int i = 0; i < mxAr.GetLength(0); i++)
            {
                int[,] mx1 = GetSmallerMatrix(mxAr, i);
                int detTemp = GetDeterminantXbyX(mx1);
                int detTemp2 = detTemp * i;

                if ( i % 2 == 0 )
                {
                    Console.WriteLine( "add  :{0}", detTemp);
                    totalDet = totalDet + detTemp2;
                }
                else
                {
                    Console.WriteLine("minus :{0}", detTemp);
                    totalDet = totalDet - detTemp2;
                }
            }

            return totalDet;


        }

        static int[,] GetSmallerMatrix( int[,] mx, int rowPos )
        {
            Console.WriteLine("small {0} ", rowPos);
            int[,] mx2 = new int[mx.GetLength(0)-1, mx.GetLength(1) - 1 ];

            for (int i = 1; i < mx.GetLength(0)  ; i++)
            {
                int offSet = 0;
                for (int j = 0; j < mx.GetLength(0) ; j++)
                {
                    if(j != rowPos )
                    {
                        mx2[i - 1, j - offSet] = mx[i, j];
                    }
                    else
                    {
                        offSet = 1;
                    }
                }
            }
            Print2DArray(mx2);
            Console.WriteLine("");
            return mx2;
        }

        static int GetDeterminant2by2(int[,] mxAr)
        {
            if (mxAr.GetLength(0) != 2 && mxAr.GetLength(1) != 2)
            {
                return -1;
            }

            int x1 = mxAr[0, 0] * mxAr[1, 1];
            int x2 = mxAr[0, 1] * mxAr[1, 0];
            int x3 = x1 - x2;
            return x3;
        }


        static int[,] multiplyMatrices(int[,] mxA, int[,] mxB)
        {
            if (mxA.GetLength(1) != mxB.GetLength(0))
            {
                return null;
            }

            int[,] result = new int[mxA.GetLength(0), mxB.GetLength(1)];
            for (int i = 0; i < mxA.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    int multiplied = 0;
                    for (int k = 0; k < mxA.GetLength(1); k++)
                    {
                        Console.Write("( ");
                        Console.Write(mxA[i, k]);
                        Console.Write(" * ");

                        Console.Write(mxB[k, j]);

                        Console.Write(" )");
                        if (k != mxA.GetLength(1) - 1)
                        {
                            Console.Write("  +  ");
                        }
                        multiplied = multiplied + (mxA[i, k] * mxB[k, j]);
                    }
                    Console.Write(" = ");
                    Console.Write(multiplied);
                    Console.Write("\n");

                    result[i, j] = multiplied;
                }
            }

            return result;
        }


        static int[,] stringTo2DArray(string str1)
        {
            int[,] mx2dArA = null;
            string[] rows = str1.Split(',');

            for (int i = 0; i < rows.Length; i++)
            {
                int[] row = RowToArray(rows[i]);
                if (i == 0)
                {
                    mx2dArA = new int[rows.Length, row.Length];
                }
                else
                {
                    if (row.Length != mx2dArA.GetLength(1))
                    {
                        return null;
                    }
                }

                for (int j = 0; j < row.Length; j++)
                {
                    mx2dArA[i, j] = row[j];
                }
            }

            return mx2dArA;
        }


        static int[] RowToArray(string str1)
        {
            string[] rowsAsString = str1.Trim().Split(' ');
            int[] RowAsInts = new int[rowsAsString.Length];

            for (int i = 0; i < RowAsInts.Length; i++)
            {
                RowAsInts[i] = int.Parse(rowsAsString[i].Trim());
            }
            return RowAsInts;
        }


        static void Print2DArray(int[,] arr2D)
        {
            if (arr2D == null)
            {
                Console.Write("no array");
            }
            else
            {
                for (int i = 0; i < arr2D.GetLength(0); i++)
                {
                    for (int j = 0; j < arr2D.GetLength(1); j++)
                    {
                        Console.Write((arr2D[i, j] + "").PadLeft(6));
                    }
                    Console.WriteLine();
                }
            }
        }
}
