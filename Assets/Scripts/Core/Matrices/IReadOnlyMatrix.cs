using System.Collections.Generic;

namespace Core.Matrices
{
    public interface IReadOnlyMatrix<T> : IEnumerable<(int x, int y, T value)>
    {

        int Width { get; }

        int Height { get; }

        T GetAt(int x, int y);

        public T[,] AsRaw();
    }
}