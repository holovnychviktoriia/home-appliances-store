using System;
using HomeAppliancesStore.Modules.User;
using HomeAppliancesStore.Modules.Admin;

namespace HomeAppliancesStore.Modules.Main
{
    public class MenuActions
    {
        private readonly UserEntity _currentUser;

        public MenuActions(UserEntity user)
        {
            _currentUser = user;
        }

        public void Run()
        {
            if (_currentUser.Role == "Admin")
            {
                var adminMenu = new AdminMenuActions();
                adminMenu.ShowAdminMenu(_currentUser);
            }
            else
            {
                var userMenu = new UserMenuActions();
                userMenu.ShowUserMenu(_currentUser);
            }
        }
    }
}