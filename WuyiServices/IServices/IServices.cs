using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiServices.IServices
{
    public interface IServices<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(dynamic id);
        Task<bool> CreateAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> DeleteAsync(T obj);
        Task<bool> UsernameExistsAsync(string username);
    }
}
