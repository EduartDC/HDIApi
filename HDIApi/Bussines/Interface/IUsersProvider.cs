using HDIApi.DTOs;
using HDIApi.Models;

namespace HDIApi.Bussines.Interface
{
    public interface IUsersProvider
    {
        public Task<Employee> LoginEmployee(LoginDTO login);
    }
}
