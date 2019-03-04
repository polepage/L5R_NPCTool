using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Controls
{
    class PrintDocumentViewer: DocumentViewer
    {
        public static readonly DependencyProperty PrintCompleteCommandProperty =
            DependencyProperty.Register("PrintCompleteCommand",
                                        typeof(ICommand),
                                        typeof(PrintDocumentViewer));

        public ICommand PrintCompleteCommand
        {
            get => (ICommand)GetValue(PrintCompleteCommandProperty);
            set => SetValue(PrintCompleteCommandProperty, value);
        }

        public static readonly DependencyProperty PageOrientationProperty =
            DependencyProperty.Register("PageOrientation",
                                        typeof(PageOrientation),
                                        typeof(PrintDocumentViewer),
                                        new PropertyMetadata(PageOrientation.Portrait));

        public PageOrientation PageOrientation
        {
            get => (PageOrientation)GetValue(PageOrientationProperty);
            set => SetValue(PageOrientationProperty, value);
        }

        protected override void OnPrintCommand()
        {
            var printDialog = new PrintDialog
            {
                PrintQueue = LocalPrintServer.GetDefaultPrintQueue()
            };
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;
            printDialog.PrintTicket.PageOrientation = PageOrientation;

            if (printDialog.ShowDialog().GetValueOrDefault())
            {
                printDialog.PrintDocument(Document.DocumentPaginator, "Game Objects");
                PrintCompleteCommand?.Execute(null);
            }
        }
    }
}
