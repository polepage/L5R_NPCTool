using NPC.Parser;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Controls
{
    public class ScriptFormatingToolbar: ToolBar
    {
        #region TextBox
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target",
                                        typeof(TextBox),
                                        typeof(ScriptFormatingToolbar),
                                        new PropertyMetadata(OnTargetChanged));

        public TextBox Target
        {
            get => (TextBox)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }
        #endregion

        #region Formater
        public static readonly DependencyProperty FormaterProperty =
            DependencyProperty.Register("Formater",
                                        typeof(IFormater),
                                        typeof(ScriptFormatingToolbar));

        public IFormater Formater
        {
            get => (IFormater)GetValue(FormaterProperty);
            set => SetValue(FormaterProperty, value);
        }
        #endregion

        #region Bold
        private static readonly DependencyPropertyKey BoldCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("BoldCommand",
                                                typeof(ICommand),
                                                typeof(ScriptFormatingToolbar),
                                                new PropertyMetadata());

        public static readonly DependencyProperty BoldCommandProperty = BoldCommandPropertyKey.DependencyProperty;

        public ICommand BoldCommand
        {
            get => (ICommand)GetValue(BoldCommandProperty);
            private set => SetValue(BoldCommandPropertyKey, value);
        }
        #endregion

        #region Italic
        private static readonly DependencyPropertyKey ItalicCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("ItalicCommand",
                                                typeof(ICommand),
                                                typeof(ScriptFormatingToolbar),
                                                new PropertyMetadata());

        public static readonly DependencyProperty ItalicCommandProperty = ItalicCommandPropertyKey.DependencyProperty;

        public ICommand ItalicCommand
        {
            get => (ICommand)GetValue(ItalicCommandProperty);
            private set => SetValue(ItalicCommandPropertyKey, value);
        }
        #endregion

        #region Add Indentation
        private static readonly DependencyPropertyKey AddIndentationCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("AddIndentationCommand",
                                                typeof(ICommand),
                                                typeof(ScriptFormatingToolbar),
                                                new PropertyMetadata());

        public static readonly DependencyProperty AddIndentationCommandProperty = AddIndentationCommandPropertyKey.DependencyProperty;

        public ICommand AddIndentationCommand
        {
            get => (ICommand)GetValue(AddIndentationCommandProperty);
            private set => SetValue(AddIndentationCommandPropertyKey, value);
        }
        #endregion

        #region Insert Text
        private static readonly DependencyPropertyKey InsertTextCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("InsertTextCommand",
                                                typeof(ICommand),
                                                typeof(ScriptFormatingToolbar),
                                                new PropertyMetadata());

        public static readonly DependencyProperty InsertTextCommandProperty = InsertTextCommandPropertyKey.DependencyProperty;

        public ICommand InsertTextCommand
        {
            get => (ICommand)GetValue(InsertTextCommandProperty);
            private set => SetValue(InsertTextCommandPropertyKey, value);
        }
        #endregion

        #region Insert Symbol
        private static readonly DependencyPropertyKey InsertSymbolCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("InsertSymbolCommand",
                                                typeof(ICommand),
                                                typeof(ScriptFormatingToolbar),
                                                new PropertyMetadata());

        public static readonly DependencyProperty InsertSymbolCommandProperty = InsertSymbolCommandPropertyKey.DependencyProperty;

        public ICommand InsertSymbolCommand
        {
            get => (ICommand)GetValue(InsertSymbolCommandProperty);
            private set => SetValue(InsertSymbolCommandPropertyKey, value);
        }
        #endregion

        public ScriptFormatingToolbar()
        {
            BoldCommand = new DelegateCommand(ApplyBold);
            ItalicCommand = new DelegateCommand(ApplyItalic);
            AddIndentationCommand = new DelegateCommand(AddIntentation);
            InsertTextCommand = new DelegateCommand<string>(InsertText);
            InsertSymbolCommand = new DelegateCommand<object>(InsertSymbol);
        }

        private static void OnTargetChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is ScriptFormatingToolbar toolbar)
            {
                if (args.OldValue is TextBox oldBox)
                {
                    toolbar.ClearInputBindings(oldBox);
                }

                if (args.NewValue is TextBox newBox)
                {
                    toolbar.AddInputBindings(newBox);
                }
            }
        }

        private void ApplyBold()
        {
            if (Target == null || Formater == null)
            {
                return;
            }

            string weightedText = Formater.ApplyBold(Target.SelectedText);
            int currentCaret = Target.CaretIndex;

            if (Target.SelectionLength > 0)
            {
                Target.SelectedText = weightedText;
                Target.CaretIndex = currentCaret + weightedText.Length;
            }
            else
            {
                Target.Text = Target.Text.Insert(currentCaret, weightedText);
                Target.CaretIndex = currentCaret + (weightedText.Length / 2);
            }
        }

        private void ApplyItalic()
        {
            if (Target == null || Formater == null)
            {
                return;
            }

            string styledText = Formater.ApplyItalic(Target.SelectedText);
            int currentCaret = Target.CaretIndex;

            if (Target.SelectionLength > 0)
            {
                Target.SelectedText = styledText;
                Target.CaretIndex = currentCaret + styledText.Length;
            }
            else
            {
                Target.Text = Target.Text.Insert(currentCaret, styledText);
                Target.CaretIndex = currentCaret + (styledText.Length / 2);
            }
        }

        private void AddIntentation()
        {
            if (Target == null || Formater == null)
            {
                return;
            }

            string blockedText = Formater.IndentBlock(Target.SelectedText);
            int currentCaret = Target.CaretIndex;

            if (Target.SelectionLength > 0)
            {
                Target.SelectedText = blockedText;
            }
            else
            {
                Target.Text = Target.Text.Insert(currentCaret, blockedText);
            }

            Target.CaretIndex = currentCaret + blockedText.Length;
        }

        private void InsertText(string text)
        {
            if (Target == null || string.IsNullOrEmpty(text))
            {
                return;
            }

            int currentCaret = Target.CaretIndex;
            Target.Text = Target.Text.Insert(currentCaret, text);
            Target.CaretIndex = currentCaret + text.Length;
        }

        private void InsertSymbol(object symbol)
        {
            if (Formater == null || symbol == null)
            {
                return;
            }

            InsertText(Formater.InsertSymbol(symbol.ToString()));
        }

        private void AddInputBindings(TextBox box)
        {
            foreach (InputBinding binding in InputBindings)
            {
                box.InputBindings.Add(binding);
            }
        }

        private void ClearInputBindings(TextBox box)
        {
            var toRemove = new List<InputBinding>();
            foreach (InputBinding binding in box.InputBindings)
            {
                if (InputBindings.Contains(binding))
                {
                    toRemove.Add(binding);
                }
            }

            foreach (var binding in toRemove)
            {
                box.InputBindings.Remove(binding);
            }
        }
    }
}
