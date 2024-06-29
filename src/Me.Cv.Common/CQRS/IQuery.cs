using MediatR;

namespace Me.Cv.Common.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}
