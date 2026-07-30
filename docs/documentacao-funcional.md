# Como usar o Pequenas Finanças

Este guia explica, em palavras simples, o que cada tela do aplicativo faz.

O objetivo do app é um só: **saber quanto dinheiro livre você tem no mês e quanto dá
para gastar por dia.**

Ele não serve para anotar cada compra do dia a dia. Você cadastra o que entra e o que já
está comprometido (contas, cartão, parcelas) e o app responde quanto ainda está livre.

---

## O básico: o app é dividido por mês

Tudo no app é organizado por mês e ano.

- Ao abrir, o app já mostra o **mês de hoje**.
- Use as setas `‹` e `›` no topo para ver o mês anterior ou o próximo.
- Se você foi para outro mês, aparece o botão **Voltar para hoje**.

O mês escolhido no topo vale para todas as telas ao mesmo tempo.

Seus dados ficam salvos no seu computador, num arquivo só seu. Nada vai para a internet.

---

## Ordenar as listas do app

Em toda tabela do app, o título de cada coluna é um botão.

1. Clique no título da coluna para ordenar por ela, do menor para o maior.
2. Clique no mesmo título de novo para inverter, do maior para o menor.

Uma setinha ao lado do título mostra por qual coluna a lista está ordenada e para que lado.

Serve para qualquer coluna: nome, data, dia do vencimento ou valor. Por exemplo, na tela
**Contas e parcelas**, clicar em **Valor neste mês** joga para cima a conta que mais pesa
no mês.

A ordem que você escolhe vale só enquanto você está olhando aquela tela. Ao sair e voltar,
a lista aparece na ordem normal de novo.

---

## Como digitar valores em dinheiro

Em todo campo de dinheiro do app você pode separar os centavos com **vírgula ou ponto**,
do jeito que for mais rápido para você. Os dois valem a mesma coisa.

- `699,6` e `699.6` viram **R$ 699,60**.
- `1234,56` e `1234.56` viram **R$ 1.234,56**.
- `1.234,56` também funciona: o ponto é lido como separador de milhar.
- `87` vira **R$ 87,00**.

Quando você sai do campo, o app arruma o que foi digitado e mostra sempre com vírgula e
duas casas: `699,6` aparece como `699,60`.

Se você digitar uma letra ou algo que não é número, o app ignora e volta para o último
valor válido assim que você sai do campo.

Campos que não são de dinheiro continuam aceitando só número inteiro: dia do mês
(de 1 a 31) e quantidade de parcelas.

---

## Primeiros passos

Para o app ficar útil, faça nesta ordem:

1. Cadastre sua **renda** (o salário, por exemplo).
2. Cadastre suas **contas e parcelas** (aluguel, internet, carnê da loja…).
3. Cadastre seus **cartões**.
4. Lance suas **compras no cartão**.
5. Crie um lugar para o seu **dinheiro guardado**.

Depois disso, a tela **Resumo do mês** mostra tudo junto.

Se quiser controlar o dinheiro semana a semana, use a tela
**Planejamento da semana** depois desses passos.

---

## Resumo do mês

É a primeira tela do app. Mostra o retrato do mês escolhido.

No topo aparecem cinco números:

| Número | O que significa |
|---|---|
| **Entra** | Todo o dinheiro que você recebe no mês |
| **Sai** | Contas e parcelas + cartões |
| **Falta pagar** | Do que sai, quanto ainda não foi marcado como pago |
| **Livre no mês** | Quanto sobra depois de pagar tudo |
| **Guardado até aqui** | Quanto você tem separado em todas as reservas |

A conta do dinheiro livre é assim:

```
Livre no mês = tudo que entra − contas e parcelas − cartões
```

O dinheiro que você guardou **não entra nessa conta**. Guardar não é gastar: o dinheiro
continua sendo seu, só mudou de lugar. Quando os gastos passam do que entrou, o valor
fica vermelho.

Mais abaixo a tela mostra:

- **Quanto dá para gastar por dia**: explicado na próxima seção.
- **Tudo que entra e sai no mês**: uma lista com cada entrada e cada saída.
  Verde é dinheiro que entra, vermelho é dinheiro que sai. O que já foi pago
  ganha uma etiqueta verde **pago**.
- **Faturas dos cartões**: quanto cada cartão vai cobrar neste mês, com o botão para
  marcar a fatura como paga.
- **Para onde vai o dinheiro**: seus gastos somados por categoria.
- **Sua semana**: um resumo curto da semana que está aberta.
- **Dinheiro guardado**: o saldo de cada reserva.

---

## Quanto dá para gastar por dia

Logo abaixo dos quatro números, o resumo mostra um valor grande: **quanto você pode
gastar por dia** sem estourar o mês.

A conta é simples:

```
Por dia = dinheiro livre do mês ÷ dias que ainda faltam
```

Os dias contados mudam conforme o mês que você está olhando:

