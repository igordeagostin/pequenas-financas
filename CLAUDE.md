# Pequenas Finanças — Convenções do Projeto

App desktop para registro simples das finanças do dia a dia.
Stack: .NET 10, WPF + BlazorWebView (Razor/CSS), banco em arquivo JSON.

## Idioma do código

Todo o código é escrito em **português**: classes, métodos, propriedades, variáveis,
parâmetros, rotas, campos do JSON, textos de interface e comentários.

Permanecem em inglês apenas:

- palavras-chave da linguagem;
- APIs da BCL e do framework (`Task`, `List<T>`, `OnInitializedAsync`, `StateHasChanged`);
- arquivos cujo nome é exigido pelo framework (`App.xaml`, `MainWindow.xaml`,
  `_Imports.razor`, `Routes.razor`, `wwwroot`).

## Nomes

- Sem abreviações: `quantidadeParcelas`, nunca `qtdParc`.
- Métodos começam com verbo: `CalcularResumo`, `RegistrarDeposito`, `ObterParcelasDoMes`.
- Booleanos indicam estado: `EstaVigente`, `PossuiParcelasAbertas`, `Ativo`.
- Classes de serviço terminam com `Servico`; modelos usam o nome do conceito (`Cartao`, `Reserva`).

## Sem duplicação

- Toda regra de cálculo mora em um único ponto do projeto `PequenasFinancas.Core`
  e é reaproveitada pela interface. Nenhum arquivo `.razor` recalcula parcela, rateio ou saldo.
- Formulários usam os componentes compartilhados `ModalFormulario` e `CampoMoeda`.
- Listas de lançamentos usam um único componente de linha.
- CRUD dos cadastros herda de `ServicoCrud<T>`; recorrência vigente sai de `ServicoRecorrencia`.

## Código limpo

- Métodos curtos, com um único propósito.
- Nada de número mágico: usar constantes nomeadas.
- Dinheiro sempre em `decimal` — nunca `double` ou `float`.
- Nenhuma lógica de negócio no code-behind das páginas.
- O projeto `Core` não referencia WPF, Blazor ou qualquer biblioteca de interface.
- Cultura `pt-BR` para formatar valores e datas.

## Documentação funcional (obrigatória)

Toda alteração de código que muda o que o usuário vê ou faz no app precisa atualizar a
documentação funcional na mesma entrega. Sem isso, a tarefa não está concluída.

- A documentação fica em `docs/documentacao-funcional.md`.
- Se ela ainda não existir, crie antes de entregar a funcionalidade.
- Uma seção por funcionalidade, com: para que serve, como usar (passo a passo) e como o
  valor aparece no resumo do mês.

Como escrever:

- Linguagem simples e direta, como se explicasse para alguém que nunca usou o app.
- Nada de palavra difícil ou termo técnico: escreva "mês de referência", não "competência";
  "arquivo de dados", não "persistência"; "somar", não "agregar".
- Frases curtas, uma ideia por frase.
- Use exemplos com valores reais ("uma compra de R$ 4.800 em 12x vira 12 parcelas de R$ 400").
- Use listas e passo a passo numerado no lugar de parágrafos longos.
- Nada de detalhe de código: a documentação explica o uso, não a implementação.

Mudanças que **não** afetam o uso (refatoração interna, teste, ajuste de build) não exigem
atualização da documentação.

## Paleta de cores

Tema claro. As cores ficam em variáveis CSS em `wwwroot/css/app.css` e nenhum
componente escreve cor fixa fora dessas variáveis.

| Papel      | Variável        | Cor       |
|------------|-----------------|-----------|
| Primary    | `--primary`     | `#0F766E` |
| Secondary  | `--secondary`   | `#14B8A6` |
| Accent     | `--accent`      | `#F59E0B` |
| Background | `--background`  | `#F8FAFC` |
| Surface    | `--surface`     | `#FFFFFF` |
| Text       | `--text`        | `#0F172A` |
| Success    | `--success`     | `#22C55E` |
| Warning    | `--warning`     | `#F59E0B` |
| Error      | `--error`       | `#EF4444` |
| Border     | `--border`      | `#E2E8F0` |

Uso: entradas de dinheiro em `--success`, saídas em `--error`, reservas e destaques
em `--primary`/`--secondary`, alertas e vencimentos em `--accent`.

## Commits

- Mensagens em português, no imperativo ("Adiciona resumo mensal").
- **Proibido** incluir `Co-Authored-By: Claude` ou qualquer rodapé de geração automática.
- Nunca versionar dados pessoais: o `dados.json` do usuário fica em `%AppData%\PequenasFinancas`.

## Estrutura

```
src/PequenasFinancas.Core/   regras de negócio e persistência (net10.0)
src/PequenasFinancas.App/    WPF + BlazorWebView (net10.0-windows)
tests/PequenasFinancas.Tests/ testes das regras de cálculo (xUnit)
```
