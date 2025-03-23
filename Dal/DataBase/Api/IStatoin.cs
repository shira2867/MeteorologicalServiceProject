using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DataBase.models;

namespace Dal.DataBase.Api
{
    public interface IStatoin : ICrud<Station>
    {
       Task< bool> Create1(GetCalculateDatum cal);
        Task< List<GetCalculateDatum>>GetCalculateFullData();


    }
}
