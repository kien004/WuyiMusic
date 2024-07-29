using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.IReponsitory;
using WuyiServices.IServices;

namespace WuyiServices.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IAllReponsitories<T> _repository;        
            
        
        public Services(IAllReponsitories<T> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            // Sử dụng Task.Run để chạy các phương thức đồng bộ trong một task
            return await Task.Run(() => _repository.GetAll());
        }

        public async Task<T> GetByIdAsync(dynamic id)
        {
            return await Task.Run(() => _repository.GetById(id));
        }      
        public async Task<bool> CreateAsync(T obj)
        {
            return await Task.Run(() => _repository.CreatObj(obj));
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            return await Task.Run(() => _repository.UpdateObj(obj));
        }

        public async Task<bool> DeleteAsync(T obj)
        {
            return await Task.Run(() => _repository.DeleteObj(obj));
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await Task.Run(() => _repository.UsernameExists(username));
        }
    }
}
