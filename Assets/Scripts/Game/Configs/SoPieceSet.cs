#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Piece Set", menuName = "Piece Set", order = 0)]
    public class SoPieceSet : ScriptableObject
    {
        [SerializeField] private List<SoPiece> list = new List<SoPiece>();

        public IReadOnlyList<SoPiece> Pieces => list;
    }
}