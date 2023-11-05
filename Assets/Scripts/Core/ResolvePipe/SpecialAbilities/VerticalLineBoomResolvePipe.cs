#nullable enable

using Core.Boards;
using Core.ResolveResults;

namespace Core.ResolvePipe.SpecialAbilities
{

    public class VerticalLineBoomResolvePipe : IResolvePipe
    {

        private readonly LineBoomResolvePipe pipe;

        public VerticalLineBoomResolvePipe()
        {
            pipe = new LineBoomResolvePipe(e => e.piece.Pos.X == e.x);
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            pipe.ResolveAt(x, y, board, resolveResult);
        }
    }
}