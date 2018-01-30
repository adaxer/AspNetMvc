using Autofac;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Southwind.Wpf.Categories
{
    public class CatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                    .AssignableTo<IScreen>()
                    .AsSelf()
                    .SingleInstance();

            builder.RegisterType<CategoriesViewModel>().As<Screen>().SingleInstance();

            ResourceDictionary dict = new ResourceDictionary
            {
                Source = new Uri("/Southwind.Wpf.Categories;component/CatResources.xaml",
                     UriKind.RelativeOrAbsolute)
            };
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
