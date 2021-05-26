using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WpfUICultureChangeAtRuntime.Localization.Json.Extensions;

namespace WpfUICultureChangeAtRuntime.Localization.Json
{
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private readonly string _resourcesPath;
        private readonly ILogger _logger;
        /// <summary>
        /// Json files location
        /// </summary>
        private string _lookupLocation;


        public JsonStringLocalizer(JsonLocalizationOptions localizationOptions, ILogger logger)
        {
            _logger = logger ?? NullLogger.Instance;
            _resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, localizationOptions.ResourcesPath);
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetStringSafely(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _lookupLocation);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetStringSafely(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _lookupLocation);
            }
        }

        /// <summary>
        /// Feeds the language dictionary
        /// </summary>
        /// <param name="cultureKey">The wanted culture</param>
        /// <returns>The language dictionary</returns>
        private Dictionary<string, string> FeedLanguageDictionary(string cultureKey)
        {
            _logger.LogDebug($"Detected culture is {cultureKey}");
            var resourceFile = $"Lang_{cultureKey.Substring(cultureKey.Length - 2, 2)}.json";
            _lookupLocation = Path.Combine(_resourcesPath, "langs", resourceFile);

            //TODO use as Resource
            // If cannot find the culture file , must setup a default one
            if (!File.Exists(_lookupLocation))
            {
                _logger.LogWarning($"The {cultureKey} file has not been found in {_lookupLocation}, set up the default culture file");
                _lookupLocation = Path.Combine(_resourcesPath, "langs", "lang.json");
            }
            Dictionary<string, string> value = null;
            if (File.Exists(_lookupLocation))
            {
                _logger.LogInformation($"{_lookupLocation} FOUND !");
                var content = File.ReadAllText(_lookupLocation, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                if (string.IsNullOrWhiteSpace(content)) return null;
                try
                {
                    value = content.Trim().JsonToObject<Dictionary<string, string>>();
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, $"invalid json content, path: {_lookupLocation}, content: {content}");
                }
            }

            return value;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        private IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var resourceNames = includeParentCultures
                ? GetAllStringsFromCultureHierarchy(culture)
                : GetAllResourceStrings(culture);

            foreach (var name in resourceNames)
            {
                var value = GetStringSafely(name);
                yield return new LocalizedString(name, value ?? name, value == null, _lookupLocation);
            }
        }

        private string GetStringSafely(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            string value = null;

            var resources = FeedLanguageDictionary(CultureInfo.CurrentUICulture.Name);

            if (resources?.ContainsKey(name) == true)
            {
                value = resources[name];
            }
            _logger.LogInformation($"GetStringSafely => {CultureInfo.CurrentUICulture.Name}  name : {name}  >> value : {value}");
            return value;
        }

        private IEnumerable<string> GetAllStringsFromCultureHierarchy(CultureInfo startingCulture)
        {
            var currentCulture = startingCulture;
            var resourceNames = new HashSet<string>();

            while (currentCulture.Equals(currentCulture.Parent) == false)
            {
                var cultureResourceNames = GetAllResourceStrings(currentCulture);

                if (cultureResourceNames != null)
                {
                    foreach (var resourceName in cultureResourceNames)
                    {
                        resourceNames.Add(resourceName);
                    }
                }

                currentCulture = currentCulture.Parent;
            }

            return resourceNames;
        }

        private IEnumerable<string> GetAllResourceStrings(CultureInfo culture) => FeedLanguageDictionary(culture.Name)?.Select(r => r.Key);


    }
}
