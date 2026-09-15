using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daggerheartSheetWpf.Controls
{
    /// <summary>
    /// Campo numérico com setas, equivalente ao NumericUpDown do Windows Forms.
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(int),
            typeof(NumericUpDown),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged,
                CoerceValue));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum",
            typeof(int),
            typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0, OnLimitChanged));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum",
            typeof(int),
            typeof(NumericUpDown),
            new FrameworkPropertyMetadata(100, OnLimitChanged));

        public NumericUpDown()
        {
            InitializeComponent();
            UpdateText();
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!PART_TextBox.IsKeyboardFocusWithin)
            {
                return;
            }

            ChangeBy(e.Delta > 0 ? 1 : -1);
            e.Handled = true;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumericUpDown)d).UpdateText();
        }

        private static void OnLimitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ValueProperty);
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            NumericUpDown control = (NumericUpDown)d;
            int value = (int)baseValue;

            if (control.Minimum > control.Maximum)
            {
                return value;
            }

            if (value < control.Minimum)
            {
                return control.Minimum;
            }

            if (value > control.Maximum)
            {
                return control.Maximum;
            }

            return value;
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            ChangeBy(1);
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            ChangeBy(-1);
        }

        /// <summary>
        /// Confirma o que estiver digitado e soma o passo pedido, sempre dentro dos limites.
        /// </summary>
        private void ChangeBy(int delta)
        {
            CommitText();
            Value = Value + delta;
            UpdateText();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsDigitsOnly(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                ChangeBy(1);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                ChangeBy(-1);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                CommitText();
                UpdateText();
                PART_TextBox.SelectAll();
                e.Handled = true;
            }
            else if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CommitText();
            UpdateText();
        }

        /// <summary>
        /// Lê o texto digitado e joga o resultado dentro dos limites do campo.
        /// </summary>
        private void CommitText()
        {
            int parsed;
            if (int.TryParse(PART_TextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out parsed))
            {
                Value = parsed;
            }
        }

        private void UpdateText()
        {
            string text = Value.ToString(CultureInfo.CurrentCulture);
            if (PART_TextBox != null && PART_TextBox.Text != text)
            {
                PART_TextBox.Text = text;
            }
        }

        private static bool IsDigitsOnly(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            foreach (char character in text)
            {
                if (!char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
