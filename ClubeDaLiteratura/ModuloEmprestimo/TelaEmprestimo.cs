using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.ModuloAmigo;
using ClubeDaLiteratura.ModuloRevista;
using System;

namespace ClubeDaLiteratura.ModuloEmprestimo
{
    public class TelaEmprestimo
    {
        private RepositorioEmprestimo repositorio;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;

        public TelaEmprestimo(RepositorioEmprestimo repositorio, RepositorioAmigo repoAmigo, RepositorioRevista repoRevista)
        {
            this.repositorio = repositorio;
            repositorioAmigo = repoAmigo;
            repositorioRevista = repoRevista;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Empréstimos =====");
                Console.WriteLine("1 - Registrar Empréstimo");
                Console.WriteLine("2 - Editar Empréstimo");
                Console.WriteLine("3 - Registrar Devolução");
                Console.WriteLine("4 - Visualizar Todos");
                Console.WriteLine("S - Voltar");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1": Inserir(); break;
                    case "2": Editar(); break;
                    case "3": RegistrarDevolucao(); break;
                    case "4": VisualizarTodos(); break;
                }
            } while (opcao.ToUpper() != "S");
        }

        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Novo Empréstimo ===");

            VisualizarAmigos();
            Console.Write("ID do Amigo: ");
            int idAmigo = int.Parse(Console.ReadLine());
            Amigo amigo = repositorioAmigo.SelecionarPorId(idAmigo);

            if (amigo == null)
            {
                Notificador.ExibirMensagemErro("Amigo não encontrado.");
                return;
            }

            VisualizarRevistas();
            Console.Write("ID da Revista: ");
            int idRevista = int.Parse(Console.ReadLine());
            Revista revista = repositorioRevista.SelecionarPorId(idRevista);

            if (revista == null)
            {
                Notificador.ExibirMensagemErro("Revista não encontrada.");
                return;
            }

            int idEmprestimo = GeradorId.GerarIdEmprestimo();
            Emprestimo novo = new Emprestimo(idEmprestimo, amigo, revista, DateTime.Today);

            string erro = novo.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            revista.Emprestar();
            repositorio.Inserir(novo);
            Notificador.ExibirMensagemSucesso("Empréstimo registrado com sucesso!");
        }

        public void Editar()
        {
            VisualizarTodos(false);
            Console.Write("Digite o ID do empréstimo que deseja editar: ");
            int id = int.Parse(Console.ReadLine());

            Emprestimo emprestimoAtual = repositorio.SelecionarPorId(id);

            if (emprestimoAtual == null)
            {
                Notificador.ExibirMensagemErro("Empréstimo não encontrado.");
                return;
            }

            Console.WriteLine($"\nAmigo atual: {emprestimoAtual.Amigo.Nome}");
            VisualizarAmigos();
            Console.Write("Novo ID de amigo (ENTER para manter): ");
            string entradaAmigo = Console.ReadLine();
            Amigo novoAmigo = emprestimoAtual.Amigo;
            if (!string.IsNullOrWhiteSpace(entradaAmigo))
            {
                int idAmigo = int.Parse(entradaAmigo);
                Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

                if (amigoSelecionado == null)
                {
                    Notificador.ExibirMensagemErro("Amigo não encontrado.");
                    return;
                }

                novoAmigo = amigoSelecionado;
            }

            Console.WriteLine($"\nRevista atual: {emprestimoAtual.Revista.Titulo}");
            VisualizarRevistas();
            Console.Write("Novo ID de revista (ENTER para manter): ");
            string entradaRevista = Console.ReadLine();
            Revista novaRevista = emprestimoAtual.Revista;
            if (!string.IsNullOrWhiteSpace(entradaRevista))
            {
                int idRevista = int.Parse(entradaRevista);
                Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

                if (revistaSelecionada == null || revistaSelecionada.StatusEmprestimo != "Disponível")
                {
                    Notificador.ExibirMensagemErro("Revista inválida ou indisponível.");
                    return;
                }

                emprestimoAtual.Revista.Devolver();
                novaRevista = revistaSelecionada;
                novaRevista.Emprestar();
            }

            Console.WriteLine($"\nData atual de empréstimo: {emprestimoAtual.DataEmprestimo.ToShortDateString()}");
            Console.Write("Nova data (ENTER para manter): ");
            string entradaData = Console.ReadLine();
            DateTime novaData = emprestimoAtual.DataEmprestimo;
            if (!string.IsNullOrWhiteSpace(entradaData))
            {
                if (!DateTime.TryParse(entradaData, out novaData))
                {
                    Notificador.ExibirMensagemErro("Data inválida.");
                    return;
                }
            }

            Emprestimo atualizado = new Emprestimo(id, novoAmigo, novaRevista, novaData)
            {
                Situacao = emprestimoAtual.Situacao
            };

            string erro = atualizado.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.Editar(id, atualizado);
            Notificador.ExibirMensagemSucesso("Empréstimo atualizado com sucesso!");
        }

        public void RegistrarDevolucao()
        {
            VisualizarTodos(false);
            Console.Write("Digite o ID do empréstimo: ");
            int id = int.Parse(Console.ReadLine());

            Emprestimo emp = repositorio.SelecionarPorId(id);

            if (emp == null || emp.Situacao == "Devolvido")
            {
                Notificador.ExibirMensagemErro("Empréstimo inválido ou já devolvido.");
                return;
            }

            emp.RegistrarDevolucao();
            Notificador.ExibirMensagemSucesso("Devolução registrada!");
        }

        public void VisualizarTodos(bool pausa = true)
        {
            var lista = repositorio.SelecionarTodos();

            Console.WriteLine("ID | Amigo             | Revista                 | Data      | Devolução | Situação");
            Console.WriteLine("----------------------------------------------------------------------------------");

            foreach (var e in lista)
            {
                if (e == null) continue;

                string dt = e.DataEmprestimo.ToShortDateString();
                string dtDev = e.ObterDataDevolucao().ToShortDateString();
                Console.WriteLine($"{e.Id,-3} | {e.Amigo.Nome,-17} | {e.Revista.Titulo,-23} | {dt,-10} | {dtDev,-10} | {e.Situacao}");
            }

            if (pausa) Console.ReadLine();
        }

        public void VisualizarAmigos()
        {
            Amigo[] amigos = repositorioAmigo.SelecionarTodos();
            Console.WriteLine("\nAmigos:");
            foreach (var a in amigos)
            {
                if (a != null)
                    Console.WriteLine($"{a.Id} - {a.Nome}");
            }
        }

        public void VisualizarRevistas()
        {
            Revista[] revistas = repositorioRevista.SelecionarTodos();
            Console.WriteLine("\nRevistas:");
            foreach (var r in revistas)
            {
                if (r != null)
                    Console.WriteLine($"{r.Id} - {r.Titulo} [{r.StatusEmprestimo}]");
            }
        }
    }
}
