#nullable enable

using System.Linq;
using Core.Boards;
using Core.Conditions.LevelConditions;
using Core.Features.CollectedPieces;
using Core.ResolveResults;

namespace Core.Levels
{

    public class CollectedPiecesCountLevel : ILevel
    {

        private readonly ILevel origin;
        private readonly ICollectedPiecesCount collectedPiecesCount;

        public CollectedPiecesCountLevel(ILevel origin, ICollectedPiecesCount collectedPiecesCount)
        {
            this.origin = origin;
            this.collectedPiecesCount = collectedPiecesCount;
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
            foreach (var grouping in result.OfType<ChangeInfo.Resolved>().GroupBy(e => e.Type))
            {
                collectedPiecesCount.Add(grouping.Key, grouping.Count());
            }

            return result;
        }
    }
}