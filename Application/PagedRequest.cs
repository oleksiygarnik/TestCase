using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Application
{
    public class PagedRequest<T> : PagedRequest, IRequest<PagedResponse<T>>
    {
    }


    public class PagedRequest
    {
        [DataMember]
        public int? PageNumber { get; set; }

        [DataMember]
        public int? PageSize { get; set; }
    }
}
