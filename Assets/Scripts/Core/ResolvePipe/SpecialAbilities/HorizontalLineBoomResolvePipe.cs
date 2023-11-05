#nullable enable

using Core.Boards;
using Core.ResolveResults;

namespace Core.ResolvePipe.SpecialAbilities
{

    public class HorizontalLineBoomResolvePipe : IResolvePipe
    {

        private readonly LineBoomResolvePipe pipe;

        public HorizontalLineBoomResolvePipe()
        {
            pipe = new LineBoomResolvePipe(e => e.piece.Pos.Y == e.y);
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            pipe.ResolveAt(x, y, board, resolveResult);
        }
    }
}