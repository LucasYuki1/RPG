using System.Windows;
using daggerheartSheetWpf.Models;

namespace daggerheartSheetWpf.Dialogs
{
    /// <summary>
    /// Janela de cadastro de uma habilidade.
    /// </summary>
    public partial class AddSkillWindow : Window
    {
        private static readonly string[] Types = { string.Empty, "Habilidade", "Magia" };

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

        private static readonly string[] Domains =
        {
            string.Empty,
            "Arcano",
            "Códice",
            "Esplendor",
            "Falange",
            "Graça",
            "Lâmina",
            "Meia-noite",
            "Sabedoria",
            "Sangue",
            "Valor"
        };

        public AddSkillWindow()
        {
            InitializeComponent();

            comboSource.ItemsSource = SkillSources.CreateOptions();
            comboType.ItemsSource = Types;
            comboTrait.ItemsSource = Traits;
            comboRange.ItemsSource = Ranges;
            comboDomain.ItemsSource = Domains;

            Loaded += delegate { textBoxName.Focus(); };
        }

        /// <summary>
        /// Habilidade criada pela janela. Só fica preenchida quando o jogador salva.
        /// </summary>
        public Skill Skill { get; private set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SkillSourceOption source = comboSource.SelectedItem as SkillSourceOption;

            if (string.IsNullOrWhiteSpace(textBoxName.Text) || source == null)
            {
                MessageBox.Show(
                    this,
                    "Digite o nome da habilidade e selecione a fonte!",
                    "Adicionar Habilidade",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    textBoxName.Focus();
                }
                else
                {
                    comboSource.Focus();
                }

                return;
            }

            Skill = new Skill
            {
                Name = textBoxName.Text.Trim(),
                Description = textBoxDescription.Text,
                Source = source.Value,
                RecallCost = numericRecallCost.Value,
                Type = GetSelectedText(comboType),
                Range = GetSelectedText(comboRange),
                Damage = textBoxDamage.Text.Trim(),
                Trait = GetSelectedText(comboTrait),
                Domain = GetSelectedText(comboDomain),
                ChosenColor = checkBoxCustomColor.IsChecked == true
                    ? colorPicker.SelectedColor
                    : (System.Windows.Media.Color?)null
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
