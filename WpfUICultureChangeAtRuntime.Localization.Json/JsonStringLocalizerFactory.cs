using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace WpfUICultureChangeAtRuntime.Localization.Json
{

    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {

        private readonly ILoggerFactory _loggerFactory;
        private readonly JsonLocalizationOptions _localizationOptions;

        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
        {
            _localizationOptions = localizationOptions.Value;
            _loggerFactory = loggerFactory;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
            {
                throw new ArgumentNullException(nameof(resourceSource));
            }
            return CreateJsonStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            if (baseName == null)
            {
                throw new ArgumentNullException(nameof(baseName));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            return CreateJsonStringLocalizer();
        }

        /// <summary>
        /// Creates a Json string localizer
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private JsonStringLocalizer CreateJsonStringLocalizer()
        {
            var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();
            logger.LogInformation($"CreateJsonStringLocalizer gonna create a JsonStringLocalizer, option : {_localizationOptions.ResourcesPath} ");
            return new JsonStringLocalizer(_localizationOptions, logger);
        }


    }
}
