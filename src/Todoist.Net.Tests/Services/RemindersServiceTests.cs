using System;
using System.Linq;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public class RemindersServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public RemindersServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task CreateDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var taskId = await transaction.Tasks.AddAsync(new AddTask("Temp"));
            var reminder = new Reminder(taskId) { DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1)) };
            await transaction.Reminders.AddAsync(reminder);
            await transaction.CommitAsync();
            try
            {
                var reminders = await client.Reminders.GetAsync();

                Assert.Contains(reminders, r => r.Id == reminder.Id);
            }
            finally
            {
                await client.Reminders.DeleteAsync(reminder.Id);
                await client.Tasks.DeleteAsync(taskId);
            }
        }

        [Fact]
        public async Task AddRelativeReminder_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = new AddTask("Test")
            {
                DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1))
            };

            var taskId = await client.Tasks.AddAsync(task);
            try
            {
                var user = await client.Users.GetCurrentAsync();
                var reminder = new Reminder(taskId)
                {
                    MinuteOffset = 60,
                    NotifyUid = user.Id
                };

                var reminderId = await client.Reminders.AddAsync(reminder);

                Assert.NotNull(reminderId.PersistentId);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }
    }
}
