#nullable enable

using Core.Conditions.LevelConditions.Factory.Config;
using Core.Conditions.MoveReachedAmount;
using Core.Conditions.PiecesCollectedAmount;
using Core.Utils.NullSafety;
using Game.Configs.Conditions;
using UnityEngine;

namespace Game.Configs
{

    [CreateAssetMenu(fileName = "Condition", menuName = "Conditions", order = 0)]
    public class SoConditions : ScriptableObject, IWinConditionsConfig, ILoseConditionsConfig
    {

        [Header("Win")]
        [SerializeField] private SoPiecesCollectedAmountConditionParams collectedAmountCondition = null!;
        [Header("Lose")]
        [SerializeField] private MovesReachedAmountConditionParams movesReachedAmountCondition = null!;


        public IPiecesCollectedAmountConditionParams CollectedAmount => collectedAmountCondition.EnsureNotNull();

        public IMovesReachedAmountConditionParams MovesReachedAmount => movesReachedAmountCondition.EnsureNotNull();
    }
}