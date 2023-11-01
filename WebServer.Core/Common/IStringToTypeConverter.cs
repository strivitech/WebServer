namespace WebServer.Core.Common;

public interface IStringToTypeConverter
{
    object Convert(string value, Type targetType);
}