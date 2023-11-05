#nullable enable

using Core.Conditions.PiecesCollectedAmount;

namespace Core.Conditions.LevelConditions.Factory.Config
{

    public interface IWinConditionsConfig
    {

        IPiecesCollectedAmountConditionParams CollectedAmount { get; }

    }
}