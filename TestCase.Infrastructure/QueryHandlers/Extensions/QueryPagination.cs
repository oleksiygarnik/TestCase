using Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCase.Infrastructure.QueryHandlers.Extensions
{
    public static class QueryPagination
    {
        public static async Task<PagedResponse<T>> Paginate<T>(this IQueryable<T> query, PagedRequest request, CancellationToken cancellationToken)
        {
            var page = PageOptions.From(request);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page.Number - 1) * page.Size)
                .Take(page.Size)
                .ToArrayAsync(cancellationToken);

            return new PagedResponse<T>(page, total, items);
        }
    }
}
