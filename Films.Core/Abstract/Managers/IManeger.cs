using System.Threading.Tasks;

namespace Films.Core.Abstract.Managers
{
    public interface IManager
    {
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}