#nullable enable

using System;
using System.Collections.Generic;
using Core.Levels;
using Core.ResolveResults;
using Core.Utils;
using Core.ValueObjects;

namespace Core.Boards
{

    public static class BoardExtension
    {
        public static bool IsWithinBounds(this IReadOnlyBoard board, int x, int y)
        {
            return x < board.Width && y < board.Height && x >= 0 && y >= 0; //more simply version
        }

        public static bool TryGetPiecePos(this IBoard board, BoardedPiece piece, out int px, out int py)
        {
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    if (board.GetAt(x, y) == piece)
                    {
                        px = x;
                        py = y;
                        return true;
                    }
                }
            }

            px = -1;
            py = -1;
            return false;
        }

        public static IReadOnlyList<BoardedPiece> GetNeighbors(this IBoard board, int x, int y)
        {
            var neighbors = new List<BoardedPiece>(4);

            neighbors = AddNeighbor(board, x - 1, y, neighbors); // Left
            neighbors = AddNeighbor(board, x, y - 1, neighbors); // Top
            neighbors = AddNeighbor(board, x + 1, y, neighbors); // Right
            neighbors = AddNeighbor(board, x, y + 1, neighbors); // Bottom

            return neighbors;
        }

        private static List<BoardedPiece> AddNeighbor(IBoard board, int x, int y, List<BoardedPiece> neighbors)
        {
            if (!board.IsWithinBounds(x, y)) return neighbors;

            neighbors.Add(board.GetAt(x, y) ?? throw new InvalidOperationException());
            return neighbors;
        }

        public static int[,] GetBoardStateAsArrayWithTypes(this IBoard board)
        {
            var result = new int[board.Width, board.Height];

            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    var p = board.GetAt(x, y);
                    result[x, y] = p?.Piece.Type ?? -1; //simplify condition with p?.
                }
            }

            return result.TransposeArray(); //as extension method
        }

        public static void FindAndRemoveConnectedAt(this IBoard board, int x, int y)
        {
            var connections = board.GetConnected(x, y);
            if (connections.Count > 1)
            {
                board.RemovePieces(connections);
            }
        }

        private static void RemovePieces(this IBoard board, IReadOnlyList<BoardedPiece> connections)
        {
            foreach (var piece in connections)
            {
                if (board.TryGetPiecePos(piece, out var x, out var y))
                {
                    board.RemovePieceAt(x, y);
                }
            }
        }

        public static List<BoardedPiece> GetConnected(this IBoard board, int x, int y)
        {
            var start = board.GetAt(x, y);
            return board.SearchForConnected(start, new List<BoardedPiece>());
        }

        private static List<BoardedPiece> SearchForConnected(
            this IBoard board,
            BoardedPiece piece,
            List<BoardedPiece> searched)
        {
            if (!board.TryGetPiecePos(piece, out var x, out var y))
            {
                return searched;
            }

            searched.Add(piece);
            var neighbors = board.GetNeighbors(x, y);

            if (neighbors.Count == 0)
            {
                return searched;
            }

            for (int i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                if (!searched.Contains(neighbor) && neighbor?.Piece.Type == piece.Piece.Type)
                {
                    SearchForConnected(board, neighbor, searched);
                }
            }

            return searched;
        }

        public static void EnsureEmpty(this IBoard board, int x, int y)
        {
            if (board.GetAt(x, y) != null)
            {
                throw new InvalidOperationException("Pose is not empty");
            }
        }

        public static void RemovePieceAt(this IBoard board, BoardPos boardPos)
        {
            board.RemovePieceAt(boardPos.X, boardPos.Y);
        }

        public static bool TryResolve(this ILevel level, int x, int y, out ResolveResult? resolveResult)
        {
            if (level.Board().IsWithinBounds(x, y))
            {
                resolveResult = level.Resolve(x, y);
                return true;
            }

            resolveResult = null;
            return false;
        }
    }
}