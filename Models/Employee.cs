using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace TokenAuthLogin.Models{
    public class Employee{
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        
    }
}