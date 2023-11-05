#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utils.NullSafety;
using Core.ValueObjects;
using Game.Configs;
using UnityEngine;

namespace Game.Views
{
    public class ViewsFactory : MonoBehaviour
    {
        [SerializeField] private List<SoPieceSet> pieceSet = null!;

        private void Awake()
        {
            if (pieceSet == null)
            {
                throw new NullReferenceException();
            }
        }

        public GameObject NewView(IPiece piece)
        {
            var prefab = pieceSet.SelectMany(e => e.Pieces)
                             .FirstOrDefault(e => e.Type == piece.Type)
                             ?.PrefabView
                         ?? throw new Exception("Piece type not found");
            return Instantiate(prefab).EnsureNotNull();
        }
    }
}