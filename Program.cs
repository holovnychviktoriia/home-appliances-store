using HomeAppliancesStore.Modules.Auth;
using HomeAppliancesStore.Modules.Main;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseConfig.Initialize();

            while (true)
            {
                var authMenu = new AuthMenuActions();
                var user = authMenu.Authenticate();

                if (user != null)
                {
                    var mainMenu = new MenuActions(user);
                    mainMenu.Run();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
