namespace TodoApp.Hangfire.Models
{
    public class EmailSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}
