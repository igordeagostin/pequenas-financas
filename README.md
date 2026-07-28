<img src="src/PequenasFinancas.App/wwwroot/img/logo.svg" width="88" alt="Pequenas Finanças" />

# Pequenas Finanças

![status](https://img.shields.io/badge/status-em%20produção-brightgreen)
![.NET](https://img.shields.io/badge/.NET-10-512BD4)
![plataforma](https://img.shields.io/badge/plataforma-Windows-0078D4)

Aplicativo de computador para saber **quanto dinheiro livre você tem no mês**.

Sem complicação de sistema contábil e sem cadastro na internet: você cadastra o que entra,
o que já está comprometido e o que ficou parcelado, e o app responde as duas perguntas que
importam — **quanto sobra livre no mês e quanto dá para gastar por dia.**

Ele não é um caderno de gastos: você não anota o mercado nem o cafezinho.

## Status

O aplicativo **está em produção** e em uso no dia a dia.

![Resumo do mês](docs/imagens/resumo-do-mes.png)

## O que ele faz

- **Renda** — cadastre o salário uma vez e ele aparece sozinho em todos os meses.
  Também dá para lançar entradas que vieram só naquele mês (freela, 13º, uma venda).
- **Contas e parcelas** — aluguel, internet, escola, carnê, empréstimo: cadastre uma vez,
  com mês de início e de fim, e a conta se repete pelos meses. A tela mostra quanto do mês
  já foi pago e quanto ainda falta.
- **Cartões de crédito** — cadastre seus cartões com o dia do vencimento da fatura.
- **Compras no cartão** — lance a compra uma vez, informe em quantas vezes e em qual
  cartão. As parcelas aparecem sozinhas em cada mês, com o progresso (`3/12`).
- **Dinheiro guardado** — crie reservas e registre quanto guardou (ou resgatou) em
  qualquer mês, com saldo acumulado e barra de progresso até o objetivo.
- **Resumo do mês** — abre sempre no mês atual e mostra quanto entra, quanto sai,
  quanto foi guardado e quanto fica livre, com os gastos somados por categoria.
- **Quanto dá para gastar por dia** — o dinheiro livre dividido pelos dias que ainda
  faltam no mês.
- **Planejamento da semana** — abra a semana, veja a fatia dela dentro do que resta do
  mês, anote os gastos prováveis e, ao fechar, informe quanto sobrou de verdade. A
  próxima semana começa desse saldo.

![Nova compra no cartão](docs/imagens/nova-compra.png)

## Como o dinheiro livre é calculado

```
Livre no mês = renda + entradas extras
             − contas e parcelas
             − parcelas dos cartões
             − dinheiro guardado no mês

Por dia      = livre no mês ÷ dias que ainda faltam
```

No mês em andamento os dias contados vão de hoje até o fim do mês; nos outros meses, o mês
inteiro. O valor por dia é arredondado para baixo, para a soma dos dias nunca passar do que
existe.

O resumo mostra também quanto ficaria livre **antes** de separar dinheiro na reserva.

As parcelas não são gravadas uma a uma: elas são calculadas a partir da compra. Quando o
valor não divide certinho, a última parcela absorve os centavos, para a soma bater com o
total (R$ 100 em 3x = 33,33 + 33,33 + 33,34).

## Tecnologias

- .NET 10
- WPF hospedando componentes Blazor (`BlazorWebView` + WebView2) — interface em Razor e CSS
- Banco de dados em um arquivo JSON simples
- Testes em xUnit

## Como executar

Você precisa de:

- Windows 10 ou 11
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- WebView2 Runtime (já vem instalado no Windows 11)

```bash
git clone https://github.com/igordeagostin/pequenas-financas.git
cd pequenas-financas

dotnet test                                     # roda os testes
dotnet run --project src/PequenasFinancas.App   # abre o aplicativo
```

Para gerar o executável:

```bash
dotnet publish src/PequenasFinancas.App -c Release
```

## Onde ficam os dados

```
%AppData%\PequenasFinancas\dados.json    seus dados
%AppData%\PequenasFinancas\backups\      cópias de segurança das últimas gravações
```

O arquivo é pessoal, fica só na sua máquina e **nunca** é versionado neste repositório.

## Estrutura do projeto

```
src/PequenasFinancas.Core/     regras de negócio e leitura/gravação do JSON
src/PequenasFinancas.App/      janela WPF, componentes Razor e CSS
tests/PequenasFinancas.Tests/  testes das regras de cálculo
docs/                          documentação funcional
```

## Documentação

- [Como usar o aplicativo](docs/documentacao-funcional.md) — explicação de cada tela, em
  linguagem simples.
- [CLAUDE.md](CLAUDE.md) — convenções de código do projeto.
