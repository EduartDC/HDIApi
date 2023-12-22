namespace HDIApi.DTOs
{
    public class EmployeeDTO
    {
        public string? IdEmployee {get; set;} = "0";
        public string NameEmployee {  get; set; }

        public string LastnameEmployee { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }

        public DateTime? RegistrationDate {get; set;} 
    }
}
