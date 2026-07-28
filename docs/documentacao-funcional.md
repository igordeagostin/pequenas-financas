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

## Primeiros passos

Para o app ficar útil, faça nesta ordem:

1. Cadastre sua **renda** (o salário, por exemplo).
2. Cadastre seus **gastos fixos** (aluguel, internet…).
3. Cadastre seus **cartões**.
4. Lance suas **compras no cartão**.
5. Se tiver carnê ou boleto parcelado, cadastre em **parcelados sem cartão**.
6. Crie um lugar para o seu **dinheiro guardado**.

Depois disso, a tela **Resumo do mês** mostra tudo junto.

Se quiser controlar o dinheiro semana a semana, use a tela
**Planejamento da semana** depois desses passos.

---

## Resumo do mês

É a primeira tela do app. Mostra o retrato do mês escolhido.

No topo aparecem quatro números:

| Número | O que significa |
|---|---|
| **Entra** | Todo o dinheiro que você recebe no mês |
| **Sai** | Gastos fixos + cartões + parcelas |
| **Livre no mês** | Quanto sobra depois de pagar tudo |
| **Guardado até aqui** | Quanto você tem separado em todas as reservas |

A conta do dinheiro livre é assim:

```
Livre no mês = tudo que entra − gastos fixos − cartões − parcelas
```

O dinheiro que você guardou **não entra nessa conta**. Guardar não é gastar: o dinheiro
continua sendo seu, só mudou de lugar. Quando os gastos passam do que entrou, o valor
fica vermelho.

Mais abaixo a tela mostra:

- **Quanto dá para gastar por dia**: explicado na próxima seção.
- **Tudo que entra e sai no mês**: uma lista com cada entrada e cada saída.
  Verde é dinheiro que entra, vermelho é dinheiro que sai.
- **Faturas dos cartões**: quanto cada cartão vai cobrar neste mês.
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
  que aparece no resumo (o que entra, menos gastos fixos, cartões e parcelas).
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

## Gastos fixos

São as contas que se repetem todo mês: aluguel, internet, escola, plano de saúde.

Para cadastrar:

1. Clique em **+ Novo gasto fixo**.
2. Escreva a descrição (ex.: `Aluguel`).
3. Informe o valor por mês.
4. Escreva a categoria (ex.: `Moradia`) — ela é usada no gráfico do resumo.
5. Informe o dia do vencimento.
6. Escolha o mês em que a conta começa e, se souber, o mês em que ela acaba.

Assim como a renda, você cadastra uma vez e a conta aparece em todos os meses.

Quando uma conta acaba (por exemplo, você cancelou a academia), abra o cadastro
e preencha o campo **Termina em** com o último mês que você pagou. O gasto continua
aparecendo nos meses antigos e some dos meses seguintes.

---

## Cartões

Antes de lançar uma compra no cartão, cadastre o cartão.

1. Clique em **+ Novo cartão**.
2. Escreva o nome (ex.: `Nubank`) e a bandeira.
3. Informe o limite.
4. Informe o dia em que a fatura fecha e o dia do vencimento.
5. Escolha uma cor — o app mostra uma bolinha nessa cor ao lado do nome do cartão,
   para você reconhecer o cartão de longe nas listas.

A lista de cartões mostra, para o mês escolhido:

- **Falta pagar**: tudo que ainda vai ser cobrado desse cartão daqui para a frente.
- **Neste mês**: quanto esse cartão vai cobrar no mês que você está vendo.

Se parou de usar um cartão, desmarque **Ainda uso este cartão**. Ele continua na lista,
mas não aparece mais quando você lança uma compra nova.

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

1. Clique em **+ Nova compra**.
2. Escreva o que você comprou.
3. Escolha o cartão usado.
4. Informe o valor total da compra (não o valor da parcela).
5. Informe a data da compra.
6. Informe em quantas vezes você parcelou.
7. Escolha o mês da primeira parcela.

Enquanto você preenche, o app mostra uma prévia: `12x de R$ 400,00 · 07/2026 → 06/2027`.

Exemplo prático: um notebook de R$ 4.800 em 12 vezes, com a primeira parcela em julho
de 2026, vira 12 parcelas de R$ 400. Em julho aparece `parcela 1/12`, em agosto
`parcela 2/12`, e assim por diante até junho de 2027. Em julho de 2027 a compra
some sozinha, porque já acabou.

Compra à vista no cartão: use **1** no campo de parcelas.

Quando o valor não divide certinho, a última parcela fica com os centavos que sobram.
Por exemplo, R$ 100 em 3 vezes vira R$ 33,33 + R$ 33,33 + R$ 33,34.

A lista mostra:

- **Parcelas**: quanto é cada parcela.
- **Período**: o mês da primeira e o da última parcela.
- **Neste mês**: em que parcela você está no mês que está vendo.
- **Falta pagar**: quanto ainda falta dessa compra.

Marque a caixa **mostrar só o que cai em…** para ver apenas as compras que pesam no
mês escolhido.

---

## Parcelados sem cartão

Funciona igual às compras no cartão, mas para o que você paga **fora do crédito**:
carnê de loja, boleto parcelado, empréstimo, acordo com alguém.

1. Clique em **+ Novo parcelado**.
2. Escreva o que você parcelou e para quem você paga.
3. Informe o valor total, em quantas vezes e o mês da primeira parcela.

No resumo do mês essas parcelas aparecem separadas das faturas dos cartões, para você
saber o que é cartão e o que não é.

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
