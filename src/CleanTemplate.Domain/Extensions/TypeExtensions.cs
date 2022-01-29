using System.Reflection;

namespace CleanTemplate.Domain.Extensions;

public static class TypeExtensions
{
	public static TAttribute? GetAttribute<TAttribute>(this Type type)
	{
		Attribute? attr = type.GetCustomAttribute(typeof(TAttribute));
		return attr is null ? default : (TAttribute) (object) attr;
	}

	public static TAttribute GetRequiredAttribute<TAttribute>(this Type type)
	{
		Attribute? attr = type.GetCustomAttribute(typeof(TAttribute));
		if (attr is null)
			throw new TypeLoadException($"Attribute {typeof(TAttribute).Name} for type {type.Name} not found.");

		return (TAttribute) (object) attr;
	}
}