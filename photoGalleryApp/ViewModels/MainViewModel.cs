using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using photoGalleryApp.Interfaces;
using photoGalleryApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace photoGalleryApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IPhotoImporter _photoImporter;
        private readonly ILocalStorage _localStorage;

        public ObservableCollection<Photo> Recent { get; set; }
        public ObservableCollection<Photo> Favorites { get; set; }

        public MainViewModel(IPhotoImporter photoImporter, ILocalStorage localStorage)
        {
            _photoImporter = photoImporter;
            _localStorage = localStorage;

            Task.Run(Initialize);
        }

        public async Task Initialize()
        {
            var photos = await _photoImporter.Get(0, 20);

            Recent = photos;
            RaisePropertyChanged(nameof(Recent));

            await LoadFavorites();

            MessagingCenter.Subscribe<GalleryViewModel>(this, "FavoritesAdded", (sender) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await LoadFavorites();
                });
            });
        }

        private async Task LoadFavorites()
        {
            var filenames = await _localStorage.Get();
            var favorites = await _photoImporter.Get(filenames);

            Favorites = favorites;
            RaisePropertyChanged(nameof(Favorites));
        }
    }
}
