using System.Reflection;

namespace ModularMonolith.WebBlazor;

public class Web
{
    public const string CompanyName = "Light";

    public const string AppName = "eSystem";

    public static readonly Assembly[] Assemblies = [typeof(Web).Assembly];
}
