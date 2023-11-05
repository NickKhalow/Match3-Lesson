#nullable enable

using System.Collections.Generic;
using Core.ValueObjects;

namespace Core.ResolvePipe.SpecialAbilities.Map
{

    public class AbilityPieceMap : IAbilityPieceMap
    {

        private readonly IReadOnlyDictionary<int, IResolvePipe> typeToAbility;

        public AbilityPieceMap(IReadOnlyDictionary<int, IResolvePipe> typeToAbility)
        {
            this.typeToAbility = typeToAbility;
        }

        public IResolvePipe? Ability(IPiece piece)
        {
            return typeToAbility.TryGetValue(piece.Type, out var ability)
                ? ability
                : null;
        }
    }
}