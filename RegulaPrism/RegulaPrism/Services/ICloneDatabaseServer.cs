using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public interface ICloneDatabaseServer
    {
        void CloneDatabase(IRegulaApiService regulaApiService);
    }
}
