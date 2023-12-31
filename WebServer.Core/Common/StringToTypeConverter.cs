﻿namespace WebServer.Core.Common;

internal class StringToTypeConverter : IStringToTypeConverter
{
    public object Convert(string value, Type targetType)
    {
        try
        {
            return System.Convert.ChangeType(value, targetType);
        }
        catch (InvalidCastException)
        {
            throw new InvalidOperationException($"Unsupported type {targetType}");
        }
    }
}