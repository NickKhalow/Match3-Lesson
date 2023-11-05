#nullable enable

using Core.Boards;
using Core.ResolveResults;

namespace Core.ResolvePipe
{
    public interface IResolvePipe
    {
        void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult);
    }
}