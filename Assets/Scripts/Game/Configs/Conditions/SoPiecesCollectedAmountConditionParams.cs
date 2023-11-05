#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Core.Conditions.PiecesCollectedAmount;
using UnityEngine;

namespace Game.Configs.Conditions
{
    [Serializable]
    public class SoPiecesCollectedAmountConditionParams : IPiecesCollectedAmountConditionParams
    {
        [Header("Collected Amount")]
        [SerializeField] private List<Pair> list = new List<Pair>();

        public IReadOnlyList<(int Type, int Amount)>? Pieces => list.Count > 0
            ? list.Select(e => (e.type.Type, e.amount)).ToList()
            : null;

        [Serializable]
        public class Pair
        {
            public SoPiece type;
            public int amount;
        }
    }
}