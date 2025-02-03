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

            var itemId = await transaction.Items.AddAsync(new AddItem("Temp"));
            var reminder = new Reminder(itemId) { DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1)) };
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
                await client.Items.DeleteAsync(itemId);
            }
        }

        [Fact]
        public async Task AddRelativeReminder_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new AddItem("Test")
            {
                DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1))
            };

            var taskId = await client.Items.AddAsync(item);
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
                await client.Items.DeleteAsync(item.Id);
            }
        }
    }
}
