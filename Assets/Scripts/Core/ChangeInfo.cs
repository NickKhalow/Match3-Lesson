#nullable enable

using System;
using Core.ValueObjects;

namespace Core
{

    public abstract class ChangeInfo
    {

        public Guid Id { get; }

        protected ChangeInfo(Guid id)
        {
            Id = id;
        }

        public class Resolved : ChangeInfo
        {
            public BoardPos Pos { get; }
            public int Type { get; }

            public Resolved(PiecePosition position) : this(position.Id, position.Piece.Type, position.Pos)
            {
            }

            public Resolved(Guid guid, int type, BoardPos pos) : base(guid)
            {
                Pos = pos;
                Type = type;
            }

            public override string ToString()
            {
                return "ChangeInfo.Resolved{"
                       + $", Pos={Pos}"
                       + '}';
            }
        }


        public class Created : ChangeInfo
        {
            public IPiece Piece { get; }
            public int CreationTime { get; }
            public BoardPos ToPos { get; }

            public Created(Guid guid, IPiece piece, int creationTime, BoardPos toPos) : base(guid)
            {
                CreationTime = creationTime;
                ToPos = toPos;
                Piece = piece;
            }

            public override string ToString()
            {
                return "ChangeInfo.Created{"
                       + $", CreationTime={CreationTime}"
                       + $", ToPos={ToPos}"
                       + '}';
            }
        }

        public class InstantSpawn : ChangeInfo
        {
            public IPiece Piece { get; }
            public BoardPos ToPos { get; }

            public InstantSpawn(Guid guid, IPiece piece, int toX, int toY) : this(guid, piece, new BoardPos(toX, toY))
            {
            }

            public InstantSpawn(Guid guid, IPiece piece, BoardPos toPos) : base(guid)
            {
                ToPos = toPos;
                Piece = piece;
            }

            public override string ToString()
            {
                return "ChangeInfo.Created{"
                       + $", ToPos={ToPos}"
                       + '}';
            }
        }

        public class FallDown : ChangeInfo
        {

            public IPiece Piece { get; }
            public BoardPos FromPos { get; }
            public BoardPos ToPos { get; }

            public FallDown(Guid guid, IPiece piece, BoardPos fromPos, BoardPos toPos) : base(guid)
            {
                Piece = piece;
                FromPos = fromPos;
                ToPos = toPos;
            }

            public override string ToString()
            {
                return "ChangeInfo.FallDown{"
                       + $", FromPos={FromPos}"
                       + $", ToPos={ToPos}"
                       + '}';
            }
        }
    }
}