using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application
{
    public abstract class AbstractQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}