- **No mês de hoje**: conta de hoje até o último dia do mês, incluindo hoje.
  Se hoje é dia 27 e o mês tem 31 dias, são 5 dias.
- **Em qualquer outro mês** (passado ou futuro): conta o mês inteiro.
  Em julho, 31 dias.

Exemplo: sobraram R$ 3.900 livres e faltam 5 dias para o mês acabar.
O app mostra **R$ 780,00 por dia**.

Outro exemplo: você está olhando agosto, que ainda nem começou. Se ficam R$ 3.900 livres
e agosto tem 31 dias, o app mostra **R$ 125,80 por dia**.

Detalhes que valem saber:

- O valor sempre é arredondado **para baixo**, para a soma dos dias nunca passar do que
  você tem. Por isso pode faltar alguns centavos no fim do mês — a favor do seu bolso.
- O dinheiro que você guardou não entra na conta. Se sobraram R$ 3.900 e você guardou
  R$ 500, o app continua dividindo R$ 3.900 pelos dias.
- Se os gastos passaram do que entrou, o valor aparece como R$ 0,00 e o app avisa que
  não há dinheiro livre para dividir por dia.

---

## Marcar o que já foi pago

Saber quanto sai no mês é uma coisa. Saber **o que você já pagou** é outra. Esta marcação
serve para você não pagar a mesma conta duas vezes nem esquecer uma que ainda falta.

Duas coisas podem ser marcadas como pagas:

- **Contas e parcelas** (o aluguel de julho, a internet de julho, a parcela do sofá de julho…).
- **Faturas do cartão** (a fatura do Nubank de julho).

### Como marcar

Em todas as telas o botão é o mesmo:

1. Escolha o mês na barra de cima do app.
2. Clique em **Marcar pago**.
3. O botão fica verde e passa a mostrar **Pago**.

Para desmarcar, clique de novo no mesmo botão.

Onde fica o botão:

| O que você quer marcar | Onde clicar |
|---|---|
| Conta ou parcela | Tela **Contas e parcelas**, coluna **Pagamento** |
| Fatura do cartão | Tela **Resumo do mês**, quadro **Faturas dos cartões** |

### A marca vale só para um mês

Cada marcação vale para **um mês de cada vez**. O aluguel pago em julho continua
aparecendo como não pago em agosto — e é isso que você quer, porque em agosto ele vence
de novo.

Por isso, antes de marcar, confira o mês que está escolhido na barra de cima.

O botão só aparece quando aquela conta realmente cai no mês escolhido. Uma conta que
já terminou, ou uma que ainda não começou, não tem nada a pagar naquele mês.

### Marcar a fatura marca as parcelas

Você não marca parcela por parcela do cartão: marca **a fatura inteira**, porque é isso
que você paga de verdade — um pagamento só, no vencimento do cartão.

Exemplo: em julho o Nubank cobra a parcela do notebook (R$ 300) e a do fone (R$ 100),
total de R$ 400. Ao marcar a fatura do Nubank como paga, as duas parcelas aparecem com a
etiqueta **pago** na lista do resumo.

### Como isso aparece no resumo do mês

O número **Falta pagar** mostra quanto do mês ainda não foi marcado.

Exemplo: o mês tem R$ 2.100 de gastos (R$ 1.500 de aluguel, R$ 200 de parcela do sofá e
R$ 400 de fatura do cartão). Você marca o aluguel e a fatura como pagos:

- Sai: R$ 2.100
- Falta pagar: R$ 200
- Embaixo do número aparece: *R$ 1.900 já pagos de R$ 2.100*

Quando não sobra nada para marcar, o app escreve **Tudo pago neste mês**.

Marcar como pago **não muda o dinheiro livre** nem o quanto dá para gastar por dia. O
gasto já estava contado desde que você cadastrou a conta: pagar só confirma que ele saiu
da sua mão. Se marcar tirasse o valor da conta, o dinheiro livre subiria de mentira a cada
conta paga.

---

## Planejamento da semana

O mês inteiro é muito tempo para se controlar de uma vez. Esta tela quebra o mês em
semanas: ela mostra **quanto dá para gastar nesta semana** com o dinheiro que ainda
resta no mês, deixa você anotar o que acha que vai gastar e, no fim da semana, pergunta
quanto sobrou de verdade.

### Abrir a semana

A semana não começa sozinha: você abre quando quiser.

1. Clique em **Abrir semana**.
2. O app mostra com quanto dinheiro a semana começa.
3. Escolha o primeiro dia da semana (o app já sugere um).
4. Confirme.

A semana vale por sete dias, ou até o fim do mês, o que vier primeiro. Uma semana aberta
no dia 29 de julho termina no dia 31.

Só existe uma semana aberta por vez em cada mês.

### Com quanto a semana começa

- **É a primeira semana do mês**: começa com o **dinheiro livre do mês**, o mesmo valor
  que aparece no resumo (o que entra, menos contas, parcelas e cartões).
