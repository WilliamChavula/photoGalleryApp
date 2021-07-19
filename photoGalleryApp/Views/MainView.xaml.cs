using System;
using System.Collections.Generic;
using photoGalleryApp.ViewModels;
using Xamarin.Forms;

namespace photoGalleryApp.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = Resolver.Resolve<MainViewModel>();
        }
    }
}
