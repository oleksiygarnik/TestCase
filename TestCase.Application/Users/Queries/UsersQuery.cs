using Application;
using System.Runtime.Serialization;

namespace TestCase.Application.Users.Queries
{
	[DataContract]
    public class UsersQuery : PagedRequest<UserDto>
    {
		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Email { get; set; }
	}
}