- **Já teve outra semana antes**: começa com **o saldo que você informou** ao fechar a
  semana anterior.

O dinheiro que está guardado nas reservas **não entra nessa conta**, nem o que você
guardou no mês, nem o saldo inicial das reservas. Se você tem R$ 7.000 guardados e o mês
não teve nada a receber nem a pagar, a semana começa com R$ 0,00, e não com R$ -7.000.

### Quanto dá para gastar na semana

O app divide o dinheiro pelos dias que faltam no mês e multiplica pelos dias da semana:

```
Pode gastar na semana = dinheiro da semana ÷ dias que faltam no mês × dias da semana
```

Exemplo: no dia 11 de julho você tem R$ 2.100 livres. Faltam 21 dias no mês, então dá
R$ 100 por dia. A semana de 11 a 17 tem 7 dias, então **pode gastar R$ 700 na semana** e
R$ 1.400 ficam para o resto do mês.

A semana curta do fim do mês fica com tudo que sobrou: se você abre a semana no dia 29 e
tem R$ 300, os três últimos dias recebem os R$ 300.

### Anotar gastos prováveis

Gasto provável é o que você **acha** que vai gastar na semana: mercado, gasolina,
farmácia, o almoço de sábado. Serve para você ver o buraco antes de cair nele.

1. Clique em **Novo gasto provável**.
2. Escreva o que é (ex.: `mercado da semana`).
3. Informe quanto acha que vai gastar.
4. Escolha o dia, dentro da semana aberta.

Os quatro números do topo mostram na hora:

| Número | O que significa |
|---|---|
| **Começou com** | O dinheiro que a semana tinha ao abrir |
| **Pode gastar na semana** | A fatia da semana, pela conta acima |
| **Gastos prováveis** | A soma do que você anotou |
| **Ainda livre na semana** | Quanto da fatia ainda não tem destino |

Logo abaixo aparece **quanto dá para gastar por dia nesta semana**: o que ainda está
livre dividido pelos dias que faltam até o fim da semana.

Exemplo: dos R$ 700 da semana você anotou R$ 420 de gastos prováveis. Ficam R$ 280
livres. Se ainda faltam 4 dias, o app mostra **R$ 70,00 por dia**.

Se os gastos prováveis passarem da fatia da semana, os valores ficam vermelhos e o app
avisa que você já planejou mais do que pode.

### Fechar a semana

Quando a semana acabar (ou quando você quiser encerrar), feche:

1. Clique em **Fechar semana**.
2. O app mostra quanto **deveria** sobrar, pelo que você anotou.
3. Olhe sua conta e sua carteira e informe **quanto sobrou de verdade**.
4. Confirme.

A semana fechada vai para a lista **Semanas já fechadas no mês**, com o período, com
quanto começou, quanto podia gastar, quanto sobrou de verdade e a **diferença do
previsto**:

- Diferença verde: sobrou mais do que você imaginava.
- Diferença vermelha: sobrou menos, você gastou além do anotado.

Exemplo: pelos gastos anotados deveria sobrar R$ 1.680, mas sobraram R$ 1.500.
A diferença é **− R$ 180,00**.

Depois de fechar, abra a próxima semana. Ela começa com o valor que você informou, e o
app já sugere começar no dia seguinte ao fechamento. Como a conta usa o saldo real, o
erro de uma semana não se arrasta: a semana seguinte já nasce com o valor certo.

### Como isso aparece no resumo do mês

O planejamento da semana **não muda** o "Livre no mês". Gasto provável é palpite, não
conta lançada — se ele entrasse na soma, uma compra no cartão poderia ser contada duas
vezes.

No resumo do mês aparece um quadro **Sua semana**, só para olhar, com o período, quanto
dá para gastar na semana, os gastos prováveis e quanto ainda está livre. Se nenhuma
semana estiver aberta, o quadro convida você a abrir uma.

---

## Renda

Aqui você cadastra o dinheiro que entra.

### Renda de todo mês

Cadastre uma vez e ela aparece sozinha em todos os meses seguintes.

Para cadastrar:

1. Clique em **+ Nova renda**.
2. Escreva a descrição (ex.: `Salário`).
3. Informe o valor que você recebe por mês.
4. Escolha se é a renda **principal** ou **complementar**.
5. Informe o dia em que o dinheiro cai na conta.
6. Em **Começa em**, escolha o primeiro mês em que essa renda vale.
7. **Termina em** é opcional. Deixe em branco se não tem data para acabar.

Exemplo: um salário de R$ 6.000 começando em janeiro de 2026 aparece em janeiro,
fevereiro, março e em todos os meses depois disso, sem você precisar lançar de novo.

### Entrou só neste mês

Para dinheiro que veio uma vez só: freela, 13º, venda de alguma coisa, presente.

1. Clique em **+ Nova entrada**.
2. Escreva a descrição, o valor e a data.

