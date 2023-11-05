#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Core.ValueObjects.SpecialPieces;

namespace Core.ResolvePipe.SpecialAbilities.Map.Factory
{
    public class AbilityPieceMapFactory
    {

        public IAbilityPieceMap AbilityPieceMap(IReadOnlyList<ISpecialPiece> pieces)
        {
            return new AbilityPieceMap(pieces.ToDictionary(e => e.Type, e => FromAbility(e.Ability)));
        }

        private static IResolvePipe FromAbility(SpecialAbility ability)
        {
            return ability switch
            {
                SpecialAbility.HorizontalBoom => new HorizontalLineBoomResolvePipe(),
                SpecialAbility.VerticalBoom => new VerticalLineBoomResolvePipe(),
                _ => throw new ArgumentOutOfRangeException(nameof(ability), ability, null)
            };
        }
    }
}