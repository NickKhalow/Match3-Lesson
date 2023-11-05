#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.ResolveResults;
using Core.Utils.NullSafety;
using Cysharp.Threading.Tasks;
using Game.Views.VisualPieces;
using UnityEngine;

namespace Game.Views.BoardRenderers
{
    public class InstantRenderStep : AbstractRenderStep
    {

        [SerializeField] private VisualPieceFactory visualPieceFactory = null!;

        private void Awake()
        {
            visualPieceFactory.EnsureNotNull();
        }

        public override UniTask Render(
            IReadOnlyResolveResult resolveResult,
            IDictionary<Guid, VisualPiece> visualPieces)
        {
            foreach (var instantSpawn in resolveResult.OfType<ChangeInfo.InstantSpawn>())
            {
                visualPieceFactory.VisualPiece(
                    new PiecePosition(
                        instantSpawn.Id,
                        instantSpawn.Piece,
                        instantSpawn.ToPos
                    )
                );
            }

            return UniTask.CompletedTask;
        }
    }
}