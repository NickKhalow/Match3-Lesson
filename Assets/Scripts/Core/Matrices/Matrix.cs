#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Matrices
{

    public readonly struct Matrix<T> : IReadOnlyMatrix<T>
    {
        private readonly T[,] matrix;

        public int Width => matrix.GetLength(0);

        public int Height => matrix.GetLength(1);

        public Matrix(T[,] matrix)
        {
            this.matrix = matrix;
        }

        public static Matrix<T> From<TK>(Matrix<TK> other, Func<TK, T> converter)
        {
            var defWidth = other.Width;
            var defHeight = other.Height;
            var matrix = new T[defWidth, defHeight];
            for (int y = 0; y < defHeight; y++)
            {
                for (int x = 0; x < defWidth; x++)
                {
                    matrix[x, y] = converter(other.GetAt(x, y));
                }
            }

            return new Matrix<T>(matrix);
        }

        public static Matrix<T> From<TK>(TK[,] other, Func<TK, T> converter)
        {
            return From(new Matrix<TK>(other), converter);
        }

        public T GetAt(int x, int y)
        {
            return matrix[x, y];
        }

        public void SetAt(int x, int y, T value)
        {
            matrix[x, y] = value;
        }

        public T[,] AsRaw()
        {
            return matrix;
        }

        public IEnumerator<(int x, int y, T value)> GetEnumerator()
        {
            var defWidth = matrix.GetLength(0);
            var defHeight = matrix.GetLength(1);
            for (int y = 0; y < defHeight; y++)
            {
                for (int x = 0; x < defWidth; x++)
                {
                    yield return (x, y, matrix[x, y]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}