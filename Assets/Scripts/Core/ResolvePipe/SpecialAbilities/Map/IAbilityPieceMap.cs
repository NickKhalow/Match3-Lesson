#nullable enable

using Core.ValueObjects;

namespace Core.ResolvePipe.SpecialAbilities.Map
{
    public interface IAbilityPieceMap
    {
        IResolvePipe? Ability(IPiece piece);
    }
}