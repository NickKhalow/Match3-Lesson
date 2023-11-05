#nullable enable

using Core.Features.CollectedPieces;

namespace Core.Conditions.PiecesCollectedAmount
{

    public class PiecesCollectedAmountCondition : ICondition
    {

        private readonly ICollectedPiecesCount collectedPiecesCount;
        private readonly IPiecesCollectedAmountConditionParams conditionParams;

        public PiecesCollectedAmountCondition(
            ICollectedPiecesCount collectedPiecesCount,
            IPiecesCollectedAmountConditionParams conditionParams
        )
        {
            this.conditionParams = conditionParams;
            this.collectedPiecesCount = collectedPiecesCount;
        }

        public bool IsMet()
        {
            if (conditionParams.Pieces is null)
            {
                return false;
            }

            foreach (var (type, amount) in conditionParams.Pieces)
            {
                if (collectedPiecesCount.Count(type) < amount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}