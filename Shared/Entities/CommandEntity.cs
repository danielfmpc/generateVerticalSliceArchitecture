namespace Shared.Entities;

public class CommandEntity(
    string namespaceBase,
    string name,
    string pathMain,
    bool fluentResult = false)
{
    public string Type { get; private set; }
    public string Name  { get; private set; } = name;
    public string PathMain { get; private set; } = pathMain;
    public bool FluentResult { get; private set; } = fluentResult;
    public string NamespaceBase { get; private set; } = namespaceBase;
    
    public void SetType(string type)
    {
        Type = type;
    }
}