using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.ModuloRevista;
using ClubeDaLiteratura.Validadores;
using System;

namespace ClubeDaLiteratura.ModuloCaixa
{
    public class TelaCaixa
    {
        private RepositorioCaixa repositorio;
        private RepositorioRevista repositorioRevista;

        public TelaCaixa(RepositorioCaixa repositorio, RepositorioRevista repositorioRevista)
        {
            this.repositorio = repositorio;
            this.repositorioRevista = repositorioRevista;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Caixas =====");
                Console.WriteLine("1 - Inserir Caixa");
                Console.WriteLine("2 - Editar Caixa");
                Console.WriteLine("3 - Excluir Caixa");
                Console.WriteLine("4 - Visualizar Caixas");
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
            Console.Clear();
            Console.WriteLine("=== Cadastro de Caixa ===\n");

            Console.Write("Etiqueta: ");
            string etiqueta = Console.ReadLine()!;

            Console.Write("Cor: ");
            string cor = Console.ReadLine()!;

            Console.Write("Dias de Empréstimo: ");
            string entradaDias = Console.ReadLine()!;

            if (!int.TryParse(entradaDias, out int diasEmprestimo))
            {
                Notificador.ExibirMensagemErro("O campo 'Dias de Empréstimo' deve conter um número válido.");
                return;
            }

            int novoId = GeradorId.GerarIdCaixa();
            Caixa nova = new Caixa(novoId, etiqueta, cor, diasEmprestimo);

            string erro = nova.Validar();

            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            if (repositorio.Inserir(nova))
                Notificador.ExibirMensagemSucesso("Caixa cadastrada com sucesso!");
            else
                Notificador.ExibirMensagemErro("Erro ao cadastrar a caixa.");
        }

        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Edição de Caixa ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite o ID da caixa que deseja editar: ");
            int id = LerInteiro();

            Caixa atual = repositorio.SelecionarPorId(id);
            if (atual == null)
            {
                Notificador.ExibirMensagemErro("Caixa não encontrada.");
                return;
            }

            Console.Write($"Nova etiqueta (ENTER para manter: {atual.Etiqueta}): ");
            string novaEtiqueta = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(novaEtiqueta))
                novaEtiqueta = atual.Etiqueta;

            Console.Write($"Nova cor (ENTER para manter: {atual.Cor}): ");
            string novaCor = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(novaCor))
                novaCor = atual.Cor;

            Console.Write($"Novo prazo (ENTER para manter: {atual.DiasEmprestimo}): ");
            string entradaDias = Console.ReadLine();
            int novoPrazo;
            if (string.IsNullOrWhiteSpace(entradaDias))
                novoPrazo = atual.DiasEmprestimo;
            else
                novoPrazo = int.Parse(entradaDias);

            Caixa atualizada = new Caixa(id, novaEtiqueta, novaCor, novoPrazo);

            string erro = atualizada.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.Editar(id, atualizada);
            Notificador.ExibirMensagemSucesso("Caixa editada com sucesso!");
        }


        public void Excluir()
        {

            Console.Clear();
            Console.WriteLine("=== Exclusão de Caixa ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite o ID da caixa que deseja excluir: ");
            int id = LerInteiro();

            if (!Validador.CaixaPodeSerExcluida(id, repositorioRevista))
            {
                Notificador.ExibirMensagemErro("Não é possível excluir esta caixa pois ela contém revistas.");
                return;
            }

            if (repositorio.Excluir(id))
                Notificador.ExibirMensagemSucesso("Caixa excluída.");
            else
                Notificador.ExibirMensagemErro("Caixa não encontrada.");
        }

        public void VisualizarTodos(bool pausa = true)
        {
            var caixas = repositorio.SelecionarTodos();
            Console.WriteLine("ID | Etiqueta       | Cor        | Dias");
            Console.WriteLine("-----------------------------------------");

            foreach (var c in caixas)
            {
                if (c == null) continue;
                Console.WriteLine($"{c.Id,-3} | {c.Etiqueta,-13} | {c.Cor,-10} | {c.DiasEmprestimo}");
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
    }
}