Essa entrada vale só para o mês da data que você escolher.

---

## Contas e parcelas

Aqui ficam todas as contas que você paga mês a mês, fora do cartão de crédito:

- as que não têm fim à vista: aluguel, internet, escola, plano de saúde;
- as que têm data para acabar: carnê da loja, boleto parcelado, empréstimo, acordo.

As duas coisas se cadastram do mesmo jeito. A diferença é só o campo **Termina em**.

### Cadastrar uma conta

1. Clique em **+ Nova conta**.
2. Escreva a descrição (ex.: `Aluguel`).
3. Informe o valor por mês.
4. Escolha a categoria (ex.: `Moradia`) — ela é usada no gráfico do resumo. Abaixo do campo
   ficam as categorias que você já usou: clique em uma delas ou escreva uma nova.
   Veja **Categorias**.
5. Informe o dia do vencimento.
6. Em **Começa em**, escolha o primeiro mês em que você paga.
7. **Termina em** é opcional: preencha só quando a conta tem data para acabar.

Você cadastra uma vez e a conta aparece sozinha em todos os meses do período.

### Cadastrar um parcelado

Um carnê é uma conta com data para acabar. Conte os meses e preencha o **Termina em**.

Exemplo: um sofá de R$ 1.200 em 6 vezes, começando em julho de 2026.

- Valor por mês: `200`
- Começa em: `07/2026`
- Termina em: `12/2026`

Pronto: a parcela de R$ 200 aparece de julho a dezembro e some sozinha em janeiro.

Se a parcela for de valor quebrado, use o valor que está no carnê. O app não divide nada
por você aqui — quem divide o total em parcelas é a tela **Compras no cartão**.

### Quando uma conta acaba

Se você cancelou algo que não tinha data para acabar (a academia, por exemplo), abra o
cadastro e preencha **Termina em** com o último mês que você pagou. A conta continua
aparecendo nos meses antigos e some dos meses seguintes.

### Quanto já paguei e quanto falta

No topo da tela ficam três números, sempre do mês escolhido lá em cima:

| Número | O que significa |
|---|---|
| **Total de julho / 2026** | A soma de todas as contas que caem naquele mês |
| **Já paguei** | A soma das que você marcou como pagas |
| **Falta pagar** | O que sobra: total menos o que já foi pago |

Embaixo de cada número aparece a contagem, para você saber quantas contas ainda estão sem
marcar: *2 de 5 já marcadas*, *3 ainda sem marcar*.

Exemplo: em julho você tem aluguel de R$ 1.500 e a parcela do sofá de R$ 200. Ao marcar
só o aluguel, a tela mostra Total R$ 1.700, Já paguei R$ 1.500 e Falta pagar R$ 200.

Quando você marca a última conta do mês, o app escreve **Tudo pago neste mês**.

Esses três números contam **apenas as contas desta tela**. O cartão tem o quadro dele no
resumo do mês.

### Lista de contas

A lista mostra todas as contas cadastradas, inclusive as que não valem no mês escolhido —
essas aparecem com *não vale neste mês* no lugar do valor, e sem o botão de pagamento.

Na coluna **Pagamento** você marca a conta como paga no mês escolhido. Veja
**Marcar o que já foi pago**.

---

## Cartões

Antes de lançar uma compra no cartão, cadastre o cartão.

1. Clique em **+ Novo cartão**.
2. Escreva o nome (ex.: `Nubank`).
3. Informe o dia do vencimento da fatura.
4. Escolha uma cor — o app mostra uma bolinha nessa cor ao lado do nome do cartão,
   para você reconhecer o cartão de longe nas listas.

A lista de cartões mostra, para o mês escolhido:

- **Vencimento**: o dia do mês em que a fatura desse cartão vence.
- **Falta pagar**: tudo que ainda vai ser cobrado desse cartão daqui para a frente.
- **Neste mês**: quanto esse cartão vai cobrar no mês que você está vendo.

Se parou de usar um cartão, desmarque **Ainda uso este cartão**. Ele continua na lista,
mas não aparece mais quando você lança uma compra nova.

Para dizer que já pagou a fatura de um mês, vá ao **Resumo do mês**, no quadro
**Faturas dos cartões**. Veja **Marcar o que já foi pago**.

### Gráfico de gastos por mês

Abaixo da lista de cartões fica um gráfico de linha que mostra quanto o cartão cobra
em cada mês. Ele serve para você ver se o próximo mês vem mais pesado ou mais leve
que o mês em que você está.

Como ler:

- Cada ponto é um mês. O ponto laranja, maior, é o mês que você está vendo agora.
- A linha sobe quando o mês tem mais parcelas somadas e desce quando tem menos.
- Passe o mouse em cima de um ponto para ver o valor exato daquele mês.
- O gráfico mostra 12 meses: os 5 meses antes do mês escolhido, o mês escolhido
  e os 6 meses seguintes.

