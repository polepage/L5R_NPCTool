using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace L5RUI.Controls
{
    class TagSelector: MultiSelector
    {
        #region Templates
        public static readonly DependencyProperty PromptTemplateProperty =
            DependencyProperty.Register("PromptTemplate",
                                        typeof(DataTemplate),
                                        typeof(TagSelector));

        public DataTemplate PromptTemplate
        {
            get => (DataTemplate)GetValue(PromptTemplateProperty);
            set => SetValue(PromptTemplateProperty, value);
        }

        public static readonly DependencyProperty PromptTemplateSelectorProperty =
            DependencyProperty.Register("PromptTemplateSelector",
                                        typeof(DataTemplateSelector),
                                        typeof(TagSelector));

        public DataTemplateSelector PromptTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(PromptTemplateSelectorProperty);
            set => SetValue(PromptTemplateSelectorProperty, value);
        }
        #endregion

        #region Prompt
        public static readonly DependencyProperty PromptProperty =
            DependencyProperty.Register("Prompt",
                                        typeof(object),
                                        typeof(TagSelector),
                                        new PropertyMetadata("Add"));

        public object Prompt
        {
            get => GetValue(PromptProperty);
            set => SetValue(PromptProperty, value);
        }

        public static readonly DependencyProperty PromptStringFormatProperty =
            DependencyProperty.Register("PromptStringFormat",
                                        typeof(string),
                                        typeof(TagSelector));

        public string PromptStringFormat
        {
            get => (string)GetValue(PromptStringFormatProperty);
            set => SetValue(PromptStringFormatProperty, value);
        }
        #endregion

        #region DropDown
        


        #endregion
    }
}
