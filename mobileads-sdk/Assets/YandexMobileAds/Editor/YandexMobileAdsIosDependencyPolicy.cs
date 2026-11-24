using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YandexAdsEditor
{
    // Select one dependency manager for the entire export. EDM owns resolution and injection.
    internal static class YandexMobileAdsIosDependencyPolicy
    {
        internal static bool UseSwiftPackages(IEnumerable<string> files, bool supported, bool enabled)
        {
            var documents = files.Select(file => new { File = file, Xml = XDocument.Load(file) }).ToArray();
            var pods = new HashSet<string>(documents.SelectMany(document => document.Xml.Descendants("iosPod"))
                .Select(pod => (string)pod.Attribute("name")), StringComparer.Ordinal);
            var products = documents.SelectMany(document => document.Xml.Descendants("remoteSwiftPackage")
                .Elements("swiftPackage").Select(product => new { document.File, Product = product })).ToArray();
            var replacements = new HashSet<string>(products.SelectMany(product => ReplacedPods(product.Product)), StringComparer.Ordinal);
            var useSpm = supported && enabled && pods.All(replacements.Contains);
            if (useSpm)
            {
                var duplicate = products.GroupBy(product => (string)product.Product.Attribute("name"))
                    .FirstOrDefault(group => group.Count() > 1);
                if (duplicate != null)
                    throw new InvalidOperationException("Duplicate Swift product '" + duplicate.Key + "' in " +
                        string.Join(", ", duplicate.Select(product => product.File).ToArray()) +
                        ". Remove the duplicate installation (including Assets and UPM copies); EDM drops target information for duplicates.");
            }
            else
            {
                var missing = products.FirstOrDefault(product => !ReplacedPods(product.Product).Any() ||
                    !ReplacedPods(product.Product).All(pods.Contains));
                if (missing != null)
                    throw new InvalidOperationException("This iOS export requires CocoaPods, but Swift product '" +
                        (string)missing.Product.Attribute("name") + "' has no complete iosPod fallback in " + missing.File +
                        ". Install its CocoaPods dependency declarations, or use Unity 2021.3+, a compatible EDM4U with SPM enabled, and SPM replacements for every installed pod.");
            }
            return useSpm;
        }

        private static IEnumerable<string> ReplacedPods(XElement product)
        {
            return ((string)product.Attribute("replacesPod") ?? "").Split(',')
                .Select(name => name.Trim()).Where(name => name.Length != 0);
        }
    }
}
