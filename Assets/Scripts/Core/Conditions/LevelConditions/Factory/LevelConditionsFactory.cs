#nullable enable

using Core.Conditions.LevelConditions.Factory.Config;
using Core.Conditions.MoveReachedAmount;
using Core.Conditions.PiecesCollectedAmount;
using Core.Features.CollectedPieces;
using Core.Features.MoveCounts;

namespace Core.Conditions.LevelConditions.Factory
{

    public class LevelConditionsFactory
    {

        private readonly ICollectedPiecesCount collectedPiecesCount;
        private readonly IMovesCount movesCount;

        public LevelConditionsFactory(ICollectedPiecesCount collectedPiecesCount, IMovesCount movesCount)
        {
            this.collectedPiecesCount = collectedPiecesCount;
            this.movesCount = movesCount;
        }

        public ILevelConditions CreateLevelConditions(IWinConditionsConfig win, ILoseConditionsConfig lose)
        {
            return new LevelConditions(
                winCondition: new PiecesCollectedAmountCondition(collectedPiecesCount, win.CollectedAmount),
                loseCondition: new MoveReachedAmountCondition(movesCount, lose.MovesReachedAmount)
            );
        }
    }
}