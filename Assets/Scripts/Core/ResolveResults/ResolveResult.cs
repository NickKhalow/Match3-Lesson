#nullable enable

using System.Collections;
using System.Collections.Generic;
using Core.ValueObjects;

namespace Core.ResolveResults
{

    public class ResolveResult : IReadOnlyResolveResult
    {
        private readonly Dictionary<IPiece, ChangeInfo> changes = new Dictionary<IPiece, ChangeInfo>();

        public void ApplyChange(IPiece piece, ChangeInfo changeInfo)
        {
            changes[piece] = changeInfo;
        }

        public ChangeInfo? ChangeInfo(IPiece piece)
        {
            return changes.TryGetValue(piece, out var changeInfo)
                ? changeInfo
                : null;
        }

        public IEnumerator<ChangeInfo> GetEnumerator()
        {
            return changes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}