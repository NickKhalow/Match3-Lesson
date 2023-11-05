#nullable enable

using System;
using System.Linq;
using Core.Boards;
using Core.ResolveResults;

namespace Core.ResolvePipe.SpecialAbilities
{
    public class LineBoomResolvePipe : IResolvePipe
    {

        private readonly Func<(PiecePosition piece, int x, int y), bool> lineSelector;

        public LineBoomResolvePipe(Func<(PiecePosition piece, int x, int y), bool> lineSelector)
        {
            this.lineSelector = lineSelector;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            foreach (var piecePosition in board.Where(e => lineSelector((new PiecePosition(e), x, y))))
            {
                board.RemovePieceAt(piecePosition.x, piecePosition.y);
                resolveResult.ApplyChange(
                    piecePosition.value.Piece,
                    new ChangeInfo.Resolved(new PiecePosition(piecePosition))
                );
            }
        }
    }
}