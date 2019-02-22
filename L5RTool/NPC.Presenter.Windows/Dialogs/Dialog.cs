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

        public static class Open
        {
            public static readonly string Name = "opendialog";
            public static readonly string Source = "Source";
            public static readonly string Selection = "Selection";
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
    }
}
