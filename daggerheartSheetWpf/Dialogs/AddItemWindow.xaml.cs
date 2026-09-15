using System.Windows;
using daggerheartSheetWpf.Models;

namespace daggerheartSheetWpf.Dialogs
{
    /// <summary>
    /// Janela de cadastro de um item do inventário.
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private static readonly string[] Types =
        {
            string.Empty,
            "Armadura",
            "Cadeira de rodas de combate",
            "Consumível",
            "Item",
            "Arma primária",
            "Arma secundária"
        };

        private static readonly string[] Tiers = { string.Empty, "1", "2", "3", "4" };

        private static readonly string[] Traits =
        {
            string.Empty,
            "Agilidade",
            "Força",
            "Finesse",
            "Instinto",
            "Presença",
            "Conhecimento"
        };

        private static readonly string[] Ranges =
        {
            string.Empty,
            "Corpo-a-corpo",
            "Muito perto",
            "Perto",
            "Distante",
            "Muito distante"
        };

        public AddItemWindow()
        {
            InitializeComponent();

            comboType.ItemsSource = Types;
            comboTier.ItemsSource = Tiers;
            comboTrait.ItemsSource = Traits;
            comboRange.ItemsSource = Ranges;

            Loaded += delegate { textBoxName.Focus(); };
        }

        /// <summary>
        /// Item criado pela janela. Só fica preenchido quando o jogador salva.
        /// </summary>
        public InventoryItem Item { get; private set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show(
                    this,
                    "Digite o nome do item!",
                    "Adicionar Item",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                textBoxName.Focus();
                return;
            }

            int tier;
            if (!int.TryParse(comboTier.SelectedItem as string, out tier))
            {
                tier = 0;
            }

            Item = new InventoryItem
            {
                Name = textBoxName.Text.Trim(),
                Description = textBoxDescription.Text,
                Type = GetSelectedText(comboType),
                Tier = tier,
                Trait = GetSelectedText(comboTrait),
                Range = GetSelectedText(comboRange),
                Damage = textBoxDamage.Text.Trim(),
                Amount = numericAmount.Value,
                ChosenColor = colorPicker.SelectedColor
            };

            DialogResult = true;
        }

        private static string GetSelectedText(System.Windows.Controls.ComboBox comboBox)
        {
            string selected = comboBox.SelectedItem as string;
            return selected == null ? string.Empty : selected;
        }
    }
}
