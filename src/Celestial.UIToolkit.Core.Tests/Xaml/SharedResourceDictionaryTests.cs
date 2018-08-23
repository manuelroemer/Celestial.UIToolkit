using Celestial.UIToolkit.Tests.Resources;
using Celestial.UIToolkit.Xaml;
using System;
using System.Linq;
using System.Windows.Markup;
using Xunit;

namespace Celestial.UIToolkit.Tests.Xaml
{

    public class SharedResourceDictionaryTests
    {

        [Fact]
        public void ThrowsIfSourceIsNull()
        {
            var dict = new SharedResourceDictionary();
            var ex = Record.Exception(() => dict.Source = null);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void CachesSourceDictionary()
        {
            var firstDict = new SharedResourceDictionary();
            var secondDict = new SharedResourceDictionary();
            firstDict.Source = SampleDictionaryKeys.DictionaryUri;
            secondDict.Source = SampleDictionaryKeys.DictionaryUri;

            // If this test is run after other tests, firstDict may already load a cached dict.
            // If it is the first test to be run, firstDict will load the dict directly.
            // -> Non-cached dicts are loaded directly into the dictionary.
            //    Cached dicts are loaded into the 'MergedDictionaries' property.
            //    
            // We need to check these two cases.
            if (firstDict.MergedDictionaries.Count == 0)
            {
                // First dict loaded the dictionary directly.
                Assert.NotEmpty(firstDict);
                Assert.Empty(secondDict);
                Assert.NotEmpty(secondDict.MergedDictionaries);
                Assert.Equal(firstDict, secondDict.MergedDictionaries.First());
            }
            else
            {
                // First dict already loaded a cached dictionary.
                Assert.Empty(firstDict);
                Assert.Empty(secondDict);
                Assert.NotEmpty(firstDict.MergedDictionaries);
                Assert.NotEmpty(secondDict.MergedDictionaries);
                Assert.Equal(
                    firstDict.MergedDictionaries.First(),
                    secondDict.MergedDictionaries.First());
            }
        }

        [Fact]
        public void LoadsRelativeUris()
        {
            // For testing if the dictionary can load relative URIs, it needs a BaseUri
            // (to simulate where it is located).
            // Set this BaseUri to the dictionary's folder.
            // We can then test if the dict can load the relative "FileName.xaml" URI.
            var baseUri = GetSampleDictionaryDirectoryUri();
            var dict = new SharedResourceDictionary();
            var fileNameUri = new Uri(
                SampleDictionaryKeys.DictionaryUri.Segments.Last(),
                UriKind.Relative);

            ((IUriContext)dict).BaseUri = baseUri;
            dict.Source = fileNameUri;

            Assert.NotEmpty(dict);
            Assert.Equal(SampleDictionaryKeys.DictionaryUri, dict.Source);
        }

        private Uri GetSampleDictionaryDirectoryUri()
        {
            // The URI's last segment part is the filename - simply remove it from the string
            // and we've got the dictionary's folder URI.
            var dictUri = SampleDictionaryKeys.DictionaryUri;
            var fileName = dictUri.Segments.Last();
            return new Uri(dictUri.OriginalString.Replace(fileName, ""));
        }

        [Fact]
        public void ThrowsForRelativeSourceUriWithoutBaseUri()
        {
            var dict = new SharedResourceDictionary();
            var source = new Uri("RelativeFile.xaml", UriKind.Relative);

            var ex = Record.Exception(() => dict.Source = source);
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
        }

    }

}
