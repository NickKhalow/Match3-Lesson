#nullable enable

using System;
using System.Linq;
using Game.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.ObjectiveViews
{

    public class PieceCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countText = null!;
        [SerializeField] private Image icon = null!;
        [SerializeField] private SoPieceSet pieceSet = null!;

        public void Render(int type, int count)
        {
            gameObject.SetActive(true);
            icon.sprite = pieceSet.Pieces.FirstOrDefault(e => e.Type == type) is { } piece
                ? piece.Icon
                : throw new NullReferenceException("Sprite not found");
            countText.SetText(count.ToString());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}