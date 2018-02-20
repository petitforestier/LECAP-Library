using System;
using System.Linq.Expressions;

namespace Library.Entity
{
	public static class MyExpression
	{
		#region Public METHODS

		/// <summary>
		/// Retourne une expression avec récupération de la constante, au lieu d'une variable pour faire du query.
		/// </summary>
		public static Expression<Func<TSource, TResult>> EmbedConstant
			<TSource, TResult, TConstant>(
		this Expression<Func<TSource, TConstant, TResult>> expression,
	TConstant constant)
		{
			var body = expression.Body.Replace(
				expression.Parameters[1],
				Expression.Constant(constant));
			return Expression.Lambda<Func<TSource, TResult>>(
				body, expression.Parameters[0]);
		}

		#endregion Public METHODS

		#region Internal CLASSES

		internal class ReplaceVisitor : ExpressionVisitor
		{
			#region Public CONSTRUCTORS

			public ReplaceVisitor(Expression from, Expression to)
			{
				this.from = from;
				this.to = to;
			}

			#endregion Public CONSTRUCTORS

			#region Public METHODS

			public override Expression Visit(Expression node)
			{
				return node == from ? to : base.Visit(node);
			}

			#endregion Public METHODS

			#region Private FIELDS

			private readonly Expression from, to;

			#endregion Private FIELDS
		}

		#endregion Internal CLASSES

		#region Private METHODS

		private static Expression Replace(this Expression expression,
	Expression searchEx, Expression replaceEx)
		{
			return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
		}

		#endregion Private METHODS
	}
}