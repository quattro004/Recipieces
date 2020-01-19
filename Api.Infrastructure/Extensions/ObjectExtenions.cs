using System;

namespace Api.Infrastructure.Extensions
{
    /// <summary>
    /// Contains extensions for the <see cref="object" /> data type.
    /// </summary>
    public static class ObjectExtenions
    {
        /// <summary>
        /// Throws an exception if the <paramref name="objectRef" /> is null.
        /// </summary>
        /// <param name="objectRef">Object reference to check.</param>
        /// <param name="objectName">Name of the object used in the exception message.</param>
        /// <returns><paramref name="objectRef" /></returns>
        /// <exception type="ArgumentNullException">Thrown when the <paramref name="objectRef" /> is null.</exception>
        public static object ThrowIfNull(this object objectRef, string objectName)
        {
            if (null == objectRef)
            {
                throw new ArgumentNullException(nameof(objectName));
            }

            return objectRef;
        }
    }
}