using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Simon8029.EMPDemo.Model;
using Simon8029.EMPDemo.Repository;

namespace Simon8029.EMPDemo.RepositoryTests
{
    class LeadsRepositoryTest
    {
        [Test]
        public void AddTest()
        {
            EM_LeadsRepository target = new EM_LeadsRepository();
            EM_Leads entity = null;
            entity = new EM_Leads();
            entity.FirstName = "abc";
            entity.LastName = "def";
            entity.EmailAddress = "abc3@def.com";
            entity.IsValid = true;
            entity.Unsubscribed = false;
            target.Add(entity);
            DbContextFactory.GetDbContext().SaveChanges();
            EM_Leads addedEntity = target.Get(e => e.FirstName == "abc").FirstOrDefault();
            Assert.That(addedEntity.FirstName, Is.EqualTo(entity.FirstName));

        }
    }
}
