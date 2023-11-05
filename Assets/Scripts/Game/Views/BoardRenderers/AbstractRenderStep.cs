#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.ResolveResults;
using Core.ValueObjects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Views.VisualPieces;
using UnityEngine;

namespace Game.Views.BoardRenderers
{

    [DisallowMultipleComponent]
    public abstract class AbstractRenderStep : MonoBehaviour
    {
        public abstract UniTask Render(
            IReadOnlyResolveResult resolveResult,
            IDictionary<Guid, VisualPiece> visualPieces);

        protected static async UniTask MoveMany(IReadOnlyList<(BoardPos, VisualPiece)> list)
        {
            if (list.Count == 0)
            {
                return;
            }

            var sequence = DOTween.Sequence()!;
            foreach (var pair in list)
            {
                sequence.Insert(0, pair.Item2.MoveTo(pair.Item1));
            }
            
            ;
        }
    }
}