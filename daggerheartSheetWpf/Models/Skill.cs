using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace daggerheartSheetWpf.Models
{
    /// <summary>
    /// Uma habilidade, magia ou carta de domínio do personagem.
    /// </summary>
    public class Skill : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        private SkillSource _source = SkillSource.Class;
        private int _recallCost;
        private string _type = string.Empty;
        private string _range = string.Empty;
        private string _damage = string.Empty;
        private string _trait = string.Empty;
        private string _domain = string.Empty;
        private Color? _chosenColor;

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

        public SkillSource Source
        {
            get { return _source; }
            set
            {
                if (_source == value)
                {
                    return;
                }

                _source = value;
                OnPropertyChanged();
                OnPropertyChanged("SourceName");
                OnPropertyChanged("Background");
                OnPropertyChanged("Foreground");
            }
        }

        public int RecallCost
        {
            get { return _recallCost; }
            set
            {
                if (_recallCost == value)
                {
                    return;
                }

                _recallCost = value;
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

        public string Domain
        {
            get { return _domain; }
            set
            {
                if (_domain == value)
                {
                    return;
                }

                _domain = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cor escolhida pelo jogador. Quando fica nula o painel usa a cor padrão da origem.
        /// </summary>
        public Color? ChosenColor
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

        public string SourceName
        {
            get { return SkillSources.GetLabel(_source); }
        }

        public Color PanelColor
        {
            get { return _chosenColor.HasValue ? _chosenColor.Value : SkillSources.GetDefaultColor(_source); }
        }

        public Brush Background
        {
            get { return Helper.CreateFrozenBrush(PanelColor); }
        }

        public Brush Foreground
        {
            get { return Helper.CreateFrozenBrush(Helper.GetContrastColor(PanelColor)); }
        }

        /// <summary>
        /// Texto com todos os dados preenchidos da habilidade, usado na janela de detalhes.
        /// </summary>
        public string ToDetailText()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Nome: " + _name);
            builder.AppendLine("Fonte: " + SourceName);

            AppendIfFilled(builder, "Tipo", _type);
            AppendIfFilled(builder, "Domínio", _domain);
            AppendIfFilled(builder, "Atributo", _trait);
            AppendIfFilled(builder, "Alcance", _range);
            AppendIfFilled(builder, "Dano", _damage);

            builder.AppendLine("Custo de troca: " + _recallCost);

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
