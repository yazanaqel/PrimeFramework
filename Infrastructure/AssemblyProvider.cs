using System.Reflection;

namespace Infrastructure;

public static class AssemblyProvider
{
    public static Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}