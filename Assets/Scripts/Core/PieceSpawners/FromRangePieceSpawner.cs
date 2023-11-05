#nullable enable

using System;
using System.Collections.Generic;

namespace Core.PieceSpawners
{

    public class FromRangePieceSpawner : IPieceSpawner
    {
        private readonly IReadOnlyList<int> types;
        private readonly Random random = new Random();

        public FromRangePieceSpawner(params int[] types) : this(types as IReadOnlyList<int>)
        {
        }

        public FromRangePieceSpawner(IReadOnlyList<int> types)
        {
            this.types = types;
        }

        public int CreateBasicPiece()
        {
            return types[random.Next(0, types.Count)];
        }
    }
}