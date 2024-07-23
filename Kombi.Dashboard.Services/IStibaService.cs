using Kombi.Dashboard.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Services
{
    public interface IStibaService


    {
      
        Task<IEnumerable<StibaModel>> GetStibaAnswers();
        Task<IEnumerable<StibaModel>> GetStibaAnswers23();

    }
}
