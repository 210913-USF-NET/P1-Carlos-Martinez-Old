using System.Collections.Generic;
using Models;

namespace DL
{
    public class ExampleRepo : IRepo
    {
        public List<StoreFront> GetAllStoreFronts()
        {
            // logic to get data
            return new List<StoreFront>()
            {
                new StoreFront() {
                    Name = "Potion Shop"
                }
            };
        }
    }
}