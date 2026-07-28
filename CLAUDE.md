# Pequenas Finanças — Convenções do Projeto

App desktop para saber quanto dinheiro livre sobra no mês e quanto dá para gastar por dia.
Stack: .NET 10, WPF + BlazorWebView (Razor/CSS), banco em arquivo JSON.

## Idioma do código

Todo o código é escrito em **português**: classes, métodos, propriedades, variáveis,
parâmetros, rotas, campos do JSON e textos de interface.

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

## Sem comentários

O código **não tem comentários** — nem `//`, nem `/* */`, nem `///` de documentação,
nem `@* *@` no Razor. O nome da classe, do método e da variável explica o que o código faz.

Quando um trecho parece precisar de comentário, o problema é o código: extraia um método
com nome melhor, dê nome à constante ou simplifique a expressão. Explicação de uso do app
vai para a documentação funcional, não para dentro do código.

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

Tons de apoio, derivados dos anteriores. Também vivem no mesmo bloco de variáveis:

| Papel                          | Variável           | Cor         |
|--------------------------------|--------------------|-------------|
| Fundo suave do primary         | `--primary-claro`  | `#0F766E1A` |
| Texto sobre fundo colorido     | `--sobre-primary`  | `#FFFFFF`   |
| Texto secundário               | `--text-suave`     | `#64748B`   |
| Fundo de aviso de erro         | `--error-fundo`    | `#FEF2F2`   |
| Borda de aviso de erro         | `--error-borda`    | `#FECACA`   |
| Anel de foco dos campos        | `--foco`           | `#14B8A62E` |
| Véu escuro atrás do modal      | `--veu-modal`      | `#0F172A73` |

Uso: entradas de dinheiro em `--success`, saídas em `--error`, reservas e destaques
em `--primary`/`--secondary`, alertas e vencimentos em `--accent`.

A janela WPF em `MainWindow.xaml` repete o valor de `--background` porque o WPF não lê CSS.
É o único lugar fora do `app.css` com cor fixa, e as duas mudam juntas.

## Segurança dos dados (regra obrigatória)

Este é um **repositório público**. Nada sensível pode ser publicado nem rastreado pelo git.

Nunca versionar, em hipótese alguma:

- **O banco de dados** — qualquer `dados.json`, `dados.json.tmp` ou a pasta `backups/`.
  Ele contém salário, gastos e reservas do usuário. Vive apenas em
  `%AppData%\PequenasFinancas` e jamais é copiado para dentro do repositório.
- **Chaves e segredos** — token, senha, string de conexão, chave de API, certificado
  (`.pem`, `.pfx`, `.key`), `.env`, `secrets.json` ou credencial de qualquer tipo.
  O app não usa nenhum serviço externo; se um dia usar, o segredo fica fora do repositório.
- **Dados pessoais reais** — valores, nomes, extratos ou capturas de tela com dados
  verdadeiros. Exemplos em código, testes, documentação e imagens usam **dados fictícios**.

Antes de cada commit:

1. Rodar `git status` e conferir o que está sendo adicionado.
2. Nunca usar `git add -f` para forçar um arquivo ignorado.
3. Conferir que o `.gitignore` continua bloqueando `dados.json`, `backups/`, `.env` e chaves.

Se um segredo ou dado pessoal for enviado por engano, ele deve ser tratado como vazado:
trocar a credencial e limpar o histórico, não basta apagar em um commit novo.

## Commits

- Mensagens em português, no imperativo ("Adiciona resumo mensal").
- **Proibido** incluir `Co-Authored-By: Claude` ou qualquer rodapé de geração automática.
- **Commitar sempre ao terminar uma alteração**, sem esperar o usuário pedir. Não acumular
  vários assuntos no mesmo commit: cada mudança concluída vira um commit próprio, pequeno e
  com um único propósito. Se uma entrega mexeu em código e na documentação funcional do
  mesmo assunto, os dois vão juntos no mesmo commit.

## Estrutura

```
src/PequenasFinancas.Core/   regras de negócio e persistência (net10.0)
src/PequenasFinancas.App/    WPF + BlazorWebView (net10.0-windows)
tests/PequenasFinancas.Tests/ testes das regras de cálculo (xUnit)
```
