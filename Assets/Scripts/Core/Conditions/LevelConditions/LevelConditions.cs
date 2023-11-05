#nullable enable

namespace Core.Conditions.LevelConditions
{

    public class LevelConditions : ILevelConditions
    {

        private readonly ICondition winCondition;
        private readonly ICondition loseCondition;

        public LevelConditions(ICondition winCondition, ICondition loseCondition)
        {
            this.winCondition = winCondition;
            this.loseCondition = loseCondition;
        }

        public bool IsWin()
        {
            return winCondition.IsMet();
        }

        public bool IsLose()
        {
            return loseCondition.IsMet();
        }
    }
}