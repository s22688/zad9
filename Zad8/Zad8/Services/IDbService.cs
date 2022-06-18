using System.Collections.Generic;
using System.Threading.Tasks;
using Zad8.Models;

namespace Zad8.Services
{
    public interface IDbService
    {
        public Task<IEnumerable<object>> GetDoctor(int id);
        public Task<bool> AddDoctor(Doctor doctor);
        public Task<bool> PostDoctor(Doctor doctor, int id);
        public Task<bool> DeleteDoctor(int id);
        public Task<IEnumerable<object>> GetPerciption(int id);
        public Task<bool> SendLoginData(LoginRequest loginRequest);

    }
}
