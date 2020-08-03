using MediatR;
using System.Runtime.Serialization;

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
