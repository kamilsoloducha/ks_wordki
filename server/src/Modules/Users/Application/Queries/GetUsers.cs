using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Users.Domain;

namespace Users.Application
{
    public class GetUsers
    {
        public class QueryHandler : RequestHandlerBase<Query, Response>
        {
            private readonly IUserRepository _userRepository;

            public QueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async override Task<ResponseBase<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetUsers(cancellationToken);
                return ResponseBase<Response>.Create(new Response
                {
                    Users = users
                });
            }
        }

        public class Query : RequestBase<Response> { }
        public class Response
        {
            public IEnumerable<User> Users { get; set; }
        }

    }
}