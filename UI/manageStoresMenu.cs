using BL;

namespace UI
{
    public class manageStoresMenu : IMenu
    {
        private IBL _bl;

        public manageStoresMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
        }
    }
}