using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace daggerheartSheetWpf.Controls
{
    /// <summary>
    /// Seletor de cor em WPF: lista de cores nomeadas mais ajuste fino em R, G e B.
    /// Substitui o ColorDialog que a versão Windows Forms usava.
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor",
            typeof(Color),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(
                Colors.White,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedColorChanged));

        private readonly IList<NamedColor> _namedColors = NamedColor.CreateAll();
        private bool _updating;

        public ColorPicker()
        {
            InitializeComponent();
            comboColors.ItemsSource = _namedColors;
            SyncControls(SelectedColor);
        }

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColorPicker)d).SyncControls((Color)e.NewValue);
        }

        private void ComboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            NamedColor selected = comboColors.SelectedItem as NamedColor;
            if (selected != null)
            {
                SelectedColor = selected.Color;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_updating)
            {
                return;
            }

            SelectedColor = Color.FromRgb(
                (byte)sliderRed.Value,
                (byte)sliderGreen.Value,
                (byte)sliderBlue.Value);
        }

        /// <summary>
        /// Coloca os controles em sintonia com a cor atual sem disparar novos eventos.
        /// </summary>
        private void SyncControls(Color color)
        {
            _updating = true;
            try
            {
                sliderRed.Value = color.R;
                sliderGreen.Value = color.G;
                sliderBlue.Value = color.B;
                borderPreview.Background = Helper.CreateFrozenBrush(color);
                comboColors.SelectedItem = FindNamedColor(color);
            }
            finally
            {
                _updating = false;
            }
        }

        private NamedColor FindNamedColor(Color color)
        {
            foreach (NamedColor candidate in _namedColors)
            {
                if (candidate.Color.R == color.R &&
                    candidate.Color.G == color.G &&
                    candidate.Color.B == color.B)
                {
                    return candidate;
                }
            }

            return null;
        }
    }
}
