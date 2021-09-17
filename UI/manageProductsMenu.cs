using BL;

namespace UI
{
    public class manageProductsMenu : IMenu
    {
        private IBL _bl;

        public manageProductsMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
        }
    }
}