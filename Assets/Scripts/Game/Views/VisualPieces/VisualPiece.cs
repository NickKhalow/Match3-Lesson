#nullable enable

using System.Collections;
using Core.ValueObjects;
using DG.Tweening;
using UnityEngine;

namespace Game.Views.VisualPieces
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class VisualPiece : MonoBehaviour
    {
        [SerializeField] private float moveDuration;
        [Header("Debug")]
        [SerializeField] private GameObject? currentView;


        public void ApplyView(GameObject view)
        {
            //TODO: this should be a pool
            if (currentView != null)
            {
                Destroy(currentView);
            }

            view.transform.SetParent(transform);
            view.transform.localPosition = Vector3.zero;
            currentView = view;
        }

        public void PlaceAt(float x, float y)
        {
            transform.localPosition = LogicPosToVisualPos(x, y);
        }

        public void PlaceAt(BoardPos pos)
        {
            transform.localPosition = LogicPosToVisualPos(pos);
        }

        public Tween MoveTo(BoardPos pos, float duration)
        {
            var end = LogicPosToVisualPos(pos);
            return transform
                .DOLocalMove(end, duration)
                .OnComplete(() => PlaceAt(pos))!;
        }

        public Tween MoveTo(BoardPos pos)
        {
            return MoveTo(pos, moveDuration);
        }

        private static Vector3 LogicPosToVisualPos(BoardPos pos)
        {
            return LogicPosToVisualPos(pos.X, pos.Y);
        }

        private static Vector3 LogicPosToVisualPos(float x, float y)
        {
            return new Vector3(x, -y, -y);
        }
    }
}