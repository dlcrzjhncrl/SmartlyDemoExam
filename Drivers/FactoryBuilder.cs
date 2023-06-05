using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System;

namespace SmartlySpecflow.Drivers
{
    public static class FactoryBuilder
    {

        private static readonly IDictionary<string, Type> s_factories = Assembly.GetExecutingAssembly()
                                                                              .GetTypes()
                                                                              .Where(type => type.GetInterface(typeof(IDriverFactory).ToString()) != null)
                                                                              .ToDictionary(type => type.Name.ToLower(CultureInfo.InstalledUICulture), type => type);

        public static IDriverFactory? GetFactory(string browser)
        {
            foreach (var factory in s_factories)
            {
                if (factory.Key.Contains(browser, StringComparison.InvariantCulture))
                {
                    return Activator.CreateInstance(factory.Value) as IDriverFactory;
                }
            }
            throw new ArgumentException("Driver factory " + browser + " not supported");
        }

        public static IDriverFactory GetFactory(string browser, string gridUrl)
        {
            return new RemoteDriverFactory(browser, gridUrl);
        }
    }
}
