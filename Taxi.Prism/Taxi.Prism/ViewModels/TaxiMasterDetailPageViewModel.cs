using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Taxi.Common.Models;

namespace Taxi.Prism.ViewModels
{
    public class TaxiMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public TaxiMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_airplanemode_active",
                    PageName = "HomePage",
                    Title = "New trip"
                },
                new Menu
                {
                    Icon = "ic_local_taxi",
                    PageName = "TaxiHistoryPage",
                    Title = "See taxi history"
                },
                new Menu
                {
                    Icon = "ic_nature_people",
                    PageName = "GroupPage",
                    Title = "Admin my user group"
                },
                new Menu
                {
                    Icon = "ic_account_box",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_report_problem",
                    PageName = "ReportPage",
                    Title = "Report an incident"
                },
                new Menu
                {
                    Icon = "ic_supervised_user_circle",
                    PageName = "LoginPage",
                    Title = "Log in"
                }
            };
            Menus = new ObservableCollection<MenuItemViewModel>(
               menus.Select(m => new MenuItemViewModel(_navigationService)
               {
                   Icon = m.Icon,
                   PageName = m.PageName,
                   Title = m.Title
               }).ToList());
        }

    }
}
