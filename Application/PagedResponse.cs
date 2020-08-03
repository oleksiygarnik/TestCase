using System;
using System.Collections.Generic;

namespace Application
{
    public class PagedResponse<TEntityModel>
    {
        public virtual int Total { get; }

        public virtual int Size { get; }

        public int Current { get; }

        public int Last
        {
            get
            {
                var last = Total / Size;

                return Total % Size != 0 ? last + 1 : last;
            }
        }

        public IEnumerable<TEntityModel> Items { get; }

        public PagedResponse(PageOptions options, int total, params TEntityModel[] items)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (total < 0)
                throw new ArgumentOutOfRangeException(nameof(total));

            Size = options.Size;
            Current = options.Number;
            Total = total;
            Items = items;
        }
    }
}
