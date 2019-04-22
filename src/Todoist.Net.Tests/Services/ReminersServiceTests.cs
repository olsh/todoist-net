using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    [IntegrationPremium]
    public class ReminersServiceTests
    {
        [Fact]
        public void CreateDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var transaction = client.CreateTransaction();

            var itemId = transaction.Items.AddAsync(new Item("Temp")).Result;
            var reminderId =
                transaction.Reminders.AddAsync(new Reminder(itemId) { DueDate = new DueDate(DateTime.UtcNow.AddDays(1)) }).Result;
            transaction.CommitAsync().Wait();

            var reminders = client.Reminders.GetAsync().Result;
            Assert.True(reminders.Any());

            var reminderInfo = client.Reminders.GetAsync(reminderId).Result;
            Assert.True(reminderInfo != null);

            client.Reminders.DeleteAsync(reminderInfo.Reminder.Id).Wait();
            client.Items.DeleteAsync(itemId);
        }
    }
}
