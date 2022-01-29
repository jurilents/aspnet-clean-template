using System.Reflection;

namespace CleanTemplate.Domain.Core.Tools;

public static class AssemblyProvider
{
	private static readonly IList<Assembly> LoadedAssemblies = new List<Assembly>();

	public static Func<Assembly, bool> IsNonSystemAssembly = asm =>
			asm.FullName != null
			&& !asm.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase)
			&& !asm.FullName.StartsWith("Newtonsoft", StringComparison.OrdinalIgnoreCase)
			&& !asm.FullName.StartsWith("JetBrains", StringComparison.OrdinalIgnoreCase)
			&& !asm.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase);


	public static void AddAssembly(Assembly assembly)
	{
		LoadedAssemblies.Add(assembly);
	}

	public static void AddAssembly(string assemblyName)
	{
		AddAssembly(Assembly.Load(assemblyName));
	}

	public static IEnumerable<Assembly> GetAssemblies(string assemblyName) => GetCurrentDomainAssemblies(assemblyName).ToList();

	public static IEnumerable<Assembly> GetAssemblies() => GetCurrentDomainAssemblies().ToList();

	public static IEnumerable<Type> GetTypes() => GetCurrentDomainAssemblies().SelectMany(a => a.GetTypes()).ToList();
	public static IEnumerable<Type> GetTypes(string assemblyName) => GetCurrentDomainAssemblies(assemblyName).SelectMany(a => a.GetTypes()).ToList();

	public static IEnumerable<Type> GetImplementations<TBase>()
	{
		Type baseType = typeof(TBase);

		return GetTypes()
				.Where(t => t != baseType && baseType.IsAssignableFrom(t))
				.ToList();
	}


	public static IEnumerable<Type> GetImplementations<TBase>(string namespaceName)
	{
		Type baseType = typeof(TBase);

		return GetTypes()
				.Where(t => t != baseType && t.Namespace == namespaceName && baseType.IsAssignableFrom(t))
				.Distinct()
				.ToList();
	}

	private static IEnumerable<Assembly> GetCurrentDomainAssemblies() =>
			LoadAssemblies().Where(IsNonSystemAssembly);

	private static IEnumerable<Assembly> GetCurrentDomainAssemblies(string assemblyName) =>
			LoadAssemblies().Where(IsNonSystemAssembly).Where(a => a.FullName!.StartsWith(assemblyName, StringComparison.OrdinalIgnoreCase));

	public static IEnumerable<Assembly> LoadAssemblies()
	{
		var list = new List<string>();
		var stack = new Stack<Assembly>();

		stack.Push(Assembly.GetEntryAssembly()!);

		foreach (Assembly assembly in LoadedAssemblies)
			stack.Push(assembly);

		do
		{
			Assembly asm = stack.Pop();
			// Console.WriteLine("asm:: " + asm.GetName());
			yield return asm;

			foreach (AssemblyName reference in asm.GetReferencedAssemblies().Where(a => !list.Contains(a.FullName)))
			{
				stack.Push(Assembly.Load(reference));
				list.Add(reference.FullName);
			}
		} while (stack.Count > 0);
	}
}