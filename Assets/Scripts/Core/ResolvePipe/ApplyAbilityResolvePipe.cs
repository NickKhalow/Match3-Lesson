#nullable enable

using Core.Boards;
using Core.ResolvePipe.SpecialAbilities.Map;
using Core.ResolveResults;

namespace Core.ResolvePipe
{
    public class ApplyAbilityResolvePipe : IResolvePipe
    {

        private readonly IAbilityPieceMap abilityPieceMap;

        public ApplyAbilityResolvePipe(IAbilityPieceMap abilityPieceMap)
        {
            this.abilityPieceMap = abilityPieceMap;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            if (board.GetAt(x, y) is { } piece)
            {
                abilityPieceMap.Ability(piece.Piece)?.ResolveAt(x, y, board, resolveResult);
            }
        }
    }
}