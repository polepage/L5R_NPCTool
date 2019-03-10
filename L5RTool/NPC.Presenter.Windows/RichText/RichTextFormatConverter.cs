using System;
using System.Reflection;
using System.Windows;

namespace NPC.Presenter.Windows.RichText
{
    static class RichTextFormatConverter
    {
        public static string RtfToXaml(string rtf)
        {
            var assembly = Assembly.GetAssembly(typeof(FrameworkElement));
            var xamlRtfConverterType = assembly.GetType("System.Windows.Documents.XamlRtfConverter");
            var xamlRtfConverter = Activator.CreateInstance(xamlRtfConverterType, true);
            var convertRtfToXaml = xamlRtfConverterType.GetMethod("ConvertRtfToXaml", BindingFlags.Instance | BindingFlags.NonPublic);
            var xaml = (string)convertRtfToXaml.Invoke(xamlRtfConverter, new object[] { rtf });

            return xaml;
        }
    }
}
