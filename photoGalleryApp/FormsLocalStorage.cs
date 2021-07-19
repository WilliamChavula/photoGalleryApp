using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using photoGalleryApp.Interfaces;
using Xamarin.Forms;

namespace photoGalleryApp
{
    public class FormsLocalStorage : ILocalStorage
    {
        public const string FAVORITE_PHOTOS_KEY = "FavoritePhotos";
        public FormsLocalStorage()
        {
        }

        public async Task<List<string>> Get()
        {
            if (Application.Current.Properties.ContainsKey(FAVORITE_PHOTOS_KEY))
            {
                var filenames = (string)Application.Current.Properties[FAVORITE_PHOTOS_KEY];

                return JsonConvert.DeserializeObject<List<string>>(filenames);
            }

            return new List<string>();
        }

        public async Task Store(string filename)
        {
            var filenames = await Get();
            filenames.Add(filename);

            var json = JsonConvert.SerializeObject(filenames);
            Application.Current.Properties[FAVORITE_PHOTOS_KEY] = json;

            await Application.Current.SavePropertiesAsync();
        }
    }
}
