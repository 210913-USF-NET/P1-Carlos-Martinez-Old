using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        public List<StoreFront> GetAllStoreFronts();
    }
}
