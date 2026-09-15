using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using daggerheartSheetWpf.Dialogs;
using daggerheartSheetWpf.Models;

namespace daggerheartSheetWpf
{
    /// <summary>
    /// Janela principal da ficha: atributos, recursos, inventário e habilidades.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Inventory = new ObservableCollection<InventoryItem>();
            ClassSkills = new ObservableCollection<Skill>();
            DomainSkills = new ObservableCollection<Skill>();
            HeritageSkills = new ObservableCollection<Skill>();
            OtherSkills = new ObservableCollection<Skill>();

            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<InventoryItem> Inventory { get; private set; }

        public ObservableCollection<Skill> ClassSkills { get; private set; }

        public ObservableCollection<Skill> DomainSkills { get; private set; }

        public ObservableCollection<Skill> HeritageSkills { get; private set; }

        public ObservableCollection<Skill> OtherSkills { get; private set; }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow window = new AddItemWindow();
            window.Owner = this;

            if (window.ShowDialog() == true && window.Item != null)
            {
                Inventory.Add(window.Item);
                listBoxInventory.SelectedItem = window.Item;
                listBoxInventory.ScrollIntoView(window.Item);
            }
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            AddSkillWindow window = new AddSkillWindow();
            window.Owner = this;

            if (window.ShowDialog() == true && window.Skill != null)
            {
                GetSkillCollection(window.Skill.Source).Add(window.Skill);
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem(GetDataItem(sender) as InventoryItem);
        }

        private void RemoveItemMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem(GetDataItem(sender) as InventoryItem);
        }

        private void RemoveSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem(listBoxInventory.SelectedItem as InventoryItem);
        }

        private void InventoryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete)
            {
                return;
            }

            InventoryItem selected = listBoxInventory.SelectedItem as InventoryItem;
            if (selected != null)
            {
                RemoveItem(selected);
                e.Handled = true;
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDetails(GetDataItem(sender));
        }

        private void DetailsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowDetails(GetDataItem(sender));
        }

        private void ListItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowDetails(GetDataItem(sender));
        }

        /// <summary>
        /// Tira um item do inventário depois de confirmar com o jogador.
        /// </summary>
        private void RemoveItem(InventoryItem item)
        {
            if (item == null)
            {
                return;
            }

            MessageBoxResult answer = MessageBox.Show(
                this,
                string.Format("Deseja excluir \"{0}\" do inventário?", item.Name),
                "Excluir Item",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                Inventory.Remove(item);
            }
        }

        private void ShowDetails(object data)
        {
            InventoryItem item = data as InventoryItem;
            if (item != null)
            {
                MessageBox.Show(this, item.ToDetailText(), "Informações do Item");
                return;
            }

            Skill skill = data as Skill;
            if (skill != null)
            {
                MessageBox.Show(this, skill.ToDetailText(), "Informações da Habilidade");
            }
        }

        private ObservableCollection<Skill> GetSkillCollection(SkillSource source)
        {
            switch (source)
            {
                case SkillSource.Class:
                    return ClassSkills;
                case SkillSource.Domain:
                    return DomainSkills;
                case SkillSource.Heritage:
                    return HeritageSkills;
                default:
                    return OtherSkills;
            }
        }

        /// <summary>
        /// Recupera o item (ou habilidade) ligado ao botão, menu ou linha que disparou o evento.
        /// </summary>
        private static object GetDataItem(object sender)
        {
            FrameworkElement element = sender as FrameworkElement;
            return element == null ? null : element.DataContext;
        }
    }
}
