using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace daggerheartSheetWpf.Models
{
    /// <summary>
    /// Um item do inventário do personagem.
    /// </summary>
    public class InventoryItem : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _type = string.Empty;
        private int _tier;
        private string _trait = string.Empty;
        private string _range = string.Empty;
        private string _damage = string.Empty;
        private int _amount = 1;
        private Color _chosenColor = Colors.White;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayName");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                {
                    return;
                }

                _type = value;
                OnPropertyChanged();
            }
        }

        public int Tier
        {
            get { return _tier; }
            set
            {
                if (_tier == value)
                {
                    return;
                }

                _tier = value;
                OnPropertyChanged();
            }
        }

        public string Trait
        {
            get { return _trait; }
            set
            {
                if (_trait == value)
                {
                    return;
                }

                _trait = value;
                OnPropertyChanged();
            }
        }

        public string Range
        {
            get { return _range; }
            set
            {
                if (_range == value)
                {
                    return;
                }

                _range = value;
                OnPropertyChanged();
            }
        }

        public string Damage
        {
            get { return _damage; }
            set
            {
                if (_damage == value)
                {
                    return;
                }

                _damage = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value)
                {
                    return;
                }

                _amount = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayName");
            }
        }

        public Color ChosenColor
        {
            get { return _chosenColor; }
            set
            {
                if (_chosenColor == value)
                {
                    return;
                }

                _chosenColor = value;
                OnPropertyChanged();
                OnPropertyChanged("Background");
                OnPropertyChanged("Foreground");
            }
        }

        /// <summary>
        /// Nome mostrado na lista, com a quantidade quando houver mais de uma unidade.
        /// </summary>
        public string DisplayName
        {
            get { return _amount > 1 ? string.Format("{0}  x{1}", _name, _amount) : _name; }
        }

        public Brush Background
        {
            get { return Helper.CreateFrozenBrush(_chosenColor); }
        }

        public Brush Foreground
        {
            get { return Helper.CreateFrozenBrush(Helper.GetContrastColor(_chosenColor)); }
        }

        /// <summary>
        /// Texto com todos os dados preenchidos do item, usado na janela de detalhes.
        /// </summary>
        public string ToDetailText()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Nome: " + _name);
            builder.AppendLine("Quantidade: " + _amount);

            AppendIfFilled(builder, "Tipo", _type);

            if (_tier > 0)
            {
                builder.AppendLine("Tier: " + _tier);
            }

            AppendIfFilled(builder, "Atributo", _trait);
            AppendIfFilled(builder, "Alcance", _range);
            AppendIfFilled(builder, "Dano", _damage);

            if (!string.IsNullOrWhiteSpace(_description))
            {
                builder.AppendLine();
                builder.AppendLine("Descrição:");
                builder.AppendLine(_description);
            }

            return builder.ToString().TrimEnd();
        }

        private static void AppendIfFilled(StringBuilder builder, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                builder.AppendLine(label + ": " + value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
