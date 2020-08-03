using System.Linq;
using TestCase.Domain;

namespace TestCase.Infrastructure.QueryHandlers.Users
{
	internal static class QueryableFilterExtensions
	{
		public static IQueryable<User> ByUsername(this IQueryable<User> query,
			string value)
		{
			if (string.IsNullOrEmpty(value))
				return query;

			return query.Where(c => c.UserName == value);
		}

		public static IQueryable<User> ByEmail(this IQueryable<User> query,
			string value)
		{
			if (string.IsNullOrEmpty(value))
				return query;

			//var email = value.ToLowerInvariant().Trim();
			//return query.Where(c => c.Email.ToLowerInvariant() == email);
			// EF Core 3.1 error

			return query.Where(c => c.Email == value);
		}
	}
}
