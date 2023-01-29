namespace Guardians
{
    public class Ensure
    {
        //entry point for all arguments checks
        public static IArgumentCheck Argument { get; }
        public static void That(bool condition, Func<Exception> exceptionFactory)
        {
            if (exceptionFactory == null)
            {
                throw new ArgumentNullException(nameof(exceptionFactory));
            }

            if (!condition)
            {
                var e = exceptionFactory();
                throw e;
            }
        }
    }
}