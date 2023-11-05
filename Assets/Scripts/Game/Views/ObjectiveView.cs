#nullable enable

using Core.Conditions.PiecesCollectedAmount;
using Core.Features.CollectedPieces;
using Game.Views.ObjectiveViews;
using UnityEngine;

namespace Game.Views
{

    public class ObjectiveView : MonoBehaviour
    {

        [SerializeField]
        private PiecesCollectedAmountObjectiveView piecesCollectedAmountObjectiveView = null!;

        public void Initialize(
            ICollectedPiecesCount collectedPiecesCount,
            IPiecesCollectedAmountConditionParams conditionParams)
        {
            piecesCollectedAmountObjectiveView.Initialize(collectedPiecesCount, conditionParams);
        }
    }
}