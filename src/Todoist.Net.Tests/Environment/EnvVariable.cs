namespace Todoist.Net.Tests;

public struct EnvVariable
{
    public string Name { get; }
    public string? DefaultValue { get; }
    
    public string Value => field ??= (
        Environment.GetEnvironmentVariable(Name) 
            ?? DefaultValue
            ?? throw new InvalidOperationException($"Required `{Name}` environment variable is not set."));

    public EnvVariable(string name, string? defaultValue = null)
    {
        Name = name;
        DefaultValue = defaultValue;
    }

    public static implicit operator string(EnvVariable envVariable) => envVariable.Value;
    public static implicit operator EnvVariable(string name) => new(name);
}
