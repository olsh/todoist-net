namespace Todoist.Net.Tests;


[CollectionDefinition(Name)]
public class TodoistApiTestCollection : ICollectionFixture<TodoistApiFixture>
{
    public const string Name = "todoist-api-tests";
}
