using NPC.Common;
using NPC.Parser.Structure;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NPC.Presenter.Windows.Extensions
{
    static class ParserExtensions
    {
        public static Inline GetWindowsInline(this InlineElement inline)
        {
            switch (inline)
            {
                case TextRun r:
                    return new Run(r.Text)
                    {
                        FontWeight = r.IsBold ? FontWeights.Bold : FontWeights.Regular,
                        FontStyle = r.IsItalic ? FontStyles.Italic : FontStyles.Normal
                    };
                case Symbol s:
                    return new InlineUIContainer(new Image
                    {
                        Width = 12,
                        Height = 12,
                        Source = ((DiceIcons)Enum.Parse(typeof(DiceIcons), s.Name)).GetImageSource()
                    });
                default:
                    throw new ArgumentException("Unknown InlineElement.");
            }
        }

        private static ImageSource GetImageSource(this DiceIcons icons)
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Toolbar/" + icons.ToString() + @".png"));
        }
    }
}
