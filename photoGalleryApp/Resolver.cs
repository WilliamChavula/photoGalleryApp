using System;
using Autofac;

namespace photoGalleryApp
{
    public class Resolver
    {

        private static IContainer _container;
        public Resolver()
        {
        }

        public static void Initialize(IContainer container)
        {
            _container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
