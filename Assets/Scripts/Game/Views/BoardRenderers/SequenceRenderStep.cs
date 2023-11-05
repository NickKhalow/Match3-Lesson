#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using Core.ResolveResults;
using Cysharp.Threading.Tasks;
using Game.Views.VisualPieces;
using UnityEngine;

namespace Game.Views.BoardRenderers
{
    public class SequenceRenderStep : AbstractRenderStep
    {

        [SerializeField] private AbstractRenderStep[] steps = Array.Empty<AbstractRenderStep>();

        public override async UniTask Render(
            IReadOnlyResolveResult resolveResult,
            IDictionary<Guid, VisualPiece> visualPieces)
        {
            foreach (var step in steps)
            {
                await step.Render(resolveResult, visualPieces);
            }
        }
    }
}