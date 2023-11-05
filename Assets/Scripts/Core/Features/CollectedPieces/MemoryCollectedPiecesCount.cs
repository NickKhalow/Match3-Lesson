#nullable enable

using System.Collections.Generic;
using UniRx;

namespace Core.Features.CollectedPieces
{
    public class MemoryCollectedPiecesCount : ICollectedPiecesCount
    {
        private readonly Dictionary<int, int> reactiveValue = new Dictionary<int, int>();
        public ReactiveCommand OnUpdate { get; } = new ReactiveCommand();

        public int Count(int type)
        {
            return reactiveValue.TryGetValue(type, out var amount) ? amount : 0;
        }

        public void Add(int type, int amount)
        {
            reactiveValue[type] = Count(type) + amount;
            OnUpdate.Execute();
        }
    }
}