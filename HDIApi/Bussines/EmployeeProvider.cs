using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace HDIApi.Bussines
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly InsurancedbContext _context;

        public EmployeeProvider(InsurancedbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }

        public Task<(int, EmployeeDTO)> GetEmployeeById(string idEmployee)
        {
            int code = 0;
            EmployeeDTO employee;
            try
            {
                var employeeTemp = _context.Employees.Where(a => a.IdEmployee.Equals(idEmployee)).FirstOrDefault();
                if (employeeTemp != null)
                {
                    employee = new EmployeeDTO()
                    {
                        IdEmployee = employeeTemp.IdEmployee,
                        NameEmployee = employeeTemp.NameEmployee,
                        LastnameEmployee = employeeTemp.LastnameEmployee,
                        Username = employeeTemp.Username,
                        Password = employeeTemp.Password,
                        Rol = employeeTemp.Rol,
                        RegistrationDate = employeeTemp.RegistrationDate
                    };
                    code = 200;
                }
                else
                {
                    code = 404;
                    employee = new EmployeeDTO();
                }
            }
            catch (Exception)
            {
                code = 500;
                employee = new EmployeeDTO();
            }
            return Task.FromResult((code, employee));
        }

        public Task<(int, List<EmployeeDTO>)> GetEmployeeList()
        {
            int code = 0;
            List<EmployeeDTO> employeesList = new List<EmployeeDTO>();
            try
            {
                employeesList = new List<EmployeeDTO>();
                var list = _context.Employees.ToList();
                foreach(var item in list)
                {
                    EmployeeDTO temp = new EmployeeDTO(){
                        IdEmployee = item.IdEmployee,
                        NameEmployee = item.NameEmployee,
                        LastnameEmployee = item.LastnameEmployee,
                        Username = item.Username,
                        Password = "",
                        Rol = item.Rol,
                        RegistrationDate = item.RegistrationDate

                    };
                    employeesList.Add(temp);
                }
                code = 200;
            }
            catch (Exception)
            {
                code = 500;
            }
            return Task.FromResult((code, employeesList));
        }

        public Task<int> SetNewEmployee(EmployeeDTO newEmployee)
        {
            int code = 0;
            //buscar si existe ya el usuario
            var employeeTemp = _context.Employees.Where(a => a.Username.Equals(newEmployee.Username)).FirstOrDefault();
            if (employeeTemp == null)
            {
                Employee employeeModel = new Employee()
                {
                    IdEmployee = Utility.GenerateRandomID.GenerateID(),
                    NameEmployee = newEmployee.NameEmployee,
                    LastnameEmployee = newEmployee.LastnameEmployee,
                    Username = newEmployee.Username,
                    Password = newEmployee.Password,
                    Rol = newEmployee.Rol,
                    RegistrationDate = DateTime.Now

                };
                _context.Employees.Add(employeeModel);
                int change = _context.SaveChanges();
                if (change == 1)
                    code = 200;
                else
                    code = 500;
            }
            else
                code = 409;//Conflito - ya hay alguien con la licencia registado
            return Task.FromResult(code);
        }

        public Task<int> SetUpdateEmployee(EmployeeDTO updatedEmployee)
        {
            int changes = 0;
            int code = 0;
            //buscar si existe ya el usuario
            try
            {
                var employeeTemp = _context.Employees.Where(a => a.Username.Equals(updatedEmployee.Username)).FirstOrDefault();
                if (employeeTemp != null)
                {
                    Console.WriteLine("Entrnado");
                    // se verifica que el username encontrado ya este asignado al mismo usuario
                    if (employeeTemp.IdEmployee.Equals(updatedEmployee.IdEmployee))
                    {
                        employeeTemp.NameEmployee = updatedEmployee.NameEmployee;
                        employeeTemp.LastnameEmployee = updatedEmployee.LastnameEmployee;
                        if (!updatedEmployee.Password.IsNullOrEmpty())
                            employeeTemp.Password = updatedEmployee.Password;
                        employeeTemp.Rol = updatedEmployee.Rol;
                        changes = _context.SaveChanges();
                        if (changes == 1)
                            code = 200;
                    }
                    else
                    {
                        code = 409;//conflicto-Otro usuario ya tene asignado ese username
                    }
                }
                else
                {
                    //no se habia encontrado el username,entonces si se lo cambio
                    employeeTemp = _context.Employees.Where(a => a.IdEmployee.Equals(updatedEmployee.IdEmployee)).FirstOrDefault();
                    if (employeeTemp != null)
                    {
                        employeeTemp.NameEmployee = updatedEmployee.NameEmployee;
                        employeeTemp.LastnameEmployee = updatedEmployee.LastnameEmployee;
                        employeeTemp.Username = updatedEmployee.Username;
                        if (!updatedEmployee.Password.IsNullOrEmpty())
                            employeeTemp.Password = updatedEmployee.Password;
                        employeeTemp.Rol = updatedEmployee.Rol;
                        changes = _context.SaveChanges();
                        if (changes == 1)
                            code = 200;
                    }
                }
            }
            catch (Exception)
            {
                code = 500;
            }
            return Task.FromResult(code);
        }
    }

}
