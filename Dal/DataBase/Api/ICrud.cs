using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DataBase.Api
{


    public interface ICrud<T>
    {
       void  Create(T item);

    Task< List<T> >Get();

     List<T> Search(Func<T, bool> func);

     Task< bool> Delete(T item);

        Task<bool> Update(T item,string n);
    }
}


