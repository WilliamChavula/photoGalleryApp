using System;
using System.Collections.Generic;
using System.Linq;
using photoGalleryApp.Models;
using photoGalleryApp.ViewModels;
using Xamarin.Forms;

namespace photoGalleryApp.Views
{
    public partial class GalleryView : ContentPage
    {
        public GalleryView()
        {
            InitializeComponent();
            BindingContext = Resolver.Resolve<GalleryViewModel>();
        }

        private void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!Photos.SelectedItems.Any())
            {
                DisplayAlert("No Photos", "No photos selected", "OK");
                return;
            }

            var viewModel = (GalleryViewModel)BindingContext;
            viewModel.AddFavorites.Execute(Photos.SelectedItems.Select(x => (Photo)x).ToList());

            DisplayAlert("Added", "Selected photos have been added to favorites", "OK");
        }
    }
}
