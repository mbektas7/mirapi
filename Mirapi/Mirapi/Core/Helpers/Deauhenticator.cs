using Mirapi.Core.Domain;
using Mirapi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Core.Helpers
{
    public class Deauhenticator
    {
        public UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());

        public bool BlackListToken(string tokenHash)
        {
            //var accesToken = Request.Headers["Authorization"];
            //kullanıcnın son bilinen durumunu döndermeli
            //bu durumda kullanıcının bilinen son durumu değiştirilmeli 

            return true;
        }
    }
}
