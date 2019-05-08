using SME.Pedagogico.Repository.Context;
using System.Threading.Tasks;

namespace SME.Pedagogico.Repository
{
    public class CRUDRepository<T> where T: class
    {
        public async Task<bool> AddAsync(T entity, ApiContext _db)
        {
            _db.Add(entity);
            return await SaveAsync(_db);
        }

        public bool Add(T entity, ApiContext _db)
        {
            _db.Add(entity);
            return Save(_db);
        }

        public bool Delete(T entity, ApiContext _db)
        {
            _db.Remove(entity);
            return Save(_db);
        }

        private bool Save(ApiContext _db)
        {
            return  _db.SaveChanges() > 0;
        }

        private async Task<bool> SaveAsync(ApiContext _db)
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public bool Update(T entity, ApiContext _db)
        {
            _db.Update(entity);
            return Save(_db);
        }
    }
}
