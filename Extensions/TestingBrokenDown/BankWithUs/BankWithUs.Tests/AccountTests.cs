using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BankWithUs.Controllers;
using BankWithUs.Data;
using BankWithUs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Xunit;

namespace BankWithUs.Tests
{
    //public class MockContext : IBankContext
    //{
    //    public IQueryable<Account> Accounts { get => new List<Account>().AsQueryable(); set => throw new NotImplementedException(); }

    //    public EntityEntry Add(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void AddEntity<TEntity>(TEntity entity) where TEntity : class
    //    {
            
    //    }

    //    public void Remove(Account account)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        return Task.FromResult(1);
    //    }

    //    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public EntityEntry Update(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class AccountTests
    {
        [Fact]
        public async Task Test_Account_Create()
        {
            // Assemble
            var mock = new Moq.Mock<IBankContext>();
            mock.Setup(context => context.SaveChangesAsync(Moq.It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mock.SetupGet(context => context.Accounts).Returns(new List<Account>().AsQueryable());
            AccountsController controller = new AccountsController(mock.Object);
            
            // Act
            IActionResult result = await controller.Create(new Account() { Name = "Test", SSN = "078-05-1120" });

            // Assert
            Assert.True(result is ViewResult, "Assert 1");
            var response = (result as ViewResult);
            Assert.True(response.Model is Account, "Assert 2");
            var responseItem = response.Model as Account;
            Assert.True(responseItem.Name == "Test", "Assert 3");
            Assert.True(responseItem.SSN == "078-05-1120", "Assert 4");
            Assert.True(responseItem.DateCreated != null, "Assert 5");
        }


        [Fact]
        public async Task Test_Account_Create_Fails()
        {
            // Assemble
            var existing = new Account() { Name = "Test1", SSN = "078-05-1120" };
            var mock = new Moq.Mock<IBankContext>();
            mock.SetupGet(context => context.Accounts).Returns(new List<Account>(){ existing }.AsQueryable());
            AccountsController controller = new AccountsController(mock.Object);

            // Act
            IActionResult result = await controller.Create(existing);

            // Assert
            Assert.True(result is StatusCodeResult, "Assert 1");
            
        }
    }
}
