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

    public class CreatedRenderStep : AbstractRenderStep
    {

        [SerializeField] private VisualPieceFactory visualPieceFactory = null!;

        private void Awake()
        {
            visualPieceFactory.EnsureNotNull();
        }

        public override async UniTask Render(
            IReadOnlyResolveResult resolveResult,
            IDictionary<Guid, VisualPiece> visualPieces)
        {
            var createdVisuals = new List<(BoardPos, VisualPiece)>();
            foreach (var grouping in resolveResult.OfType<ChangeInfo.Created>().GroupBy(e => e.CreationTime))
            {
                foreach (var created in grouping)
                {
                    var visual = visualPieceFactory.VisualPiece(
                        new PiecePosition(
                            created.Id,
                            created.Piece,
                            new BoardPos(created.ToPos.X, -1)
                        )
                    );
                    createdVisuals.Add((created.ToPos, visual));
                }

                await MoveMany(createdVisuals);
                createdVisuals.Clear();
            }
        }
    }
}