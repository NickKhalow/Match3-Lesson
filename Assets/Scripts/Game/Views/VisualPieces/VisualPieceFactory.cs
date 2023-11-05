#nullable enable

using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Views.VisualPieces
{
    public class VisualPieceFactory : MonoBehaviour
    {
        [FormerlySerializedAs("viewsDatabase")] [FormerlySerializedAs("pieceTypeDatabase")] [SerializeField] private ViewsFactory viewsFactory;
        [SerializeField] private VisualPiece visualPiecePrefab;

        private readonly Stack<VisualPiece> pool = new Stack<VisualPiece>();

        public VisualPiece VisualPiece(PiecePosition piecePosition)
        {
            var pieceObject = pool.Count > 0 ? pool.Pop() : Instantiate(visualPiecePrefab, transform, true);
            pieceObject!.Show();
            var view = viewsFactory.NewView(piecePosition.Piece);
            pieceObject.ApplyView(view);
            pieceObject.PlaceAt(piecePosition.Pos.X, piecePosition.Pos.Y);
            return pieceObject;
        }

        public void Recycle(VisualPiece visualPiece)
        {
            pool.Push(visualPiece);
            visualPiece.Hide();
        }
    }
}