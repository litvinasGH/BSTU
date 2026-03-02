using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Printinvest.Models;
using Printinvest.Models.Enums;
using Printinvest.ViewModels;

namespace Printinvest.Views
{
    public partial class AdminPanelWindow : Window
    {
        private readonly AdminPanelViewModel _vm;

        public AdminPanelWindow()
        {
            InitializeComponent();
            InitializeComponent();
            DataContext = new AdminPanelViewModel();
        }

        
    }
}