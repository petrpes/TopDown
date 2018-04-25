using System;
using System.Reflection;

public static class SystemExtentions
{
    public static Type FindType(string typeName, string assemblyQualifiedName)
    {
        Type type = Type.GetType(typeName);

        if (type != null)
        {
            return type;
        }

        if (assemblyQualifiedName != "")
        {
            type = Type.GetType(assemblyQualifiedName);
            if (type != null)
            {
                return type;
            }
        }
        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = asm.GetType(typeName);
            if (type != null)
            {
                return type;
            }
        }
        return null;
    }
}

