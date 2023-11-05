#nullable enable

using System;
using System.Collections.Generic;
using Core;
using Core.Boards;
using Core.Conditions.LevelConditions;
using Core.Levels;
using Core.Utils.NullSafety;
using Cysharp.Threading.Tasks;
using Game.Views.BoardRenderers;
using Game.Views.VisualPieces;
using UnityEngine;

namespace Game.Views
{

    public class BoardRenderer : MonoBehaviour
    {

        [SerializeField] private AbstractRenderStep renderStep = null!;
        [SerializeField] private VisualPieceFactory visualPieceFactory = null!;
        [SerializeField] private ViewCamera viewCamera = null!;

        private readonly Dictionary<Guid, VisualPiece> visualPieces = new Dictionary<Guid, VisualPiece>();
        private ILevel level = null!;

        public void Initialize(ILevel level)
        {
            this.level = level;

            renderStep.EnsureNotNull();
            visualPieceFactory.EnsureNotNull();
            viewCamera.EnsureNotNull();

            viewCamera.CenterTo(level.Board());
            RenderVisualPiecesFromBoardState();
        }

        private void RenderVisualPiecesFromBoardState()
        {
            DestroyVisualPieces();
            foreach (var pieceInfo in level.Board())
            {
                var visual = visualPieceFactory.VisualPiece(new PiecePosition(pieceInfo));
                visualPieces.Add(pieceInfo.value.Id, visual);
            }
        }

        private void DestroyVisualPieces()
        {
            foreach (var visualPiece in visualPieces)
            {
                visualPieceFactory.Recycle(visualPiece.Value!);
            }

            visualPieces.Clear();
        }

        public async UniTask Play()
        {
            while (level.Conditions().IsFinish() == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //should extract input to separated object
                    var pos = viewCamera.ScreenPosToLogicPos(Input.mousePosition.x, Input.mousePosition.y);
                    if (level.TryResolve(pos.X, pos.Y, out var resolveResult))
                    {
                        await renderStep.Render(resolveResult!, visualPieces);
                        RenderVisualPiecesFromBoardState();
                    }
                }

                await UniTask.Yield();
            }
        }
    }
}