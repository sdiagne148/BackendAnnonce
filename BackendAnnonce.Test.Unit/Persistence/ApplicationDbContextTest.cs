using BackendAnnonce.Domain.Entities;
using BackendAnnonce.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BackendAnnonce.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var annonce = new Annonce();
            context.Annonces.Add(annonce);
            Assert.AreEqual(EntityState.Added, context.Entry(annonce).State);
        }
    }
}
