using LaboratoryThreads;
using System;
using Xunit;

namespace ProjectTests
{
    public sealed class NumericMatrixDefaultOperationsTests
    {
        [Fact]
        public void NumericMatrix_Cad_Add_Another_Matrix()
        {
            NumericMatrix first = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {1, 2, 3, 4 },
                {1, 2, 3, 4 }
            });

            NumericMatrix second = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {1, 2, 3, 4 },
                {1, 2, 3, 4 }
            });

            NumericMatrix expectedResult = new NumericMatrix(new double[,]
            {
                {2, 4, 6, 8 },
                {2, 4, 6, 8 },
                {2, 4, 6, 8 }
            });

            var actualResult = first + second;


            var expected = true;
            var actual = true;
            for (int i = 0; i < expectedResult.Rows; i++)
            {
                for (int j = 0; j < expectedResult.Columns; j++)
                {
                    if (expectedResult[i,j] != actualResult[i,j])
                    {
                        actual = false;
                        break;
                    }
                }
            }


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NumericMatrix_Cad_Substract_Another_Matrix()
        {
            NumericMatrix first = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {1, 2, 3, 4 },
                {1, 2, 3, 4 }
            });

            NumericMatrix second = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {1, 2, 3, 4 },
                {1, 2, 3, 4 }
            });

            NumericMatrix expectedResult = new NumericMatrix(new double[,]
            {
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 }
            });

            var actualResult = first - second;


            var expected = true;
            var actual = true;
            for (int i = 0; i < expectedResult.Rows; i++)
            {
                for (int j = 0; j < expectedResult.Columns; j++)
                {
                    if (expectedResult[i, j] != actualResult[i, j])
                    {
                        actual = false;
                        break;
                    }
                }
            }


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NumericMatrix_Can_Multiply_Another_Matrix_With_Default_Method()
        {
            NumericMatrix first = new NumericMatrix(new double[,] 
            {
                {1, 2, 3, 4, 5, 6, 7 },
                {11, 12, 13, 14, 15, 16, 17 },
                {21, 22, 23, 24, 25, 26, 27 },
                {31, 32, 33, 34, 35, 36, 37 },
                {41, 42, 43, 44, 45, 46, 47 },
            });

            NumericMatrix second = new NumericMatrix(new double[,] 
            {
                {1 },
                {2 },
                {3 },
                {4 },
                {5 },
                {6 },
                {7 },
            });

            NumericMatrix expectedResult = new NumericMatrix(new double[,]
            {
                {140 },
                {420 },
                {700 },
                {980 },
                {1260 }
            });

            var actualResult = first * second;

            var actual = true;
            var expected = true;
            for (int i = 0; i < expectedResult.Rows; i++)
            {
                for (int j = 0; j < expectedResult.Columns; j++)
                {
                    if (actualResult[i, j] != expectedResult[i,j])
                    {
                        actual = true;
                    }
                }
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NumericMatrix_With_Different_Dimension_Of_Columns_Can_Throw_MatrixDimensionException()
        {
            NumericMatrix first = new NumericMatrix(new double[,] 
            {
                {1, 2, 3, 4 },
                {5, 6, 7, 8 },
                {9, 10, 11, 12 },
                {13, 14, 15 ,16 }
            });

            NumericMatrix second = new NumericMatrix(new double[,] 
            {
                {1, 2, 3 },
                {4, 5, 6 },
                {7, 8, 9},
                {10, 11 ,12 }
            });

            Type expected = typeof(MatrixDimensionException);
            Exception exception = null;
            try
            {
                var result = first + second;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(expected, exception.GetType());
        }

        [Fact]
        public void NumericMatrix_With_Different_Dimension_Of_Rows_Can_Throw_MatrixDimensionException()
        {
            NumericMatrix first = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {5, 6, 7, 8 },
                {9, 10, 11, 12 },
                {13, 14, 15 ,16 }
            });

            NumericMatrix second = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4 },
                {4, 5, 6, 4 },
                {7, 8, 9, 4},
                {10, 11, 12, 4},
                {1, 2, 3, 5 }
            });

            Type expected = typeof(MatrixDimensionException);
            Exception exception = null;
            try
            {
                var result = first + second;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(expected, exception.GetType());
        }

        [Fact]
        public void NumericMatrix_Can_Throw_MatrixDimensionException_With_Muptiplication_When_Rows_Dont_Match_Columns()
        {
            NumericMatrix first = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4, 5, 6 },
                {11, 12, 13, 14, 15, 16 },
                {21, 22, 23, 24, 25, 26 },
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46 },
            });

            NumericMatrix second = new NumericMatrix(new double[,]
            {
                {1 },
                {2 },
                {3 },
                {4 },
                {5 },
                {6 },
                {7 }
            });

            Type expected = typeof(MatrixDimensionException);
            Exception exception = null;
            try
            {
                var result = first * second;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(expected, exception.GetType());
        }

        [Fact]
        public void NumericMatrix_Can_Throw_MatrixDimensionException_With_Muptiplication_When_Coulmns_Dont_Match_Rows()
        {
            NumericMatrix first = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4, 5, 6, 7 },
                {11, 12, 13, 14, 15, 16, 17 },
                {21, 22, 23, 24, 25, 26, 27 },
                {31, 32, 33, 34, 35, 36, 37 },
                {41, 42, 43, 44, 45, 46, 47 },
            });

            NumericMatrix second = new NumericMatrix(new double[,]
            {
                {1 },
                {2 },
                {3 },
                {4 },
                {5 },
                {6 },
            });

            Type expected = typeof(MatrixDimensionException);
            Exception exception = null;
            try
            {
                var result = first * second;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(expected, exception.GetType());
        }

        [Fact]
        public void NumericMatrix_Can_Transorm_Itself_To_Array()
        {
            double[,] array = new double[,]
            {
                {1, 2, 3, 4 },
                {5, 6, 7, 8 },
                {9, 10, 11, 12 },
                {13, 14, 15 ,16 }
            };

            NumericMatrix numericMatrix = new NumericMatrix(array);
            var transformed = numericMatrix.ToArray();

            var expectedType = array.GetType();
            var expectedArray = true;
            var actualType = transformed.GetType();
            var actualArray = true;

            for (int i = 0; i < transformed.GetLength(0); i++)
            {
                for (int j = 0; j < transformed.GetLength(1); j++)
                {
                    if (transformed[i,j] != array[i,j])
                    {
                        actualArray = false;
                    }
                }
            }

            Assert.Equal(actualArray && expectedArray, actualType.Equals(expectedType));
        }
    }
}
