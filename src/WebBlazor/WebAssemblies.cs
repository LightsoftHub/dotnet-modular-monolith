using ModularMonolith.Modules.Users.WebComponents;
using System.Reflection;

namespace ModularMonolith.WebBlazor;

public class WebAssemblies
{
    public static readonly Assembly[] Assemblies =
    [
        typeof(UserWebModule).Assembly,
    ];
}
