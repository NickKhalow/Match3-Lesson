#nullable enable

using Core.Boards;
using Core.Conditions.LevelConditions;
using Core.ResolveResults;

namespace Core.Levels
{
    public interface ILevel
    {

        ILevelConditions Conditions();

        IReadOnlyBoard Board();

        ResolveResult Resolve(int x, int y);
    }
}