namespace Library.Tools.Linq
{
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;

	public static class MyOrderBy
	{
		#region Public METHODS

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string iPropertyName, ListSortDirection iSortDirection)
		{
			string orderString = "OrderBy";
			if (ListSortDirection.Descending == iSortDirection)
				orderString = "OrderByDescending";

			var type = typeof(T);
			var property = type.GetProperty(iPropertyName);
			var parameter = Expression.Parameter(type, "p");
			var propertyAccess = Expression.MakeMemberAccess(parameter, property);
			var orderByExp = Expression.Lambda(propertyAccess, parameter);
			MethodCallExpression resultExp = Expression.Call(typeof(Queryable), orderString, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
			return source.Provider.CreateQuery<T>(resultExp);
		}

		#endregion Public METHODS
	}
}