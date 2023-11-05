#nullable enable

using System;
using Core.Boards;
using Core.Conditions.LevelConditions;
using Core.ResolveResults;

namespace Core.Levels
{

    public class Level : ILevel
    {

        private readonly IBoard board;
        private readonly ILevelConditions levelConditions;

        public Level(IBoard board, ILevelConditions levelConditions)
        {
            this.board = board;
            this.levelConditions = levelConditions;
        }

        public ILevelConditions Conditions()
        {
            return levelConditions;
        }

        public IReadOnlyBoard Board()
        {
            return board;
        }

        public ResolveResult Resolve(int x, int y)
        {
            if (Conditions().IsFinish())
            {
                throw new Exception("Level is finished, can't resolve anymore");
            }

            return board.Resolve(x, y);
        }
    }
}