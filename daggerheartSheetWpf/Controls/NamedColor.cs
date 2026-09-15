using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

namespace daggerheartSheetWpf.Controls
{
    /// <summary>
    /// Uma das cores nomeadas do WPF, usada na lista do seletor de cores.
    /// </summary>
    public class NamedColor
    {
        public NamedColor(string name, Color color)
        {
            Name = name;
            Color = color;
            Brush = Helper.CreateFrozenBrush(color);
        }

        public string Name { get; private set; }

        public Color Color { get; private set; }

        public Brush Brush { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Monta a lista com todas as cores nomeadas (menos a transparente).
        /// </summary>
        public static IList<NamedColor> CreateAll()
        {
            List<NamedColor> colors = new List<NamedColor>();

            PropertyInfo[] properties = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType != typeof(Color))
                {
                    continue;
                }

                Color color = (Color)property.GetValue(null, null);
                if (color.A == 0)
                {
                    continue;
                }

                colors.Add(new NamedColor(property.Name, color));
            }

            colors.Sort(delegate(NamedColor first, NamedColor second)
            {
                return string.Compare(first.Name, second.Name, StringComparison.CurrentCulture);
            });

            return colors;
        }
    }
}
