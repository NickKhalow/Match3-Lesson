#nullable enable

using System.Collections.Generic;

namespace Core.Conditions.PiecesCollectedAmount
{

    public interface IPiecesCollectedAmountConditionParams
    {

        public IReadOnlyList<(int Type, int Amount)>? Pieces { get; }
    }
}