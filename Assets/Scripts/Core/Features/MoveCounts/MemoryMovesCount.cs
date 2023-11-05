#nullable enable

using UniRx;

namespace Core.Features.MoveCounts
{

    public class MemoryMovesCount : IMovesCount
    {

        private readonly IntReactiveProperty count;

        public MemoryMovesCount(int current = 0)
        {
            count = new IntReactiveProperty(current);
        }

        IReadOnlyReactiveProperty<int> IMovesCount.Count => count;

        public void Increase()
        {
            count.Value++;
        }
    }
}