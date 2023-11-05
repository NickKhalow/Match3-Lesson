#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.ResolveResults;
using Core.Utils.NullSafety;
using Core.ValueObjects;
using Cysharp.Threading.Tasks;
using Game.Views.VisualPieces;
using UnityEngine;

namespace Game.Views.BoardRenderers
{

    public class FallDownRenderStep : AbstractRenderStep
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
            var fallVisuals = new List<(BoardPos, VisualPiece)>();
            foreach (var fall in resolveResult.OfType<ChangeInfo.FallDown>())
            {
                if (!visualPieces.TryGetValue(fall.Id, out var visualPiece))
                {
                    visualPiece = visualPieceFactory.VisualPiece(
                        new PiecePosition(
                            fall.Id,
                            fall.Piece,
                            fall.FromPos
                        )
                    );
                }

                fallVisuals.Add((fall.ToPos, visualPiece));
            }

            return MoveMany(fallVisuals);
        }
    }
}