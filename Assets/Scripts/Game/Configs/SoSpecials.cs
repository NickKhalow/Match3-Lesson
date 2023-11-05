#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.ResolvePipe.SpecialAbilities.Map;
using Core.ValueObjects.SpecialPieces;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Specials", menuName = "Specials", order = 0)]
    public class SoSpecials : ScriptableObject, IEnumerable<ISpecialPiece>
    {
        [SerializeField] private List<Pair> list = new List<Pair>();

        [Serializable]
        public class Pair
        {
            public SoPiece piece = null!;
            public SpecialAbility ability;
        }

        public IEnumerator<ISpecialPiece> GetEnumerator()
        {
            return list.Select(e => new SpecialPiece(e.piece.Type, e.ability)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}