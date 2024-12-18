using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Desive2.Objects
{
    /// <summary>
    /// An abstract class that extends <see cref="BindableObject"/> to provide custom functionality
    /// for raising property change notifications.
    /// </summary>
    public abstract class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// Raises a property changed notification for the specified property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">A lambda expression representing the property whose change is to be notified.</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        /// <summary>
        /// Extracts the member information from the provided lambda expression.
        /// </summary>
        /// <param name="expression">The expression representing the property or field.</param>
        /// <returns>The <see cref="MemberInfo"/> for the property or field represented by the expression.</returns>
        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body is UnaryExpression)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}

