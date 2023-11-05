#nullable enable

namespace Core.Conditions.LevelConditions
{

    public static class LevelConditionExtension
    {

        public static bool IsFinish(this ILevelConditions levelConditions)
        {
            return levelConditions.IsWin() || levelConditions.IsLose();
        }
    }
}