#nullable enable

using System;
using Core.Conditions.MoveReachedAmount;
using Core.Features.MoveCounts;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.Views
{

    public class MovesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null!;

        private IMovesCount movesCount = null!;
        private IMovesReachedAmountConditionParams conditionParams = null!;

        private IDisposable? subscription;

        public void Initialize(IMovesCount movesCount, IMovesReachedAmountConditionParams conditionParams)
        {
            this.movesCount = movesCount;
            this.conditionParams = conditionParams;
            subscription?.Dispose();
            subscription = movesCount.Count.Subscribe(UpdateText);
        }

        private void UpdateText(int newCount)
        {
            var newText = conditionParams.MaxMoves is { } value
                ? $"{newCount} / {value}"
                : $"{newCount}";
            text.SetText(newText);
        }
    }
}