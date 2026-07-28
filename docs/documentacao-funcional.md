# Como usar o Pequenas Finanças

Este guia explica, em palavras simples, o que cada tela do aplicativo faz.

O objetivo do app é um só: **saber quanto sobra do seu dinheiro no fim do mês.**

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

---

## Resumo do mês

É a primeira tela do app. Mostra o retrato do mês escolhido.

No topo aparecem quatro números:

| Número | O que significa |
|---|---|
| **Entrou** | Todo o dinheiro que você recebeu no mês |
| **Saiu** | Gastos fixos + cartões + parcelas + gastos soltos |
| **Sobra do mês** | Quanto sobrou de verdade, já tirando o que você guardou |
| **Guardado até aqui** | Quanto você tem separado em todas as reservas |

A conta da sobra é assim:

```
Sobra = tudo que entrou − gastos fixos − cartões − parcelas − gastos soltos − o que você guardou
```

Se você guardou dinheiro no mês, o app mostra também quanto sobraria **antes** de guardar.
Quando os gastos passam do que entrou, o valor fica vermelho.

Mais abaixo a tela mostra:

- **Tudo que aconteceu no mês**: uma lista com cada entrada e cada saída.
  Verde é dinheiro que entra, vermelho é dinheiro que sai.
- **Faturas dos cartões**: quanto cada cartão vai cobrar neste mês.
- **Para onde foi o dinheiro**: seus gastos somados por categoria.
- **Dinheiro guardado**: o saldo de cada reserva.

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
5. Escolha uma cor — ela é usada nas etiquetas do cartão pelo app.

A lista de cartões mostra, para o mês escolhido:

- **Falta pagar**: tudo que ainda vai ser cobrado desse cartão daqui para a frente.
- **Neste mês**: quanto esse cartão vai cobrar no mês que você está vendo.

Se parou de usar um cartão, desmarque **Ainda uso este cartão**. Ele continua na lista,
mas não aparece mais quando você lança uma compra nova.

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

## Gastos do mês

Para aquele gasto solto, pago na hora, que não se repete e não passou no cartão:
uma farmácia, um conserto, um presente.

1. Clique em **+ Novo gasto**.
2. Escreva no que você gastou, o valor, a data e a categoria.

O gasto vale só para o mês da data escolhida.

---

## Dinheiro guardado

Aqui você separa dinheiro para um objetivo: reserva de emergência, viagem, troca de carro.

### Criar um lugar para guardar

1. Clique em **+ Novo lugar para guardar**.
2. Dê um nome (ex.: `Emergência`).
3. Se quiser, informe quanto pretende juntar. O app mostra uma barra de progresso.
4. Escolha uma cor.

### Guardar mais dinheiro

Você pode guardar dinheiro quantas vezes quiser, em qualquer mês:

1. Clique em **Guardar dinheiro** na reserva.
2. Informe o valor e a data.
3. Se quiser, escreva uma observação (ex.: `sobra do mês`).

### Tirar dinheiro

Clique em **Resgatar** e informe o valor. O saldo diminui.

### Como isso mexe na sobra do mês

O que você guarda no mês **é descontado da sobra**, porque esse dinheiro saiu da sua conta
corrente e foi para a reserva.

Exemplo: entrou R$ 6.000, os gastos foram R$ 2.100 e você guardou R$ 500.

- Sobra antes de guardar: R$ 3.900
- Sobra do mês: R$ 3.400

O resumo mostra os dois valores, para você não se perder.

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
