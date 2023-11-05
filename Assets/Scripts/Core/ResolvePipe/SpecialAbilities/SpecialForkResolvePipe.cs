#nullable enable

using Core.Boards;
using Core.ResolvePipe.SpecialAbilities.Map;
using Core.ResolveResults;

namespace Core.ResolvePipe.SpecialAbilities
{

    public class SpecialForkResolvePipe : IResolvePipe
    {

        private readonly IResolvePipe specialPipe;
        private readonly IResolvePipe defaultPipe;
        private readonly IAbilityPieceMap abilityPieceMap;

        public SpecialForkResolvePipe(IResolvePipe specialPipe, IResolvePipe defaultPipe, IAbilityPieceMap abilityPieceMap)
        {
            this.specialPipe = specialPipe;
            this.defaultPipe = defaultPipe;
            this.abilityPieceMap = abilityPieceMap;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            if (board.GetAt(x, y)?.Piece is { } piece && abilityPieceMap.Ability(piece) is { })
            {
                specialPipe.ResolveAt(x, y, board, resolveResult);
                return;
            }

            defaultPipe.ResolveAt(x, y, board, resolveResult);
        }
    }
}