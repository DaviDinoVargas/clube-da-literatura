# 📘 Sistema do Clube da Leitura

Bem-vindo ao projeto **Sistema do Clube da Leitura**, uma aplicação C# que digitaliza o controle de empréstimos de revistas em quadrinhos, otimizando a organização e reduzindo os erros e conflitos do processo manual. 🚀

> Desenvolvido como desafio prático para a **Academia do Programador 2025**

---
## 🖥️ Tecnologias
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)
![Console App](https://img.shields.io/badge/Console_App-000000?style=for-the-badge&logo=windows-terminal&logoColor=white)
- C# Console App (.NET)
- Estrutura orientada a objetos
- Arrays fixos como repositório em memória
- Separação modular (TELA, MODELO, REPOSITÓRIO)
---
## 🎯 Objetivo

Automatizar a gestão do clube de leitura de Gustavo:
- 📚 Controle de empréstimos de revistas
- 👦 Cadastro e rastreamento de amigos e responsáveis
- 📦 Organização das caixas temáticas
- ⏱️ Controle de prazos, atrasos, multas e reservas

---

## 🛠️ Módulos do Sistema

### 1. Amigos
- Cadastro, edição e exclusão de amigos
- Visualização de empréstimos por amigo
- Regras:
  - Nome, responsável e telefone obrigatórios
  - Telefone no formato válido: `(XX) XXXX-XXXX` ou `(XX) XXXXX-XXXX`
  - Nome + telefone não podem se repetir
  - Não pode excluir amigo com empréstimos vinculados

### 2. Caixas
- Cadastro e controle por etiqueta e cor
- Cada caixa define um prazo de empréstimo
- Regras:
  - Etiquetas únicas
  - Dias de empréstimo padrão: 7 (mínimo 1)
  - Não excluir caixas com revistas vinculadas

### 3. Revistas
- Cadastro por título, edição e ano
- Vinculação obrigatória com uma caixa
- Regras:
  - Título (2-100 caracteres) e edição únicos
  - Status: `Disponível`, `Emprestada`, `Reservada`
  - Cadastro inicia como `Disponível`

### 4. Empréstimos
- Registro de retirada e devolução
- Cálculo automático da data de devolução
- Multas por atraso (R$ 2,00 por dia)
- Regras:
  - Apenas uma revista por amigo por vez
  - Destacar empréstimos em atraso
  - Reserva válida por 2 dias
  - Controle visual e automático

### 5. Multas
- Registro e controle de multas por atraso na devolução de revistas
- Multas calculadas automaticamente: R$ 2,00 por dia de atraso
- Exibição de multas pendentes para cada amigo
- Regras:
  - Multas geradas com base na data de devolução
  - Possibilidade de visualizar multas por amigo
  - Multas não podem ser removidas manualmente, a não ser que o empréstimo seja registrado como devolvido
  - Controle de pagamento de multas, com atualização do status após o pagamento
  - Exibição de valor total de multas acumuladas

---

## 📦 Estrutura de Classes

| Módulo       | Classe               | Responsabilidade                            |
|--------------|----------------------|---------------------------------------------|
| Amigos       | `Amigo`              | Dados e validações do amigo                 |
|              | `RepositorioAmigo`   | Armazenamento e gestão                      |
|              | `TelaAmigo`          | Interface de interação                      |
| Caixas       | `Caixa`              | Dados da caixa e controle de revistas       |
|              | `RepositorioCaixa`   | Armazenamento e gestão                      |
|              | `TelaCaixa`          | Interface de interação                      |
| Revistas     | `Revista`            | Dados e status da revista                   |
|              | `RepositorioRevista` | Armazenamento e gestão                      |
|              | `TelaRevista`        | Interface de interação                      |
| Empréstimos  | `Emprestimo`         | Dados, validações e status do empréstimo    |
|              | `RepositorioEmprestimo` | Gestão dos registros de empréstimo     |
|              | `TelaEmprestimo`     | Interface de empréstimos e devoluções       |
| Multas  | `Multa`         | Dados, validações e valor estipulado por dias de atraso    |
|              | `RepositorioMulta` | Lógica e Gestão das Multas    |
|              | `TelaMulta`     | Interface de Multas para visualizações       |

---

## 🧪 Validações

- Formato de telefone
- Campos obrigatórios
- Tamanho de nome e títulos
- Regras de negócio específicas
- Status e datas de empréstimos automáticos

---
## 🛠 Como utilizar:
🚀 Passo a Passo

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto

```
dotnet restore
```
4. Em seguida, compile a solução o comando:
```
dotnet build --configuration Release
```
5. Para executar o projeto compilando em tempo real
```
dotnet run --project ClubeDaLiteratura
```
6. Para executar o arquivo compilado, navegue até a pasta: ./ClubeDaLiteratura.ConsoleApp/bin/Release/net8.0/ e execute o arquivo:
```
ClubeDaLiteratura.ConsoleApp.exe
```
