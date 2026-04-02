using System.Reflection;

namespace Application;

public static class AssemblyProvider
{
    public static Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}