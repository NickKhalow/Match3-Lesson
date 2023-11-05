#nullable enable

using Core.Features.MoveCounts;

namespace Core.Conditions.MoveReachedAmount
{

    public class MoveReachedAmountCondition : ICondition
    {

        private readonly IMovesCount movesCount;
        private readonly IMovesReachedAmountConditionParams conditionParams;

        public MoveReachedAmountCondition(IMovesCount movesCount, IMovesReachedAmountConditionParams conditionParams)
        {
            this.conditionParams = conditionParams;
            this.movesCount = movesCount;
        }

        public bool IsMet()
        {
            if (conditionParams.MaxMoves is null)
            {
                return false;
            }

            return conditionParams.MaxMoves.Value <= movesCount.Count.Value;
        }
    }
}