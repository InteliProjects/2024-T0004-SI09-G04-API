using Kombi.Dashboard.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Services;
using System.Security.Cryptography.X509Certificates;


namespace Kombi.Dashboard.Services
{
    public class CidsService : ICidsService
    {
        private readonly ICidsRepository _repository;

        public CidsService(ICidsRepository repository)
        {
            _repository = repository;
        }

        public string GetModel()
        {
            return "CidsModel";
        }

        public async Task<IEnumerable<CidsModel>> GetCids()
        { 
           
            return await _repository.GetCids();

        }

       /* public async Task<CidsModel> GetCidsById(int id)
        {
            return await _repository.GetCidsById(id);
        }*/

        public Task<int> MethodToTest(int parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> MethodToTest()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> MethodToTest(CidsModel cidsModel)
        {
            throw new NotImplementedException();
        }
    }
}

