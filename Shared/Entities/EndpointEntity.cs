namespace Shared.Entities;

public class EndpointEntity(
    string namespaceBase,
    string name,
    string pathMain,
    bool fluentResult = false)
{
    public string NamespaceBase { get; private set; } = namespaceBase;
    public string Name  { get; private set; } = name;
    public string PathMain { get; private set; } = pathMain;
    public bool FluentResult { get; private set; } = fluentResult;
}