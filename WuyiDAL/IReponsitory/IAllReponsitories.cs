using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.IReponsitory
{
    public interface IAllReponsitories<T> where T : class
    {
        //read(get) lấy
        public ICollection<T> GetAll();
        public T GetById(dynamic id);// lấy theo ID
        public T GetByTwoId(dynamic id1, dynamic id2);// lấy theo 2 ID
        //create - Tạo mới
        public bool CreatObj(T obj);//tạo mới đối tượng trong CSDL truyền vào cả đối tượng
        //update-sửa đối tượng trong DB
        public bool UpdateObj(T obj);
        //delete -xóa đối tượng trong DB
        public bool DeleteObj(T obj);
        bool UsernameExists(string username);
        
    }
}
