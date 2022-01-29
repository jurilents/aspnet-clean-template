using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.Infrastructure.Core;

/// <summary>
/// Attribute to simple reference your service class with DI
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal class InjectAttribute : Attribute
{
	/// <summary>
	/// Specifies a DI scope lifetime
	/// </summary>
	public ServiceLifetime Lifetime { get; }

	/// <summary>
	/// Specify injection type
	/// </summary>
	public InjectionType InjectionType { get; }

	/// <summary>
	/// Specify injection base type
	/// </summary>
	public Type? ServiceType { get; set; }


	public InjectAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped, InjectionType type = InjectionType.Interface)
	{
		InjectionType = type;
		Lifetime = lifetime;
	}
}