#nullable enable

using System.Linq;
using Core.Boards;
using Core.Conditions.LevelConditions;
using Core.Features.MoveCounts;
using Core.ResolveResults;

namespace Core.Levels
{
    public class MovesCountLevel : ILevel
    {

        private readonly ILevel origin;
        private readonly IMovesCount movesCount;

        public MovesCountLevel(ILevel origin, IMovesCount movesCount)
        {
            this.origin = origin;
            this.movesCount = movesCount;
        }

        public ILevelConditions Conditions()
        {
            return origin.Conditions();
        }

        public IReadOnlyBoard Board()
        {
            return origin.Board();
        }

        public ResolveResult Resolve(int x, int y)
        {
            var result = origin.Resolve(x, y);
            if (result.Any())
            {
                movesCount.Increase();
            }

            return result;
        }
    }
}