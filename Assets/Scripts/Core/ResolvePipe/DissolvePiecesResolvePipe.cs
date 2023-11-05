#nullable enable

using Core.Boards;
using Core.ResolveResults;
using Core.ValueObjects;

namespace Core.ResolvePipe
{

    public class DissolvePiecesResolvePipe : IResolvePipe
    {

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            var connections = board.GetConnected(x, y);
            if (connections.Count > 1)
            {
                foreach (var connection in connections)
                {
                    board.TryGetPiecePos(connection, out var px, out var py);
                    board.RemovePieceAt(px, py);
                    resolveResult.ApplyChange(
                        connection.Piece,
                        new ChangeInfo.Resolved(
                            connection.Id,
                            connection.Piece.Type,
                            new BoardPos(px, py)
                        )
                    );
                }
            }
        }
    }
}