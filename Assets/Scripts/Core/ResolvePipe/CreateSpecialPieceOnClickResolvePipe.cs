#nullable enable

using System.Linq;
using Core.Boards;
using Core.PieceSpawners;
using Core.ResolveResults;
using Core.ValueObjects;

namespace Core.ResolvePipe
{

    public class CreateSpecialPieceOnClickResolvePipe : IResolvePipe
    {
        private readonly IPieceSpawner pieceSpawner;
        private readonly int minimalResolvedCount;

        public CreateSpecialPieceOnClickResolvePipe(IPieceSpawner pieceSpawner, int minimalResolvedCount = 5)
        {
            this.pieceSpawner = pieceSpawner;
            this.minimalResolvedCount = minimalResolvedCount;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            if (resolveResult.OfType<ChangeInfo.Resolved>().Count() >= minimalResolvedCount)
            {
                var newPiece = new Piece(pieceSpawner.CreateBasicPiece());
                var id = board.PlaceNewPieceAt(newPiece, x, y);
                resolveResult.ApplyChange(newPiece, new ChangeInfo.InstantSpawn(id, newPiece, x, y));
            }
        }
    }
}