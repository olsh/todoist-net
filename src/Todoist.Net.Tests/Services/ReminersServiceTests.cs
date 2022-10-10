using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [IntegrationPremium]
    public class ReminersServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ReminersServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void CreateDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

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
