using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.ModuloCaixa;
using System;

namespace ClubeDaLiteratura.ModuloRevista
{
    public class TelaRevista
    {
        private RepositorioRevista repositorio;
        private RepositorioCaixa repositorioCaixa;

        public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa)
        {
            this.repositorio = repositorio;
            this.repositorioCaixa = repositorioCaixa;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Revistas =====");
                Console.WriteLine("1 - Inserir Revista");
                Console.WriteLine("2 - Editar Revista");
                Console.WriteLine("3 - Excluir Revista");
                Console.WriteLine("4 - Visualizar Revistas");
                Console.WriteLine("S - Voltar");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1": Inserir(); break;
                    case "2": Editar(); break;
                    case "3": Excluir(); break;
                    case "4": VisualizarTodos(); break;
                }
            } while (opcao.ToUpper() != "S");
        }

        public void Inserir()
        {
            Console.WriteLine("== Cadastro de Revista ==");

            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Número da Edição: ");
            int edicao = int.Parse(Console.ReadLine());
            Console.Write("Ano de Publicação: ");
            int ano = int.Parse(Console.ReadLine());

            VisualizarCaixas(false);
            Console.Write("ID da Caixa: ");
            int idCaixa = int.Parse(Console.ReadLine());
            Caixa caixa = repositorioCaixa.SelecionarPorId(idCaixa);

            int id = GeradorId.GerarIdRevista();
            Revista revista = new Revista(id, titulo, edicao, ano, caixa);

            string erro = revista.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.Inserir(revista);
            Notificador.ExibirMensagemSucesso("Revista cadastrada com sucesso!");
        }

        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Edição de Revista ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite a ID da Revista que deseja editar: ");
            int id = LerInteiro();

            Revista atual = repositorio.SelecionarPorId(id);
            if (atual == null)
            {
                Notificador.ExibirMensagemErro("Revista não encontrada.");
                return;
            }

            Console.Write($"Novo título (ENTER para manter: {atual.Titulo}): ");
            string titulo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titulo)) titulo = atual.Titulo;

            Console.Write($"Nova edição (ENTER para manter: {atual.NumeroEdicao}): ");
            string edStr = Console.ReadLine();
            int edicao = string.IsNullOrWhiteSpace(edStr) ? atual.NumeroEdicao : int.Parse(edStr);

            Console.Write($"Novo ano (ENTER para manter: {atual.AnoPublicacao}): ");
            string anoStr = Console.ReadLine();
            int ano = string.IsNullOrWhiteSpace(anoStr) ? atual.AnoPublicacao : int.Parse(anoStr);

            VisualizarCaixas(false);
            Console.Write($"Nova Caixa ID (ENTER para manter: {atual.CaixaOrigem.Id}): ");
            string idCaixaStr = Console.ReadLine();
            Caixa caixa = string.IsNullOrWhiteSpace(idCaixaStr)
                ? atual.CaixaOrigem
                : repositorioCaixa.SelecionarPorId(int.Parse(idCaixaStr));

            Revista atualizada = new Revista(id, titulo, edicao, ano, caixa)
            {
                StatusEmprestimo = atual.StatusEmprestimo
            };

            string erro = atualizada.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.Editar(id, atualizada);
            Notificador.ExibirMensagemSucesso("Revista atualizada com sucesso!");
        }

        public void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Exclusão de Revistas ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite o ID da revista que deseja excluir: ");
            int id = LerInteiro();

            if (repositorio.Excluir(id))
                Notificador.ExibirMensagemSucesso("Revista excluída.");
            else
                Notificador.ExibirMensagemErro("Revista não encontrada.");
        }

        public void VisualizarTodos(bool pausa = true)
        {
            var revistas = repositorio.SelecionarTodos();
            Console.WriteLine("ID | Título                | Edição | Ano  | Caixa           | Status");
            Console.WriteLine("--------------------------------------------------------------------------");

            foreach (var r in revistas)
            {
                if (r == null) continue;

                Console.WriteLine($"{r.Id,-2} | {r.Titulo,-20} | {r.NumeroEdicao,-6} | {r.AnoPublicacao,-4} | {r.CaixaOrigem?.Etiqueta,-15} | {r.StatusEmprestimo}");
            }

            if (pausa) Console.ReadLine();
        }
        private int LerInteiro()
        {
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Notificador.ExibirMensagemErro("Digite um número válido!");
                Console.Write("Tente novamente: ");
            }
            return valor;
        }
        public void VisualizarCaixas(bool pausa = true)
        {
            var caixas = repositorioCaixa.SelecionarTodos();
            Console.WriteLine("\nID | Etiqueta       | Cor        | Dias");
            Console.WriteLine("-----------------------------------------");

            foreach (var c in caixas)
            {
                if (c == null) continue;
                Console.WriteLine($"{c.Id,-3} | {c.Etiqueta,-13} | {c.Cor,-10} | {c.DiasEmprestimo}");
            }

            if (pausa) Console.ReadLine();
        }
        
    }
}
