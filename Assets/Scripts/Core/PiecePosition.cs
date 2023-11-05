#nullable enable

using System;
using Core.ValueObjects;

namespace Core
{

    public struct PiecePosition
    {

        public Guid Id { get; }
        public IPiece Piece { get; }
        public BoardPos Pos { get; }

        public PiecePosition(BoardedPiece piece, BoardPos pose) : this(piece.Id, piece.Piece, pose)
        {
        }

        public PiecePosition((int x, int y, BoardedPiece value) tuple) : this(tuple.value!, new BoardPos(tuple.x, tuple.y))
        {
        }

        public PiecePosition(Guid id, IPiece piece, BoardPos pos)
        {
            this.Id = id;
            this.Piece = piece;
            this.Pos = pos;
        }

        public PiecePosition(ChangeInfo.InstantSpawn instantSpawn)
        {
            Id = instantSpawn.Id;
            Piece = instantSpawn.Piece;
            Pos = instantSpawn.ToPos;
        }
    }
}