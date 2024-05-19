using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Auth.Common.Constants
{
    public sealed class ResourceManagerSingleton
    {
        private static ResourceManagerSingleton _instance = null;
        private static readonly object padlock = new object();
        private readonly ResourceManager _resourceManager;
        private const string resourceFileName = "Auth.Common.Constants.Constants";

        private ResourceManagerSingleton()
        {
            _resourceManager = new ResourceManager(resourceFileName, Assembly.GetExecutingAssembly());
        }

        public static ResourceManagerSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ResourceManagerSingleton();
                    }
                }
                return _instance;
            }
        }

        public string GetString(string key, CultureInfo culture = null)
        {
            return _resourceManager.GetString(key, culture) ?? string.Empty;
        }
    }
}
