using System.ComponentModel.DataAnnotations;
using Models;

namespace WebUI.Models
{
    public class StoreFrontVM
    {
        public StoreFrontVM() { }

        // constructor w/ Name
        public StoreFrontVM(string name) : this()
        {
            this.Name = name;
        }

        public StoreFrontVM(StoreFront store)
        {
            this.Id = store.Id;
            this.Name = store.Name;
        }

        // properties
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}";
        }
        public StoreFront ToModel()
        {
            StoreFront newStore;
            try
            {
                newStore = new StoreFront
                {
                    Id = this.Id,
                    Name = this.Name ?? ""
                };
            }
            catch
            {
                throw;
            }
            return newStore;
        }
    }
}
