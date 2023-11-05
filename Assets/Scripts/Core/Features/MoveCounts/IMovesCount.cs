#nullable enable

using UniRx;

namespace Core.Features.MoveCounts
{

    public interface IMovesCount
    {

        IReadOnlyReactiveProperty<int> Count { get; }

        void Increase();
    }
}