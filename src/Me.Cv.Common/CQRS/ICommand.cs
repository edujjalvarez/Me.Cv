using MediatR;

namespace Me.Cv.Common.CQRS;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
