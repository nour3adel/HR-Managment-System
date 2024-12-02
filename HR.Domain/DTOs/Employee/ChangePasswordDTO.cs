namespace HR.Domain.DTOs.Employee
{
    public class ChangePasswordDTO
    {

        public string employeeid { get; set; }


        public string oldpassword { get; set; }

        public string newpassword { get; set; }
        public string confirm_password { get; set; }
    }
}
