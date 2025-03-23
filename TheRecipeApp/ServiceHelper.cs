using Microsoft.Extensions.DependencyInjection;
using System;

namespace TheRecipeApp
{
    public static class ServiceHelper
    {
        public static T GetService<T>() => App.ServiceProvider.GetService<T>();
    }
}
