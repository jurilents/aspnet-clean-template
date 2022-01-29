using CleanTemplate.Infrastructure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanTemplate.Infrastructure;

[Inject(ServiceLifetime.Singleton, InjectionType.Self)]
public class RandomCodeGenerator
{
	private const string DevelopmentCode = "1234";

	private readonly IWebHostEnvironment _environment;
	private readonly Random _random;

	public RandomCodeGenerator(IWebHostEnvironment environment)
	{
		_environment = environment;
		_random = new Random();
	}

	public string Generate()
	{
		if (_environment.IsDevelopment())
			return DevelopmentCode;

		int code = _random.Next(1_0000);
		return code.ToString("0000");
	}
}