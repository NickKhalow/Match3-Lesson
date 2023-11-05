using Core.ResolvePipe.SpecialAbilities.Map;

namespace Core.ValueObjects.SpecialPieces
{

    public interface ISpecialPiece : IPiece
    {
        SpecialAbility Ability { get; }
    }
}