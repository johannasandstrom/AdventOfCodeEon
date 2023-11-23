using System.Windows.Controls;

namespace AdventOfCodeEon.UI
{
    /// <summary>
    /// TextBox that automatically scrolls to end as its content grows.
    /// </summary>
    public class ScrollingTextBox : TextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            CaretIndex = Text.Length;
            ScrollToEnd();
        }
    }
}