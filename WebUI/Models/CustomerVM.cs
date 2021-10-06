using System.ComponentModel.DataAnnotations;
using Models;

namespace WebUI.Models
{
    public class CustomerVM
    {
        // constructor
        public CustomerVM() { }
        public CustomerVM(Customer custo)
        {
            this.Id = custo.Id;
            this.Name = custo.Name;
            this.Credit = custo.Credit;
            this.Password = custo.Password;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name="Username")]
        public string Name { get; set; }
        public int Credit { get; set; }
        [Required]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Credit: {this.Credit}";
        }

        public Customer ToModel()
        {
            Customer newCusto;
            try
            {
                newCusto = new Customer
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
                    Password = this.Password,
                    Credit = this.Credit
                };
            }
            catch
            {
                throw;
            }
            return newCusto;
        }
    }
}
