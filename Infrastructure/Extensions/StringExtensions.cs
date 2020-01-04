namespace Infrastructure.Extensions
{
    /// <summary>
    /// Contains extensions for the <see cref="string"/> data type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks the <paramref name="input"/> string for null or whitespace.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}