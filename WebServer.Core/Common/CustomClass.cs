﻿namespace WebServer.Core.Common;

public static class CustomClass
{
    public static bool IsCustomClass(Type type)
    {
        return type.IsClass &&
               type is { IsPrimitive: false, IsValueType: false } &&
               type != typeof(string) &&
               !type.IsArray &&
               !typeof(Delegate).IsAssignableFrom(type);
    }
}