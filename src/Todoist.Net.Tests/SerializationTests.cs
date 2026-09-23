using System.Text.Json;

namespace Todoist.Net.Tests;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class SerializationTests
{
    private static readonly JsonSerializerOptions SerializerOptions = TodoistClient.SerializerOptions;

    [Fact]
    public void AddSection_WithSectionOrder_WritesTheRestApiOrderAlias()
    {
        var section = new AddSection("Section", "6X7rM8997g3RQmvh", sectionOrder: 3);

        var json = JsonSerializer.Serialize(section, SerializerOptions);

        Assert.Contains("\"section_order\":3", json);
        Assert.Contains("\"order\":3", json);
    }

    [Fact]
    public void KarmaGoalsInfo_WithNullFlags_DeserializesThemAsFalse()
    {
        const string json = """
                            {
                                "user_id": "42",
                                "daily_goal": 5,
                                "vacation_mode": null,
                                "karma_disabled": null
                            }
                            """;

        var karmaGoals = JsonSerializer.Deserialize<KarmaGoalsInfo>(json, SerializerOptions);

        Assert.NotNull(karmaGoals);
        Assert.Equal("42", karmaGoals.UserId);
        Assert.False(karmaGoals.VacationMode);
        Assert.False(karmaGoals.KarmaDisabled);
    }

    [Fact]
    public void Deadline_WithNullDate_DeserializesToTheDefaultDate()
    {
        var deadline = JsonSerializer.Deserialize<Deadline>("""{ "date": null }""", SerializerOptions);

        Assert.NotNull(deadline);
        Assert.Equal(default, deadline.Date);
    }

    [Fact]
    public void ComplexId_UsedAsADictionaryKey_RoundTrips()
    {
        var expected = new Dictionary<ComplexId, int> { { "6X7rM8997g3RQmvh", 1 } };

        var json = JsonSerializer.Serialize(expected, SerializerOptions);
        var actual = JsonSerializer.Deserialize<Dictionary<ComplexId, int>>(json, SerializerOptions);

        Assert.NotNull(actual);
        var actualPair = Assert.Single(actual);
        Assert.Equal("6X7rM8997g3RQmvh", actualPair.Key.PersistentId);
        Assert.Equal(1, actualPair.Value);
    }

    [Fact]
    public void NotificationSettings_WithKnownNotificationType_DeserializesTheSetting()
    {
        const string json = """
                            {
                                "settings_notifications": {
                                    "item_completed": { "notify_email": true, "notify_push": false }
                                }
                            }
                            """;

        var response = JsonSerializer.Deserialize<SyncResourcesResponse>(json, SerializerOptions);

        Assert.NotNull(response);
        Assert.NotNull(response.NotificationSettings);
        var setting = response.NotificationSettings[NotificationType.ItemCompleted];
        Assert.True(setting.NotifyEmail);
        Assert.False(setting.NotifyPush);
    }

    [Fact]
    public void NotificationSettings_WithUnknownNotificationType_ThrowsJsonExceptionNamingTheValue()
    {
        const string json = """
                            {
                                "settings_notifications": {
                                    "not_a_known_notification_type": { "notify_email": true, "notify_push": false }
                                }
                            }
                            """;

        var exception = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<SyncResourcesResponse>(json, SerializerOptions));

        Assert.Contains("not_a_known_notification_type", exception.Message);
    }

    [Fact]
    public void TodoistErrorExtra_WithLargeWorkspaceId_Deserializes()
    {
        var json = $$"""{ "workspace_id": {{(long)int.MaxValue + 1}} }""";

        var errorExtra = JsonSerializer.Deserialize<TodoistErrorExtra>(json, SerializerOptions);

        Assert.NotNull(errorExtra);
        Assert.Equal((long)int.MaxValue + 1, errorExtra.WorkspaceId);
    }

    [Fact]
    public void WorkspaceFolder_WithOrderKeyAndProjectIds_WritesThemButNotTheFieldsOnlyTheApiSets()
    {
        var folder = new WorkspaceFolder("Folder")
        {
            DefaultOrderKey = "a0",
            AddProjectIds = ["6X7rM8997g3RQmvh"]
        };

        var json = JsonSerializer.Serialize(folder, SerializerOptions);

        Assert.Contains("\"default_order_key\":\"a0\"", json);
        Assert.Contains("\"add_project_ids\":[\"6X7rM8997g3RQmvh\"]", json);
        Assert.DoesNotContain("remove_project_ids", json);
        Assert.DoesNotContain("child_order", json);
        Assert.DoesNotContain("is_deleted", json);
    }

    [Fact]
    public void UpdateTask_WithOnlyAnOrderKey_WritesOnlyTheIdAndTheKey()
    {
        var task = new UpdateTask("6X7rM8997g3RQmvh") { OrderKey = "a1V" };

        var json = JsonSerializer.Serialize(task, SerializerOptions);

        Assert.Equal("""{"order_key":"a1V","id":"6X7rM8997g3RQmvh"}""", json);
    }

    [Fact]
    public void SyncResources_WithOrderKeys_ReadsThem()
    {
        const string json = """
                            {
                                "items": [{ "id": "1", "order_key": "a1" }],
                                "sections": [{ "id": "2", "order_key": "a2" }],
                                "projects": [{ "id": "3", "order_key": null, "default_order_key": "a3" }],
                                "folders": [{ "id": "4", "default_order_key": "a4" }],
                                "labels": [{ "id": "5", "order_key": "a5" }],
                                "filters": [{ "id": "6", "order_key": "a6" }],
                                "workspace_filters": [{ "id": "7", "order_key": "a7" }]
                            }
                            """;

        var resources = JsonSerializer.Deserialize<SyncResourcesResponse>(json, SerializerOptions);

        Assert.NotNull(resources);

        var task = Assert.Single(resources.Tasks);
        Assert.Equal("a1", task.OrderKey);

        var section = Assert.Single(resources.Sections);
        Assert.Equal("a2", section.OrderKey);

        var project = Assert.Single(resources.Projects);
        Assert.Null(project.OrderKey);
        Assert.Equal("a3", project.DefaultOrderKey);

        var folder = Assert.Single(resources.WorkspaceFolders);
        Assert.Equal("a4", folder.DefaultOrderKey);

        var label = Assert.Single(resources.Labels);
        Assert.Equal("a5", label.OrderKey);

        var filter = Assert.Single(resources.Filters);
        Assert.Equal("a6", filter.OrderKey);

        var workspaceFilter = Assert.Single(resources.WorkspaceFilters);
        Assert.Equal("a7", workspaceFilter.OrderKey);
    }
}
