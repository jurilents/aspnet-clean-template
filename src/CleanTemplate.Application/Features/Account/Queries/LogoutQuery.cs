using MediatR;

namespace CleanTemplate.Application.Features.Account;

public class LogoutQuery : IRequest { }

public class LogoutQueryHandler : IRequestHandler<LogoutQuery>
{
	public async Task<Unit> Handle(LogoutQuery request, CancellationToken cancellationToken)
	{
		return Unit.Value;
	}
}