#nullable enable

using Core.Boards;
using Core.PieceSpawners;
using Core.ResolveResults;
using Core.ValueObjects;

namespace Core.ResolvePipe
{

    public class MoveAndCreatePiecesUntilFullResolvePipe : IResolvePipe
    {

        private readonly IPieceSpawner pieceSpawner;

        public MoveAndCreatePiecesUntilFullResolvePipe(IPieceSpawner pieceSpawner)
        {
            this.pieceSpawner = pieceSpawner;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            ResolveAt(board, resolveResult);
        }

        public void ResolveAt(IBoard board, ResolveResult resolveResult)
        {
            int resolveStep = 0;
            bool moreToResolve = true;

            while (moreToResolve)
            {
                moreToResolve = MovePiecesOneDownIfAble(board, resolveResult);
                moreToResolve |= CreatePiecesAtTop(board, resolveResult, resolveStep);
                resolveStep++;
            }
        }

        private bool CreatePiecesAtTop(IBoard board, ResolveResult resolveResult, int resolveStep)
        {
            var createdAnyPieces = false;
            var y = 0;
            for (int x = 0; x < board.Width; x++)
            {
                if (board.GetAt(x, y) == null)
                {
                    var piece = new Piece(pieceSpawner.CreateBasicPiece());
                    var guid = board.PlaceNewPieceAt(piece, x, y);
                    createdAnyPieces = true;
                    resolveResult.ApplyChange(
                        piece,
                        new ChangeInfo.Created(
                            guid,
                            piece,
                            creationTime: resolveStep,
                            toPos: new BoardPos(x, y)
                        )
                    );
                }
            }

            return createdAnyPieces;
        }

        private bool MovePiecesOneDownIfAble(IBoard board, ResolveResult resolveResult)
        {
            bool movedAny = false;

            for (int y = board.Height - 1; y >= 1; y--)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    var dest = board.GetAt(x, y);
                    if (dest != null)
                    {
                        continue;
                    }

                    var pieceToMove = board.GetAt(x, y - 1);
                    if (pieceToMove == null)
                    {
                        continue;
                    }

                    var fromY = y - 1;
                    board.MovePieceTo(pieceToMove.Id, x, y);
                    movedAny = true;

                    if (resolveResult.ChangeInfo(pieceToMove.Piece) is ChangeInfo.Created info)
                    {
                        resolveResult.ApplyChange(
                            pieceToMove.Piece,
                            new ChangeInfo.Created(
                                info.Id,
                                info.Piece,
                                info.CreationTime,
                                toPos: new BoardPos(x, y)
                            )
                        );
                    }
                    else
                    {
                        resolveResult.ApplyChange(
                            pieceToMove.Piece,
                            new ChangeInfo.FallDown(
                                pieceToMove.Id,
                                pieceToMove.Piece,
                                fromPos: new BoardPos(x, fromY),
                                toPos: new BoardPos(x, y)
                            )
                        );
                    }
                }
            }

            return movedAny;
        }
    }
}