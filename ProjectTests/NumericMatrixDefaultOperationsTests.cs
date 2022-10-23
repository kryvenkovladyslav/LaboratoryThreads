using LaboratoryThreads;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectTests
{
    public sealed class NumericMatrixDefaultOperationsTests
    {
        [Fact]
        public void Numeric_Matrix_Cad_Add_Another_Matrix()
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

            var third = first + second;

            var result = new double[,]
            {
                {2, 4, 6, 8 },
                {2, 4, 6, 8 },
                {2, 4, 6, 8 }
            };


            var expected = true;
            var actual = true;
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (result[i,j] != third[i,j])
                    {
                        actual = false;
                        break;
                    }
                }
            }


            Assert.Equal(expected, actual);
        }
    }
}
