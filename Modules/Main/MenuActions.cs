// <copyright file="MenuActions.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System;
using HomeAppliancesStore.Modules.Admin;
using HomeAppliancesStore.Modules.User;

namespace HomeAppliancesStore.Modules.Main
{
    public class MenuActions
    {
        private readonly UserEntity currentUser;

        public MenuActions(UserEntity user)
        {
            this.currentUser = user;
        }

        public void Run()
        {
            if (this.currentUser.Role == "Admin")
            {
                var adminMenu = new AdminMenuActions();
                adminMenu.ShowAdminMenu(this.currentUser);
            }
            else
            {
                var userMenu = new UserMenuActions();
                userMenu.ShowUserMenu(this.currentUser);
            }
        }
    }
}
