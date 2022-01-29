using CleanTemplate.Data.SqlServer.Entities.Identity;
using CleanTemplate.Domain.Abstractions;
using CleanTemplate.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanTemplate.Application.Features.Account;

public class ConfirmEmailQuery : IRequest<bool>
{
	public long UserId { get; set; }
	public string Token { get; set; } = default!;
}

public class Handler : IRequestHandler<ConfirmEmailQuery, bool>
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IRepository<AppUser> _userRepository;

	public Handler(UserManager<AppUser> userManager, IRepository<AppUser> userRepository)
	{
		_userManager = userManager;
		_userRepository = userRepository;
	}

	public async Task<bool> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.UserId);
		if (user is null)
			throw new NotFoundException();

		var result = await _userManager.ConfirmEmailAsync(user, request.Token);
		return result.Succeeded;
	}
}