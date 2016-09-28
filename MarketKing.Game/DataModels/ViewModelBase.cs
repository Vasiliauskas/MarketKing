using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MarketKing.Game.DataModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExp;
            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExp = body;
                memberExp = (MemberExpression)unaryExp.Operand;
            }
            else
            {
                memberExp = (MemberExpression)lambda.Body;
            }
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(memberExp.Member.Name);
        }
    }
}
