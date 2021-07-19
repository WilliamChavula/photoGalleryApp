using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using photoGalleryApp.Interfaces;
using photoGalleryApp.Models;
using Xamarin.Forms;

namespace photoGalleryApp.ViewModels
{
    public class GalleryViewModel : ViewModel
    {
        private int _itemsAdded;
        private int _currentStartIndex = 0;
        private readonly IPhotoImporter _photoImporter;
        private readonly ILocalStorage _localStorage;

        public ObservableCollection<Photo> Photos { get; set; }

        public GalleryViewModel(IPhotoImporter photoImporter, ILocalStorage localStorage)
        {
            _photoImporter = photoImporter;
            _localStorage = localStorage;
            Task.Run(Initialize);
        }

        private async Task Initialize()
        {
            IsBusy = true;
            Photos = await _photoImporter.Get(0, 20);
            RaisePropertyChanged(nameof(Photos));

            Photos.CollectionChanged += Photos_CollectionChanged;
        }

        private void Photos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                IsBusy = false;
                Photos.CollectionChanged -= Photos_CollectionChanged;
            }
        }

        private void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            foreach (Photo photo in args.NewItems)
            {
                _itemsAdded++;
                Photos.Add(photo);
            }

            if (_itemsAdded == 20)
            {
                var collection = (ObservableCollection<Photo>)sender;
                collection.CollectionChanged -= Collection_CollectionChanged;
            }
        }

        public ICommand LoadMore => new Command(async () =>
        {
            _currentStartIndex += 20;
            _itemsAdded = 0;
            var collection = await _photoImporter.Get(_currentStartIndex, 20);
            collection.CollectionChanged += Collection_CollectionChanged;
        });

        public ICommand AddFavorites => new Command<List<Photo>>((photos) =>
        {
            foreach (var photo in photos)
            {
                _localStorage.Store(photo.Filename);
            }

            MessagingCenter.Send(this, "FavoritesAdded");
        });
    }
}
