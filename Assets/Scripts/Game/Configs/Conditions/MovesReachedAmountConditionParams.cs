using System;
using Core.Conditions.MoveReachedAmount;
using UnityEngine;

namespace Game.Configs.Conditions
{

    [Serializable]
    public class MovesReachedAmountConditionParams : IMovesReachedAmountConditionParams
    {

        [SerializeField] private int maxMoves;

        public int? MaxMoves => maxMoves == 0 ? null : (int?)maxMoves;
    }
}