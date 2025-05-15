namespace CRUDAPP.Model
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LoginDate{ get; set; }
        public bool IsLogin { get; set; }
    }
}
