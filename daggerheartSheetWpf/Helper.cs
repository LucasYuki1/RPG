using System.Windows.Media;

namespace daggerheartSheetWpf
{
    /// <summary>
    /// Pequenos utilitários compartilhados pela interface WPF.
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Devolve preto ou branco, o que tiver melhor contraste sobre a cor informada.
        /// </summary>
        public static Color GetContrastColor(Color backgroundColor)
        {
            double brightness =
                (0.299 * backgroundColor.R) +
                (0.587 * backgroundColor.G) +
                (0.114 * backgroundColor.B);

            return brightness > 128 ? Colors.Black : Colors.White;
        }

        /// <summary>
        /// Cria um pincel sólido já congelado (mais barato para a renderização).
        /// </summary>
        public static Brush CreateFrozenBrush(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }
    }
}
