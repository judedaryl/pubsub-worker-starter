using Dapper;
using PubSubWorkerStarter.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PubSubWorkerStarter.Data.DataManager
{
    internal class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> CreateAsync(User model)
        {
            var query = @"
                insert into user(firstname, lastname) 
                values(@firstname, @lastname)   
                returning user_id;
            ";

            /// Queries are scoped into transactions
            var id = await _unitOfWork.QueryFirstOrDefaultAsync<long>(query, model);

            /// You can either save changes
            _unitOfWork.SaveChanges();

            /// Or you can rollback
            /// _unitOfWork.Rollback();

            return id;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            string query = $@"delete from user where user_id = @id";
            var result = await _unitOfWork.ExecuteScalarAsync(query, new { id });
            _unitOfWork.SaveChanges();
            return result;
        }

        public Task<User> GetAsync(long id)
        {
            string query = $@"select * from user where user_id = @id";
            return _unitOfWork.QueryFirstOrDefaultAsync<User>(query, new { id });
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            string query = $@"select * from user";
            return _unitOfWork.QueryAsync<User>(query);
        }

        public async Task<bool> UpdateAsync(long id, User model)
        {
            var exists = await ExistAsync(id);
            if (!exists) return false;
            var query = $@"
                update user
                set firstname = @firstname, lastname = @lastname
                where cgmid = @id
            ";
            var dp = new DynamicParameters(model);
            dp.Add("id", id);
            await _unitOfWork.ExecuteScalarAsync(query, dp);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Task<bool> ExistAsync(long id)
        {
            return _unitOfWork.ExecuteScalarAsync("select count(1) from user where user_id = @id", new { id });
        }
    }
}
