using Core.ResolvePipe.SpecialAbilities.Map;

namespace Core.ValueObjects.SpecialPieces
{
    public class SpecialPiece : ISpecialPiece
    {

        public SpecialPiece(int type, SpecialAbility ability)
        {
            Type = type;
            Ability = ability;
        }

        public int Type { get; }
        public SpecialAbility Ability { get; }
    }
}