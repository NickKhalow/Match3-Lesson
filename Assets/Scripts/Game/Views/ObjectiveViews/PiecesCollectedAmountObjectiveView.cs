#nullable enable

using System;
using System.Collections.Generic;
using Core.Conditions.PiecesCollectedAmount;
using Core.Features.CollectedPieces;
using UniRx;
using UnityEngine;

namespace Game.Views.ObjectiveViews
{

    public class PiecesCollectedAmountObjectiveView : MonoBehaviour
    {

        [SerializeField] private PieceCountView prefab = null!;

        private readonly List<PieceCountView> instantiated = new List<PieceCountView>();
        private IPiecesCollectedAmountConditionParams conditionParams = null!;
        private ICollectedPiecesCount collectedPiecesCount = null!;

        private IDisposable? subscription;

        public void Initialize(
            ICollectedPiecesCount collectedPiecesCount,
            IPiecesCollectedAmountConditionParams conditionParams)
        {
            this.collectedPiecesCount = collectedPiecesCount;
            this.conditionParams = conditionParams;
            subscription?.Dispose();
            subscription = this.collectedPiecesCount.OnUpdate.Subscribe(_ => Render());
            Render();
        }

        // if will be performance issues this method should be optimized
        private void Render()
        {
            if (conditionParams.Pieces is { } pieces)
            {
                HideAll();
                InstantiateRemains(pieces.Count - instantiated.Count);
                RenderAll();
            }
        }

        private void HideAll()
        {
            foreach (var view in instantiated)
            {
                view.Hide();
            }
        }

        private void InstantiateRemains(int requiredCount)
        {
            for (int i = 0; i < requiredCount; i++)
            {
                var newOne = Instantiate(prefab, transform);
                instantiated.Add(newOne);
            }
        }

        private void RenderAll()
        {
            if (conditionParams.Pieces is null)
            {
                return;
            }

            var index = 0;

            foreach (var (type, amount) in conditionParams.Pieces)
            {
                var remains = amount - collectedPiecesCount.Count(type);
                if (remains > 0)
                {
                    instantiated[index]!.Render(type, remains);
                    index++;
                }
            }
        }
    }
}