using System;
using System.Linq.Expressions;

namespace TG.Blazor.IndexedDB
{
    public class StoreSchema<TEntity> : StoreSchema where TEntity : class
    {
        public StoreSchema() : base()
        {
        }

        public StoreSchema Schema => this;

        public StoreSchema<TEntity> SetName(string name)
        {
            Name = name;

            return this;
        }

        public StoreSchema<TEntity> SetKey(
            Expression<Func<TEntity, object>> propertyExpression,
            bool auto = true,
            bool unique = true)
        {
            string key = GetPropertyName(propertyExpression);

            PrimaryKey = new IndexSpec
            {
                Name = FirstToLower(key),
                KeyPath = FirstToLower(key),
                Auto = auto,
                Unique = unique
            };

            return this;
        }

        public StoreSchema<TEntity> AddSpec(
            Expression<Func<TEntity, object>> propertyExpression,
            bool auto = false,
            bool unique = false)
        {
            string name = GetPropertyName(propertyExpression);

            Indexes.Add(new IndexSpec
            {
                Name = FirstToLower(name),
                KeyPath = FirstToLower(name),
                Auto = auto,
                Unique = unique
            });

            return this;
        }

        #region Helpers

        private static string FirstToLower(string input)
        {
            if (input != string.Empty && char.IsUpper(input[0]))
            {
                input = char.ToLower(input[0]) + input.Substring(1);
            }
            return input;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression) where T : class
        {
            LambdaExpression lambda = propertyExpression;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression unaryExpression)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }

        #endregion
    }
}