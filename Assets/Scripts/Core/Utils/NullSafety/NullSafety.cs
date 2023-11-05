#nullable enable

using System;

namespace Core.Utils.NullSafety
{
    public static class NullSafety
    {
        public static T EnsureNotNull<T>(this T? value) where T : class
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            return value;
        }
    }
}