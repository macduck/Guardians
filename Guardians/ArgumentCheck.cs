using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Guardians
{
    public static class ArgumentCheck
    {
        public static void NotNull<T>(this IArgumentCheck argument, [NotNull] T? val, [CallerArgumentExpression(nameof(val))] string? argumentName = null)
        //    where T : class
        //{
        //    if (val == null) 
        //    {
        //        throw new ArgumentNullException(argumentName, "Argument is null.");
        //    }
        //}

        //public static void NotNull<T>(this IArgumentCheck argument, [NotNull] T? val, [CallerArgumentExpression(nameof(val))] string? argumentName = null)
        //    where T : struct
        {
            if (val == null)
            {
                throw new ArgumentNullException(argumentName, $"Argument is null.");
            }
        }

        public static void NotNullOrEmpty(this IArgumentCheck argument, [NotNull] string? str, [CallerArgumentExpression(nameof(str))] string? argumentName = null)
        {
            if (str == null)
            {
                throw new ArgumentNullException(argumentName, $"Argument is null.");
            }

            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"Argument is empty.", argumentName);
            }
        }

        public static void NotNullOrEmpty(this IArgumentCheck argument, [NotNull] IEnumerable? enumerable, [CallerArgumentExpression(nameof(enumerable))] string? argumentName = null)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(argumentName, $"Argument is null.");
            }

            var hasElement = enumerable.GetEnumerator().MoveNext();
            if (!hasElement)
            {
                throw new ArgumentException($"Argument is empty.", argumentName);
            }
        }

        public static void HasNoNulls<T>(this IArgumentCheck argument, [NotNull] IEnumerable<T?>? enumerable, [CallerArgumentExpression(nameof(enumerable))] string? argumentName = null)
        {
            argument.NotNull(enumerable, argumentName);
            var i = 0;
            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    throw new ArgumentException($"Argument has null elements. (Position {i})", argumentName);
                }
                i++;
            }
        }
        

        public static void NotEmpty(this IArgumentCheck argument, Guid guid, [CallerArgumentExpression(nameof(guid))] string? argumentName = null)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException($"Argument is empty.", argumentName);
            }
        }

        public static void IsPositive(this IArgumentCheck argument, int num, [CallerArgumentExpression(nameof(num))] string? argumentName = null)
        {
            if (num <= 0) 
            {
                throw new ArgumentOutOfRangeException(argumentName, "Argument is not positive.");
            }
        }

        public static void MeetsCondition(this IArgumentCheck argument, bool condition, [CallerArgumentExpression(nameof(condition))] string? conditionExpression = "condition")
        {
            if (!condition)
            {
                throw new ArgumentException($"Arguments condition was not met. (Condition '{conditionExpression}')");
            }
        }

        public static void IsOfType<TExpected>(this IArgumentCheck argument, [NotNull] object? val, [CallerArgumentExpression(nameof(val))] string? argumentName = null)
        {
            argument.NotNull(val, argumentName);

            if (val is not TExpected)
            {
                throw new ArgumentException($"Argument is of incompatible type. (Type '{val.GetType()}')", argumentName);
            }
        }
    }
}
