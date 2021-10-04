using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Credit { get; set; }

        public Customer ToModel()
        {
            Customer newCusto;
            try
            {
                newCusto = new Customer
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
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
