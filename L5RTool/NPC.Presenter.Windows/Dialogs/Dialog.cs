namespace NPC.Presenter.Windows.Dialogs
{
    static class Dialog
    {
        public static readonly string Title = "Title";

        public static class New
        {
            public static readonly string Name = "newdialog";
            public static readonly string Type = "Type";
        }

        public static class Selection
        {
            public static readonly string Name = "selectiondialog";
            public static readonly string Accept = "Accept";
            public static readonly string Source = "Source";
            public static readonly string SelectedItems = "SelectedItems";
        }

        public static class Save
        {
            public static readonly string Name = "savedialog";
            public static readonly string Items = "Items";
            public static readonly string Selector = "Selector";
            public static readonly string NeedSave = "NeedSave";
        }

        public static class Confirmation
        {
            public static readonly string Name = "confirmationdialog";
            public static readonly string Content = "Content";
        }

        public static class Print
        {
            public static readonly string Name = "printdialog";
            public static readonly string Source = "Source";
            public static readonly string Opener = "Opener";
        }

        public static class File
        {
            public static readonly string Filter = "Filter";
            public static readonly string Target = "Target";
        }

        public static class About
        {
            public static readonly string Name = "aboutdialog";
        }
    }
}