Para ver só um cartão, use a caixa de seleção no canto do gráfico. Ela começa em
**Todos os cartões**, que soma tudo. Ao escolher um cartão, o gráfico passa a mostrar
apenas as compras daquele cartão.

O valor de cada mês é a soma das parcelas que caem nele. Exemplo: se você tem um
notebook de R$ 4.800 em 12x (R$ 400 por mês) e, a partir de agosto, um celular de
R$ 1.200 em 3x (R$ 400 por mês), o gráfico mostra R$ 400 em julho e R$ 800 em agosto,
setembro e outubro. Depois disso a linha volta para R$ 400, porque o celular acabou.

Quando você troca o mês na barra de cima do app, o gráfico anda junto.

---

## Compras no cartão

Aqui é onde você lança o que comprou no crédito.

Você lança a compra **uma vez** e o app espalha as parcelas pelos meses sozinho.

Dá para lançar de duas formas: uma compra por vez, no botão **+ Nova compra**, ou a
fatura inteira de uma vez, no botão **Importar fatura**. Veja **Importar a fatura do
cartão** mais abaixo.

1. Clique em **+ Nova compra**.
2. Escreva o que você comprou.
3. Escolha o cartão usado.
4. Escolha o mês da primeira parcela.
5. Informe em quantas vezes você parcelou.
6. Em **Você vai informar**, escolha se vai digitar o valor da parcela ou o valor total.
7. Digite o valor.
8. Escolha a categoria (ex.: `Eletrônicos`). Abaixo do campo ficam as categorias que você
   já usou: clique em uma delas ou escreva uma nova. Veja **Categorias**.

Enquanto você preenche, o app mostra uma prévia: `12x de R$ 400,00 · 07/2026 → 06/2027`.

### Valor da parcela ou valor total

Você escolhe o que é mais fácil de saber na hora de lançar a compra:

- **Valor da parcela** (é o que já vem escolhido): digite quanto a compra vai pesar por
  mês. O app multiplica pelo número de parcelas para saber o total. Digitar `400` em
  12 vezes dá uma compra de R$ 4.800.
- **Valor total**: digite quanto a compra custou por inteiro. O app divide pelo número
  de parcelas. Digitar `4.800` em 12 vezes dá parcelas de R$ 400.

Se você trocar de uma opção para a outra, o app converte o valor sozinho: nada é
perdido e a compra continua valendo o mesmo.

Com **valor da parcela** escolhido, mudar o número de parcelas muda o total da compra,
porque a parcela continua a mesma: 12x de R$ 400 são R$ 4.800; se você trocar para 10
vezes, viram R$ 4.000.

Compra à vista no cartão: use **1** no campo de parcelas. Com uma parcela só, o valor da
parcela e o valor total são a mesma coisa.

Quando você informa o valor total e ele não divide certinho, a última parcela fica com os
centavos que sobram. Por exemplo, R$ 100 em 3 vezes vira R$ 33,33 + R$ 33,33 + R$ 33,34.

Exemplo prático: um notebook de R$ 4.800 em 12 vezes, com a primeira parcela em julho
de 2026, vira 12 parcelas de R$ 400. Em julho aparece `parcela 1/12`, em agosto
`parcela 2/12`, e assim por diante até junho de 2027. Em julho de 2027 a compra
some sozinha, porque já acabou.

Você não precisa informar a data da compra: o que importa para o app é o mês em que cai
a primeira parcela.

### Duas formas de ver a lista

No canto do painel existem dois botões que trocam o que a lista mostra.

**Parcelas do mês** (é o que aparece ao abrir a tela)

Mostra **só as parcelas que caem no mês escolhido** lá no topo do app. É a visão do
dia a dia: o que esse mês vai cobrar de você. No título do painel aparece quantas
parcelas caem no mês e quanto elas somam.

Cada linha mostra:

- **Compra**: o que você comprou, com o parcelamento e a categoria embaixo.
- **Cartão**: em qual cartão a compra foi feita.
- **Parcela**: em que parcela você está, por exemplo `3 de 12`.
- **Período**: o mês da primeira e o da última parcela.
- **Neste mês**: quanto essa parcela pesa no mês escolhido.
- **Falta pagar**: quanto ainda falta dessa compra, contando deste mês para a frente.

**Todas as compras**

Mostra **tudo que você já lançou**, de todos os meses, inclusive o que já acabou e o
que ainda vai começar. É onde você procura uma compra antiga ou confere o que veio de
uma importação.

Cada linha mostra:

- **Compra**, **Cartão** e **Categoria**.
- **Parcelas**: quantas vezes e quanto é cada parcela.
- **Período**: o mês da primeira e o da última parcela.
- **Total**: quanto a compra soma no fim.
- **Falta pagar**: quanto ainda falta, contando do mês escolhido para a frente.

