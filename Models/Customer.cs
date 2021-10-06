using Serilog;

namespace Models
{
    public class Customer
    {
        // default constructor
        public Customer() 
        {
            // Start every customer out with 30 gold pieces. 
            Credit = 30;
        }

        // constructor w/ Name
        public Customer(string name) : this()
        {
            this.Name = name;
        }

        // properties
        public int Id { get; set; }
        private string _name;
        public string Name
        { 
            get
            {
                return _name;
            }

            set
            {
                if (value.Length == 0)
                {
                    InputInvalidException e = new InputInvalidException("Terminating program. We require names to be at least one character long.");
                    Log.Warning(e.Message);
                    throw e;
                }
                else
                {
                    _name = value;
                }
            }
        }
        public string Password { get; set; }
        public int Credit { get; set; }
        public int StoreFrontID { get; set; }
        public int hasDefaultStore { get; set; }
        
        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Credit: {this.Credit}, StoreFrontID: {this.StoreFrontID}, hasDefaultStore: {this.hasDefaultStore}";
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
                    Credit = this.Credit,
                    Password = this.Password
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