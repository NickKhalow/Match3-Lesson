#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using Core.Matrices;
using Core.PieceSpawners;
using Core.ResolvePipe;
using Core.ResolvePipe.SpecialAbilities;
using Core.ResolvePipe.SpecialAbilities.Map;
using Core.ResolveResults;
using Core.Utils;
using Core.ValueObjects;

namespace Core.Boards
{
    public class Board : IBoard
    {
        private readonly Matrix<BoardedPiece> boardState;
        private readonly IResolvePipe resolvePipe;

        public int Width => boardState.Width;

        public int Height => boardState.Height;

        public Board(int[,] definition) : this(
            definition,
            new FromRangePieceSpawner(20),
            new FromRangePieceSpawner(80),
            new AbilityPieceMap(new Dictionary<int, IResolvePipe>())
        )
        {
        }

        public Board(int[,] definition, IPieceSpawner pieceSpawner, IPieceSpawner rocketSpawner) : this(
            definition,
            pieceSpawner,
            rocketSpawner,
            new AbilityPieceMap(new Dictionary<int, IResolvePipe>()
            )
        )
        {
        }

        public Board(
            int[,] definition,
            IPieceSpawner pieceSpawner,
            IPieceSpawner rocketSpawner,
            IAbilityPieceMap abilityMap) : this(
            definition,
            new SequenceResolvePipe(
                new SpecialForkResolvePipe(
                    specialPipe: new ApplyAbilityResolvePipe(abilityMap),
                    defaultPipe: new SequenceResolvePipe(
                        new DissolvePiecesResolvePipe(),
                        new CreateSpecialPieceOnClickResolvePipe(rocketSpawner)
                    ),
                    abilityMap
                ),
                new MoveAndCreatePiecesUntilFullResolvePipe(pieceSpawner)
            )
        )
        {
        }

        public Board(int[,] definition, IResolvePipe resolvePipe) : this(
            Matrix<Piece>.From(definition.TransposeArray(), i => new Piece(i)).AsRaw(),
            resolvePipe
        )
        {
        }

        public Board(Piece?[,] boardState, IResolvePipe resolvePipe) : this(
            Matrix<BoardedPiece>.From(
                boardState,
                piece => piece is null
                    ? null
                    : new BoardedPiece(Guid.NewGuid(), piece!)),
            resolvePipe
        )
        {
        }

        public Board(Matrix<BoardedPiece> boardState, IResolvePipe resolvePipe)
        {
            this.boardState = boardState;
            this.resolvePipe = resolvePipe;
        }

        public BoardedPiece GetAt(int x, int y)
        {
            return boardState.GetAt(x, y);
        }

        public BoardedPiece[,] AsRaw()
        {
            return boardState.AsRaw();
        }

        public ResolveResult Resolve(int x, int y)
        {
            var result = new ResolveResult();
            resolvePipe.ResolveAt(x, y, this, result);
            return result;
        }

        public Guid PlaceNewPieceAt(IPiece piece, int x, int y)
        {
            var guid = Guid.NewGuid();
            boardState.SetAt(x, y, new BoardedPiece(guid, piece));
            return guid;
        }

        public IEnumerator<(int x, int y, BoardedPiece value)> GetEnumerator()
        {
            return boardState.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void MovePieceTo(Guid pieceId, int toX, int toY)
        {
            this.EnsureEmpty(toX, toY);
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var piece = boardState.GetAt(x, y);
                    if (piece?.Id == pieceId)
                    {
                        boardState.SetAt(toX, toY, piece);
                        RemovePieceAt(x, y);
                        return;
                    }
                }
            }

            throw new Exception($"Piece with id {pieceId} not found");
        }

        public void RemovePieceAt(int x, int y)
        {
            boardState.SetAt(x, y, null!);
        }
    }
}