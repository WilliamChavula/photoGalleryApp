using System;
using Autofac;
using photoGalleryApp.Interfaces;

namespace photoGalleryApp.Droid
{
    public class Bootstrapper : photoGalleryApp.Bootstrapper
    {
        public Bootstrapper()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            ContainerBuilder.RegisterType<PhotoImporter>().As<IPhotoImporter>().SingleInstance();
        }
    }
}
