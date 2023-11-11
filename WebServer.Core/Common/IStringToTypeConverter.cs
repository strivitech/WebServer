namespace WebServer.Core.Common;

internal interface IStringToTypeConverter
{
    object Convert(string value, Type targetType);
}