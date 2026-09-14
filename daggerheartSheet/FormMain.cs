using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace daggerheartSheet
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = null;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxPronouns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = null;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxHeritage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = null;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxClass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = null;
                e.SuppressKeyPress = true;
            }
        }


        private void buttonAddSkill_Click(object sender, EventArgs e)
        {
            using (FormAddSkill window = new FormAddSkill())
            {
                if (window.ShowDialog() == DialogResult.OK)
                {
                    Panel skillPanel = new Panel();

                    Skill skill = new Skill
                    {
                        Name = window.SkillName,
                        Description = window.SkillDescription,
                        Source = window.SkillSource,
                        RecallCost = window.SkillRecallCost,
                        Type = window.SkillType,
                        Range = window.SkillRange,
                        Damage = window.SkillDamage,
                        Trait = window.SkillTrait,
                        Domain = window.SkillDomain,
                    };

                    if (window.SkillSource == "Classe/Subclasse")
                    {
                        skillPanel.BackColor = Color.LightBlue;
                    }
                    else if (window.SkillSource == "Carta de Dominio")
                    {
                        skillPanel.BackColor = Color.LightGreen;
                    }
                    else if (window.SkillSource == "Ancestraldade/Comunidade")
                    {
                        skillPanel.BackColor = Color.LightYellow;
                    }
                    else if (window.SkillSource == "Outro")
                    {
                        skillPanel.BackColor = Color.LightGray;


                    }
                }
            }
        }


        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            using (FormAddItem window = new FormAddItem())
            {
                if (window.ShowDialog() == DialogResult.OK)
                {     
                    Panel itemPanel = new Panel();

                    InventoryItem item = new InventoryItem
                    {
                        Name = window.ItemName,
                        Description = window.ItemDescription,
                        Type = window.ItemType,
                        Tier = int.TryParse(window.ItemTier, out int tier) ? tier : 0,
                        Trait = window.ItemTrait,
                        Range = window.ItemRange,
                        ChosenColor = window.chosenColor,
                        Amount = int.TryParse(window.ItemAmount, out int amount) ? amount : 0,
                        Damage = window.ItemDamage
                    };

                            

                    // Panel size
                    itemPanel.Width = flowLayoutInventory.ClientSize.Width - 30; 
                    itemPanel.Height = 50;

                    // Panel color
                    itemPanel.BackColor = item.ChosenColor;
                    itemPanel.Margin = new Padding(5);

                    // Panel Text
                    Label nameLabel = new Label();
                    nameLabel.Text = window.ItemName;
                    nameLabel.ForeColor = Helper.GetContrastColor(item.ChosenColor);
                    nameLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    nameLabel.Location = new Point(10, 10);
                    nameLabel.AutoSize = true;

                    itemPanel.Tag = item;
                    itemPanel.Click += ItemPanel_Click;
                    itemPanel.Controls.Add(nameLabel);

                    flowLayoutInventory.Controls.Add(itemPanel);
                }
            }
        }
        private void ItemPanel_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            InventoryItem item = (InventoryItem)panel.Tag;

            string message =
                $"Nome: {item.Name}\n" +
                $"Quantidade: {item.Amount}\n" +
                $"Descrição: {item.Description}\n" +
                $"Tipo: {item.Type}\n" +
                $"Tier: {item.Tier}\n" +
                $"Trait: {item.Trait}\n" +
                $"Alcance: {item.Range}\n" +
                $"Dano: {item.Damage}";

            MessageBox.Show(message, "Informações do Item");
        }
    }
}
