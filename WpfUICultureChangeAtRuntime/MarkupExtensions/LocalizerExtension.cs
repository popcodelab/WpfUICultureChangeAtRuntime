using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;
using System.Windows.Markup;
using WpfUICultureChangeAtRuntime.Converters;

namespace WpfUICultureChangeAtRuntime.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(string))]
    public sealed class LocalizerExtension : MarkupExtension
    {

        #region Private class members

        private Binding _lookupBinding;

        #endregion

        [DefaultValue("")]
        public string Key
        {
            get => (string)_lookupBinding.ConverterParameter;
            set => _lookupBinding.ConverterParameter = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Translation key</param>
        public LocalizerExtension()
        {
            _lookupBinding = UICultureLookupConverter.CreateBinding("");
        }

        /// <summary>
        /// Returns the key value through JsonLocalization, the converter provides the localized string
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>translated key value></returns>
        public override object ProvideValue([NotNull] IServiceProvider serviceProvider) => _lookupBinding.ProvideValue(serviceProvider);


    }
}
