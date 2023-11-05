#nullable enable

using System;
using Core.ResolveResults;
using Core.ValueObjects;

namespace Core.Boards
{

    public interface IBoard : IReadOnlyBoard
    {

        Guid PlaceNewPieceAt(IPiece piece, int x, int y);

        void MovePieceTo(Guid pieceId, int toX, int toY);

        void RemovePieceAt(int x, int y);

        ResolveResult Resolve(int x, int y);
    }
}