#nullable enable

using System;

namespace Core.ValueObjects
{
    public class BoardedPiece
    {
        public Guid Id { get; }
        public IPiece Piece { get; }

        public BoardedPiece(Guid id, IPiece piece)
        {
            Id = id;
            Piece = piece;
        }
    }
}