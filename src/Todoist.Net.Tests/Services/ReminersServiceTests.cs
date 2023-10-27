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
    public class ReminersServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ReminersServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task CreateDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var itemId = await transaction.Items.AddAsync(new Item("Temp")).ConfigureAwait(false);
            var reminderId =
                await transaction.Reminders.AddAsync(new Reminder(itemId) { DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1)) }).ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);

            var reminders = await client.Reminders.GetAsync().ConfigureAwait(false);
            Assert.True(reminders.Any());

            var reminderInfo = await client.Reminders.GetAsync(reminderId).ConfigureAwait(false);
            Assert.True(reminderInfo != null);

            await client.Reminders.DeleteAsync(reminderInfo.Reminder.Id).ConfigureAwait(false);
            await client.Items.DeleteAsync(itemId);
        }

        [Fact]
        public async Task AddRelativeReminder_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new Item("Test")
            {
                DueDate = DueDate.CreateFloating(DateTime.UtcNow.AddDays(1))
            };

            var taskId = await client.Items.AddAsync(item).ConfigureAwait(false);

            var user = await client.Users.GetCurrentAsync().ConfigureAwait(false);
            var reminder = new Reminder(taskId)
            {
                MinuteOffset = 60,
                NotifyUid = user.Id
            };

            var reminderId = await client.Reminders.AddAsync(reminder).ConfigureAwait(false);

            Assert.NotNull(reminderId.PersistentId);

            await client.Items.DeleteAsync(item.Id);
        }
    }
}
