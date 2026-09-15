# Ficha de Personagem - Daggerheart

Ficha de personagem para o RPG Daggerheart, em duas interfaces que compartilham as
mesmas funcionalidades:

| Projeto | Interface | Observação |
| --- | --- | --- |
| `daggerheartSheet` | Windows Forms | Versão original |
| `daggerheartSheetWpf` | WPF | Versão atual, com exclusão de itens do inventário |

Ambos são aplicações .NET Framework 4.7.2 e ficam na mesma solução
(`daggerheartSheet.slnx`). Para rodar a versão WPF, marque `daggerheartSheetWpf`
como projeto de inicialização no Visual Studio (botão direito no projeto →
**Definir como Projeto de Inicialização**) e execute com F5.

## O que a ficha faz

**Aba "Atributos e Recursos"**

- Identidade: nome, pronomes, herança, classe e nível.
- Atributos: Agilidade, Força, Finesse, Instinto, Presença e Conhecimento, cada um
  com valor e caixa de marcação.
- Defesa: evasão, armadura, pontos de armadura e os limiares de dano maior e severo.
- Recursos: vida, estresse e esperança, com valor atual e máximo.

**Aba "Inventário e Habilidades"**

- Inventário: adicionar itens (nome, quantidade, tipo, tier, atributo, alcance,
  dano, descrição e cor), ver os detalhes e **excluir itens**.
- Habilidades separadas por fonte (Classe/Subclasse, Carta de Domínio,
  Ancestralidade/Comunidade e Outros), com cor padrão por fonte ou cor escolhida
  pelo jogador.

## Como excluir um item do inventário (somente na versão WPF)

Qualquer um dos caminhos abaixo, todos pedindo confirmação antes de remover:

- botão **X** na própria linha do item;
- botão **Remover item**, embaixo da lista, com o item selecionado;
- tecla **Delete** com o item selecionado;
- clique com o botão direito no item → **Excluir item**.

Para ver os detalhes de um item ou habilidade: clique duas vezes na linha, use o
botão **i** ou o menu do botão direito.
