#nullable enable

using Core.Boards;
using Core.ResolveResults;

namespace Core.ResolvePipe
{

    public class SequenceResolvePipe : IResolvePipe
    {

        private readonly IResolvePipe[] steps;

        public SequenceResolvePipe(params IResolvePipe[] steps)
        {
            this.steps = steps;
        }

        public void ResolveAt(int x, int y, IBoard board, ResolveResult resolveResult)
        {
            foreach (var resolvePipe in steps)
            {
                resolvePipe.ResolveAt(x, y, board, resolveResult);
            }
        }
    }
}