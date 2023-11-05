#nullable enable

using Core.Conditions.MoveReachedAmount;

namespace Core.Conditions.LevelConditions.Factory.Config
{

    public interface ILoseConditionsConfig
    {
        IMovesReachedAmountConditionParams MovesReachedAmount { get; }
    }
}