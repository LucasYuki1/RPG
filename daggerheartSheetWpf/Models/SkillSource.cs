using System.Collections.Generic;
using System.Windows.Media;

namespace daggerheartSheetWpf.Models
{
    /// <summary>
    /// Origem de uma habilidade. Define em qual lista da ficha ela aparece.
    /// </summary>
    public enum SkillSource
    {
        Class,
        Domain,
        Heritage,
        Other
    }

    /// <summary>
    /// Opção de origem exibida no ComboBox da janela de cadastro.
    /// </summary>
    public class SkillSourceOption
    {
        public SkillSourceOption(SkillSource value, string label)
        {
            Value = value;
            Label = label;
        }

        public SkillSource Value { get; private set; }

        public string Label { get; private set; }

        public override string ToString()
        {
            return Label;
        }
    }

    public static class SkillSources
    {
        /// <summary>
        /// Nome da origem como ele aparece para o jogador.
        /// </summary>
        public static string GetLabel(SkillSource source)
        {
            switch (source)
            {
                case SkillSource.Class:
                    return "Classe/Subclasse";
                case SkillSource.Domain:
                    return "Carta de Domínio";
                case SkillSource.Heritage:
                    return "Ancestralidade/Comunidade";
                default:
                    return "Outros";
            }
        }

        /// <summary>
        /// Cor padrão do painel da habilidade quando o jogador não escolhe uma cor.
        /// </summary>
        public static Color GetDefaultColor(SkillSource source)
        {
            switch (source)
            {
                case SkillSource.Class:
                    return Colors.LightBlue;
                case SkillSource.Domain:
                    return Colors.LightGreen;
                case SkillSource.Heritage:
                    return Colors.LightYellow;
                default:
                    return Colors.LightGray;
            }
        }

        public static IList<SkillSourceOption> CreateOptions()
        {
            return new List<SkillSourceOption>
            {
                new SkillSourceOption(SkillSource.Class, GetLabel(SkillSource.Class)),
                new SkillSourceOption(SkillSource.Domain, GetLabel(SkillSource.Domain)),
                new SkillSourceOption(SkillSource.Heritage, GetLabel(SkillSource.Heritage)),
                new SkillSourceOption(SkillSource.Other, GetLabel(SkillSource.Other))
            };
        }
    }
}
