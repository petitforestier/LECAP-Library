namespace Library.Tools.Misc
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	public static class PropertyObserver
	{
		#region Public METHODS

		/// <summary>
		/// Retourne le nom de la propriété en string
		/// </summary>
		/// <typeparam name="TPropertySource"></typeparam>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string GetPropertyName<TPropertySource>(Expression<Func<TPropertySource, object>> expression)
		{
			var lambda = expression as LambdaExpression;
			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression)
			{
				var unaryExpression = lambda.Body as UnaryExpression;
				memberExpression = unaryExpression.Operand as MemberExpression;
			}
			else
			{
				memberExpression = lambda.Body as MemberExpression;
			}

			if (memberExpression != null)
			{
				var propertyInfo = memberExpression.Member as PropertyInfo;

				return propertyInfo.Name;
			}

			return null;
		}

		#endregion Public METHODS
	}
}