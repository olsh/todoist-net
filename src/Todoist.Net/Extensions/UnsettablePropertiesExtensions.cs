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
        /// Specifies that the targeted property should be included in the outgoing requests with no value (<see langword="null"/>).
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IUnsettableProperties"/> entity.</typeparam>
        /// <typeparam name="TProp">The type of the targeted property.</typeparam>
        /// <param name="entity">The entity to unset its property.</param>
        /// <param name="propertyExpression">A lambda expression representing the property to be unset (x => x.MyProperty).</param>
        /// <exception cref="ArgumentException"><paramref name="propertyExpression"/> is not a lambda for property.</exception>
        public static void Unset<T, TProp>(this T entity, Expression<Func<T, TProp>> propertyExpression)
            where T : IUnsettableProperties
            where TProp : class
        {
            var propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo
                ?? throw new ArgumentException($"Invalid property expression: ({propertyExpression})", nameof(propertyExpression));

            // Make sure that the property is null or default.
            propertyInfo.SetValue(entity, null);

            entity.UnsetProperties.Add(propertyInfo);
        }
    }
}
