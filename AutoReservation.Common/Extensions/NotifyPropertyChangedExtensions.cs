using System;
using System.Linq.Expressions;

namespace AutoReservation.Common.Extensions
{
    public static class NotifyPropertyChangedExtensions
    {
        public static void OnPropertyChanged<T, TProperty>(this T obj, Expression<Func<T, TProperty>> expression) 
            where T : IExtendedNotifyPropertyChanged
        {
            if (obj == null)
            {
                return;
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression is not a MemberExpression", "expression");
            }
            obj.OnPropertyChanged(memberExpression.Member.Name);
        }
    }
}
