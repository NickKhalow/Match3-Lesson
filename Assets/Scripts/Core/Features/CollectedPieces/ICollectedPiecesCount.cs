#nullable enable

using UniRx;

namespace Core.Features.CollectedPieces
{
    public interface ICollectedPiecesCount
    {
        public ReactiveCommand OnUpdate { get; }

        public int Count(int type);

        public void Add(int type, int count);
    }
}