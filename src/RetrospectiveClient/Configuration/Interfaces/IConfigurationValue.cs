namespace RetrospectiveClient.Configuration.Interfaces
{
    public interface IConfigurationValue
    {
        string Value { get; set; }
        bool IsRequired { get; }
        bool IsRequiredAndMissingValue { get; }
        string Key { get; }
        string DisplayName { get; }
    }
}