Nessa visão aparece também uma caixinha para filtrar por cartão. Escolha um cartão para
ver só as compras dele, ou deixe em **Todos os cartões**.

### Excluir a compra ou só uma parcela

Ao clicar em **Excluir** na visão **Parcelas do mês**, o app pergunta o que você quer
tirar:

- **Só a parcela deste mês**: essa parcela desaparece e as outras continuam nos meses
  delas. Serve para quando você adiantou uma parcela ou quando uma parcela veio errada
  de uma importação. O total da compra diminui só o valor dessa parcela.
- **A compra inteira**: a compra sai de todos os meses, do começo ao fim.

Exemplo: um notebook de R$ 4.800 em 12 parcelas de R$ 400. Se você excluir só a parcela
de setembro, ficam 11 parcelas de R$ 400 e o total cai para R$ 4.400. Setembro deixa de
cobrar essa compra e os outros meses não mudam.

Se você excluir a única parcela que ainda sobrava, a compra é apagada por inteiro.

Na visão **Todas as compras** não existe a opção de parcela, porque ali você não está
olhando um mês só: o **Excluir** tira a compra inteira. Quando uma compra tem parcela
excluída, aparece um aviso embaixo do nome dela, como `1 parcela excluída`.

### Importar a fatura do cartão

Em vez de digitar compra por compra, você pode trazer a fatura inteira de uma vez.

> Por enquanto a importação entende **somente o arquivo CSV da fatura do Nubank**.
> Fatura de outro banco ainda não funciona.

**1. Baixar o arquivo**

1. Abra a fatura no aplicativo do Nubank.
2. Toque em compartilhar e salve o arquivo que termina em `.csv`.
3. Guarde o arquivo em uma pasta que você ache fácil (a área de trabalho serve).

**2. Escolher o arquivo no app**

1. Vá em **Compras no cartão**.
2. Clique em **Importar fatura**.
3. Clique no campo do arquivo e escolha o `.csv` que você baixou.

Se o arquivo não for o CSV da fatura do Nubank, o app avisa e não importa nada.

**3. Conferir antes de salvar**

Depois de escolher o arquivo, o app mostra uma lista com o que encontrou. Nada é salvo
até você clicar no botão de importar.

Nessa tela você:

- escolhe o **cartão** que recebeu essa fatura;
- escolhe o **mês em que a fatura pesa**, que é o mês em que você paga ela. O app já
  tenta adivinhar pelo nome do arquivo (`Nubank_2026-08-09.csv` vira `08/2026`);
- **clica na linha** para marcar ou desmarcar a compra;
- muda o **número de parcelas** de qualquer linha;
- muda o **valor da parcela** de qualquer linha.

Todas as compras já vêm marcadas. Para deixar uma de fora, clique em qualquer lugar da
linha dela: a linha perde o destaque e a caixinha desmarca. Clique de novo para marcar
outra vez. Clicar dentro dos campos de **parcelas** e de **valor** não desmarca a linha,
então dá para corrigir o valor sem medo.

Os botões **Marcar todas** e **Desmarcar todas** ajudam quando você quer trazer só
algumas compras.

Cada linha mostra o total da compra e o mês da primeira parcela, e os dois se atualizam
na hora quando você mexe no valor ou no número de parcelas.

**4. Importar**

Clique em **Importar N compras**. As compras marcadas viram compras no cartão e você
volta para a lista, onde pode editar ou excluir qualquer uma delas.

### O que o app faz sozinho com a fatura

- **Pagamento e desconto ficam de fora.** Linhas com valor negativo (como
  `Pagamento recebido` e `Desconto Antecipação`) não são compras, então nem aparecem na lista.
- **Compra à vista vira 1 parcela.** Uma compra de R$ 19,90 sem parcelamento entra como
  1 parcela de R$ 19,90 no mês da fatura.
- **Compra parcelada já vem com o número certo de parcelas.** A linha
  `Loja do Bairro - Parcela 3/12` vira uma compra de 12 parcelas de R$ 76,30, ou seja,
  R$ 915,60 no total.
- **A primeira parcela volta no tempo.** Se a fatura pesa em `08/2026` e a linha diz
  `Parcela 3/12`, a primeira parcela foi em `06/2026`. O app já grava assim, então as
  parcelas que faltam caem nos meses certos daqui para a frente.
- **Parcela adiantada não vira compra repetida.** Quando você antecipa parcelas, a mesma
  compra aparece várias vezes na fatura (`Parcela 3/10`, `Parcela 4/10`, `Parcela 5/10`).
  O app junta tudo em uma linha só, usando a parcela mais antiga.
- **Compras iguais em dias diferentes continuam separadas.** Três almoços de R$ 50 no
  mesmo restaurante, em dias diferentes, viram três compras.

Uma coisa que o app **não** faz: conferir se você já importou aquela fatura antes. Se
importar o mesmo arquivo duas vezes, as compras aparecem em dobro. Nesse caso, use a visão
**Todas as compras** e apague as repetidas.

