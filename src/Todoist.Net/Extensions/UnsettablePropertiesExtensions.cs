using System;
using System.Linq.Expressions;
using System.Reflection;

using Todoist.Net.Models;

namespace Todoist.Net
{
    /// <summary>
    /// Provides extension methods to the <see cref="IUnsettableProperties"/> interface.
    /// </summary>
    public static class UnsettablePropertiesExtensions
    {
        /// <summary>
        /// Specifies that the targeted property should be included in the outgoing requests with no value (<see langword="null"/>)
        /// </summary>
        /// <remarks>
        /// This method can be used as follows:
        /// <code>
        /// item.Unset(x => x.DueDate);
        /// </code>
        /// </remarks>
        /// <typeparam name="T">The type of the <see cref="IUnsettableProperties"/> entity.</typeparam>
        /// <typeparam name="TProp">The type of the targeted property.</typeparam>
        /// <param name="entity">The entity to unset its property.</param>
        /// <param name="propertyExpression">A lambda expression representing the property to be unset (x => x.MyProperty).</param>
        /// <exception cref="InvalidOperationException"><typeparamref name="TProp"/> is not a nullable property.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyExpression"/> is not a lambda for property.</exception>
        public static void Unset<T, TProp>(this T entity, Expression<Func<T, TProp>> propertyExpression) where T : IUnsettableProperties
        {
            // We use runtime check instead of generic constraints to allow both reference and Nullable<T> types.
            if (default(TProp) != null)
            {
                throw new InvalidOperationException("Property type is required to be a nullable type.");
            }
            var propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo
                ?? throw new ArgumentException($"Invalid property expression: ({propertyExpression})", nameof(propertyExpression));

            // If the property doesn't have an internal setter.
            if (!propertyInfo.SetMethod.IsAssembly)
            {
                // Make sure that the property is null.
                propertyInfo.SetValue(entity, null);
            }

            entity.UnsetProperties.Add(propertyInfo);
        }
    }
}
