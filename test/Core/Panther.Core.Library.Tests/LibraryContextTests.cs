using Microsoft.EntityFrameworkCore;
using Panther.Core.Library.Tests.TestData;
using Panther.Core.Models;
using System.Threading.Tasks;
using Xunit;

namespace Panther.Core.Library.Tests
{
    public class LibraryContextTests
    {
        private readonly LibraryDbContext _context;

        public LibraryContextTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("library.db");
            _context = new LibraryDbContext(optionsBuilder.Options);
        }

        [Theory]
        [MemberData(nameof(LibraryContextTestData.TestData), MemberType = typeof(LibraryContextTestData))]
        public async Task Add_Entity_IncreaseRecord<TEntity>(TEntity entity)
            where TEntity : class, IIdentificable
        {
            var previousCount = await CountEntities<TEntity>();
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            var currentCount = await CountEntities<TEntity>();

            Assert.Equal(previousCount + 1, currentCount);
        }

        private Task<int> CountEntities<TEntity>()
            where TEntity : class, IIdentificable => _context.Set<TEntity>().CountAsync();
    }
}
