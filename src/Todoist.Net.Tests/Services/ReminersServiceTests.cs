using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    [IntegrationPremium]
    public class ReminersServiceTests
    {
        [Fact]
        public void CreateDelete_Success()
        {
            var client = CreateClient();

            var transaction = client.CreateTransaction();

            var itemId = transaction.Items.AddAsync(new Item("Temp")).Result;
            var reminderId =
                transaction.Reminders.AddAsync(new Reminder(itemId) { DueDateUtc = DateTime.UtcNow.AddDays(1) }).Result;
            transaction.CommitAsync().Wait();

            var reminderInfo = client.Reminders.GetAsync(reminderId).Result;

            client.Reminders.DeleteAsync(reminderInfo.Reminder.Id).Wait();
        }

        [Fact]
        public void GetReminderInfo_Success()
        {
            var client = CreateClient();

            var filters = client.Reminders.GetAsync().Result;

            Assert.True(filters.Count() > 0);

            var result = client.Reminders.GetAsync(filters.First().Id).Result;

            Assert.True(result != null);
        }

        private static TodoistClient CreateClient()
        {
            var client = new TodoistClient(SettingsProvider.GetToken());
            return client;
        }
    }
}