Depois de importar, vale olhar a visão **Todas as compras**: as parcelas que você já
adiantou continuam marcadas para os próximos meses. Use **Excluir → só a parcela deste
mês** nos meses em que a cobrança não vai mais existir.

As compras importadas entram **sem categoria**. Se quiser, edite depois cada compra e
escolha a categoria.

---

## Dinheiro guardado

Aqui você separa dinheiro para um objetivo: reserva de emergência, viagem, troca de carro.

### Criar um lugar para guardar

1. Clique em **+ Novo lugar para guardar**.
2. Dê um nome (ex.: `Emergência`).
3. Informe o **saldo inicial**: quanto você já tinha guardado ali antes de começar a usar
   o app. Se ainda não tem nada, deixe zero.
4. Escolha uma cor.

### Saldo inicial

O saldo inicial serve para você não precisar cadastrar tudo que guardou no passado.

Exemplo: você já tem R$ 2.000 na poupança. Crie a reserva `Emergência` com saldo inicial
de R$ 2.000. Se depois você guardar R$ 500 em julho, o saldo passa a mostrar R$ 2.500.

O saldo inicial **não conta como dinheiro guardado no mês**, porque ele já existia antes.
Ele aparece só no saldo total da reserva. Para mudar o valor depois, clique em **Editar**
na reserva.

### Guardar mais dinheiro

Você pode guardar dinheiro quantas vezes quiser, em qualquer mês:

1. Clique em **Guardar dinheiro** na reserva.
2. Informe o valor e a data.
3. Se quiser, escreva uma observação (ex.: `sobra do mês`).

### Tirar dinheiro

Clique em **Resgatar** e informe o valor. O saldo diminui.

### Como isso mexe no dinheiro livre

O que você guarda **não mexe no dinheiro livre do mês**. Guardar não é gastar: o dinheiro
continua sendo seu, só saiu da conta e foi para a reserva.

Exemplo: entrou R$ 6.000, os gastos foram R$ 2.100 e você guardou R$ 500.

- Livre no mês: R$ 3.900
- Guardado no mês: R$ 500

O **quanto dá para gastar por dia** também não muda quando você guarda dinheiro.

O saldo da reserva vai somando mês a mês. Se você guardou R$ 1.200 em maio e R$ 500 em
julho, ao olhar julho o saldo mostra R$ 1.700.

Clique em **Ver movimentos** para conferir tudo que entrou e saiu daquela reserva.

---

## Configurações

É a última tela do menu. Ali ficam os ajustes do aplicativo: a aparência, no quadro
**Exibição**, e a lista de **categorias**.

### Exibição

Serve para deixar o app do jeito mais confortável para você olhar: o tamanho da janela,
o tamanho das letras e qual tela abre primeiro.

Tudo que você escolhe aqui é salvo na hora, sem botão de salvar. Nada disso mexe nos seus
valores: só muda a aparência.

### Tamanho da janela

O app já vem lembrando o tamanho que você usou por último. Se você aumentar a janela com
o mouse e fechar o app, na próxima vez ele abre com esse mesmo tamanho.

Para escolher outro jeito:

1. Clique em **Configurações**, no menu.
2. No quadro **Exibição**, abra **Tamanho da janela**.
3. Escolha uma das opções:
   - **Lembrar o último tamanho que eu deixar** — abre do tamanho em que você fechou.
   - **Abrir em tela cheia** — abre ocupando a tela inteira.
   - Um tamanho da lista, como **1600 × 900** — abre sempre desse tamanho.

Ao escolher um tamanho da lista, a janela muda na hora: você vê o resultado sem precisar
fechar o app.

Embaixo do campo o app avisa qual foi o último tamanho que você deixou, por exemplo
*Da última vez: 1600 × 900*.

A janela nunca fica menor que 1040 × 700, para as tabelas não ficarem apertadas. Se o
tamanho escolhido for maior que a sua tela, o app usa o tamanho da tela.

### Tamanho das letras

Deixa tudo maior ou menor de uma vez: letras, botões e tabelas.

1. No quadro **Exibição**, abra **Tamanho das letras**.
2. Escolha **90%**, **100% (normal)**, **110%** ou **125%**.

A tela muda na hora.

### Tela que abre primeiro

O app abre no **Resumo do mês**. Se você usa mais uma outra tela, pode mandar o app abrir
direto nela.

1. No quadro **Exibição**, abra **Tela que abre primeiro**.
2. Escolha a tela, por exemplo **Compras no cartão**.

Vale a partir da próxima vez que você abrir o app.

### Categorias

Categoria é a etiqueta que você põe em uma conta ou em uma compra no cartão para saber
em que tipo de coisa o dinheiro foi: `Moradia`, `Transporte`, `Alimentação`.

