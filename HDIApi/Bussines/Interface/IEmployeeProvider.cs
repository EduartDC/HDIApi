using HDIApi.DTOs;
using HDIApi.Models;

namespace HDIApi.Bussines.Interface
{
    public interface IEmployeeProvider
    {
        public Task<int> SetNewEmployee(EmployeeDTO newEmployee);

        public Task<(int,List<EmployeeDTO>)> GetEmployeeList();
        public Task<(int,EmployeeDTO)> GetEmployeeById(string IdEmployee);
        public Task<int> SetUpdateEmployee(EmployeeDTO updatedEmployee);

    }
}

