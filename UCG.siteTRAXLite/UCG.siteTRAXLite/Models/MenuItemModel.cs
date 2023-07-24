using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models
{
    public class MenuItemModel : BindableBase
    {
        private string menuTitle;
        public string MenuTitle { 
            get 
            { 
                return menuTitle;
            } 
            set 
            {
               SetProperty(ref menuTitle, value); 
            } 
        }

        private string menuIcon;
        public string MenuIcon
        {
            get
            {
                return menuIcon;
            }
            set
            {
                SetProperty(ref menuIcon, value);
            }
        }
    }
}
