using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace HubBrowser.Core.Extensions
{
    public static class NotifyPropertyChangedExtensions
    {
        public static void OnChanged<TViewModel, TProperty>(this TViewModel viewModel, Expression<Func<TViewModel, TProperty>> property, Action onChanged)
            where TViewModel : INotifyPropertyChanged
        {
            var name = property.GetMemberInfo().Name;

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != name)
                    return;

                onChanged();
            };
        }

        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}