Elas aparecem no quadro **Para onde vai o dinheiro**, no resumo do mês.

Você não precisa cadastrar nada antes de começar. Toda categoria que você escreve em uma
conta ou em uma compra fica salva sozinha. As que você já tinha usado antes também entram
na lista sozinhas.

Exemplo: você cadastra o aluguel e escreve `Moradia` na categoria. Da próxima vez que
abrir o cadastro de uma conta, `Moradia` aparece logo abaixo do campo **Categoria**: é só
clicar nela, sem digitar de novo. Clicando de novo na mesma, a conta fica sem categoria.

Se você escrever `moradia` e já existir `Moradia`, o app usa a que já existe. Assim a
mesma categoria não vira duas.

### Ver as categorias que você usa

1. Clique em **Configurações**, no menu.
2. A lista mostra todas as categorias em ordem alfabética.
3. Ao lado de cada uma, **Onde é usada** diz em quantos lançamentos ela está:
   *3 lançamentos*, *1 lançamento* ou *ainda não usada*.

### Criar uma categoria

Serve para deixar a lista pronta antes de cadastrar as contas.

1. Clique em **+ Nova categoria**.
2. Escreva o nome (ex.: `Lazer`).

Ela passa a aparecer na lista de escolha das contas e das compras, mesmo sem nenhum
lançamento usando ela ainda.

Não dá para ter duas categorias com o mesmo nome: o app avisa e não deixa salvar.

### Mudar o nome de uma categoria

1. Clique em **Editar** na categoria.
2. Escreva o nome novo.

Todos os lançamentos que usavam o nome antigo passam a mostrar o nome novo, sem você
precisar abrir um por um.

Exemplo: você tem o aluguel e a compra do sofá em `Moradia`. Ao trocar o nome para
`Casa`, os dois passam a aparecer como `Casa` no quadro **Para onde vai o dinheiro**.

### Excluir uma categoria

1. Clique em **Excluir** na categoria.
2. O aviso diz de quantos lançamentos ela vai sair.

Os lançamentos **não são apagados**: eles apenas ficam sem categoria. No quadro
**Para onde vai o dinheiro** eles passam a ser somados em **Sem categoria**.

Para colocá-los em outra categoria, abra cada lançamento e escolha a nova.

---

## Perguntas rápidas

**Onde ficam meus dados?**
Num arquivo no seu computador, em `%AppData%\PequenasFinancas\dados.json`.
O app guarda cópias de segurança na pasta `backups` a cada vez que salva.

**Preciso lançar as parcelas todo mês?**
Não. Você lança a compra uma vez e o app cuida do resto.

**Meu salário aumentou. E agora?**
Abra a renda e mude o valor. Se quiser guardar o histórico certinho, coloque uma data
de fim na renda antiga e cadastre uma nova começando no mês do aumento.

**Excluí sem querer. Dá para voltar?**
Pelo app, não. Mas existe uma cópia do arquivo anterior na pasta `backups`.

**O valor da parcela ficou com um centavo diferente. Por quê?**
Quando o total não divide certinho, a diferença de centavos vai para a última parcela,
para a soma das parcelas dar exatamente o valor da compra.

**Marcar uma conta como paga aumenta meu dinheiro livre?**
Não. O gasto já estava contado desde o cadastro. A marca só mostra o que você já resolveu
e desconta do número **Falta pagar**.

**Marquei o aluguel como pago e no mês seguinte ele apareceu como não pago. Está errado?**
Não, está certo. A marca vale para um mês só, porque no mês seguinte a conta vence de novo.

**Marquei sem querer. Como desfaço?**
Clique no mesmo botão outra vez. Ele volta para **Marcar pago**.

**Preciso cadastrar as categorias antes de usar o app?**
Não. Basta escrever a categoria na conta ou na compra: ela fica salva sozinha e aparece
pronta para escolher da próxima vez. A tela **Configurações** serve para arrumar essa
lista quando você quiser.

**Onde eu anoto o mercado, a farmácia, o lanche?**
O app não é um caderno de gastos do dia a dia. O que existe é o **planejamento da
semana**: lá você anota o que **acha** que vai gastar, para ver se cabe na semana.
No fim, você não lança gasto por gasto — só informa quanto sobrou.

**O valor por dia mudou de ontem para hoje. Por quê?**
No mês de hoje, o app divide o dinheiro livre pelos dias que ainda faltam. Cada dia que
passa, o mesmo dinheiro é dividido por menos dias, então o valor por dia sobe.

**Esqueci de fechar a semana no domingo. Perdi alguma coisa?**
Não. A semana fica aberta até você fechar. Feche quando lembrar, informando o saldo
daquele dia, e abra a próxima a partir dali.

**Gastei mais do que a semana permitia. E agora?**
Feche a semana com o valor que realmente sobrou. A próxima semana vai começar com menos
dinheiro e o app já mostra um valor por dia menor, sem você precisar refazer nada.
