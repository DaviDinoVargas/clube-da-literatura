using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.Validadores;
using System;
using System.Linq;

namespace ClubeDaLiteratura
{
    public class TelaMulta
    {
        private RepositorioMulta repositorioMulta;
        private RepositorioAmigo repositorioAmigo;

        public TelaMulta(RepositorioMulta repoMulta, RepositorioAmigo repoAmigo)
        {
            repositorioMulta = repoMulta;
            repositorioAmigo = repoAmigo;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Multas =====");
                Console.WriteLine("1 - Visualizar todas as multas");
                Console.WriteLine("2 - Visualizar multas por amigo");
                Console.WriteLine("3 - Quitar multa");
                Console.WriteLine("S - Voltar");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine();

                switch (opcao.ToUpper())
                {
                    case "1": VisualizarTodas(); break;
                    case "2": VisualizarPorAmigo(); break;
                    case "3": QuitarMulta(); break;
                    case "S": break;
                    default:
                        Notificador.ExibirMensagemErro("Opção inválida.");
                        break;
                }
            } while (opcao.ToUpper() != "S");
        }

        public void VisualizarTodas()
        {
            var multas = repositorioMulta.SelecionarTodas();

            if (multas.Length == 0 || multas.All(m => m == null))
            {
                Notificador.ExibirMensagemAviso("Nenhuma multa registrada.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nTodas as Multas Registradas:");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-6} | {5,-6} | {6}",
                "ID", "Amigo", "Revista", "Data", "Dias", "Valor", "Status");

            foreach (var multa in multas)
            {
                if (multa == null) continue;

                string status = multa.Quitada ? "Paga" : "Pendente";

                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-6} | R$ {5:N2} | {6}",
                    multa.Id,
                    multa.Amigo.Nome,
                    multa.Emprestimo.Revista.Titulo,
                    multa.DataGeracao.ToShortDateString(),
                    multa.DiasAtraso,
                    multa.Valor,
                    status);
            }

            Console.ReadLine();
        }

        public void VisualizarPorAmigo()
        {
            Console.Clear();
            Console.WriteLine("=== Multas por Amigo ===");
            var amigos = repositorioAmigo.SelecionarTodos();

            foreach (var amigo in amigos)
            {
                if (amigo != null)
                    Console.WriteLine($"{amigo.Id} - {amigo.Nome}");
            }

            Console.Write("\nDigite o ID do amigo: ");
            int id = Validador.LerInteiro(Console.ReadLine()!);

            var amigoSelecionado = repositorioAmigo.SelecionarPorId(id);
            if (amigoSelecionado == null)
            {
                Notificador.ExibirMensagemErro("Amigo não encontrado.");
                return;
            }

            var multas = repositorioMulta.SelecionarMultasPorAmigo(id);

            if (multas.Length == 0 || multas.All(m => m == null))
            {
                Notificador.ExibirMensagemAviso("Esse amigo não possui multas.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nMultas de {amigoSelecionado.Nome}:");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-12} | {3,-6} | {4,-6} | {5}",
                "ID", "Revista", "Data", "Dias", "Valor", "Status");

            foreach (var multa in multas)
            {
                if (multa == null) continue;

                string status = multa.Quitada ? "Paga" : "Pendente";

                Console.WriteLine("{0,-5} | {1,-20} | {2,-12} | {3,-6} | R$ {4:N2} | {5}",
                    multa.Id,
                    multa.Emprestimo.Revista.Titulo,
                    multa.DataGeracao.ToShortDateString(),
                    multa.DiasAtraso,
                    multa.Valor,
                    status);
            }

            Console.ReadLine();
        }

        public void QuitarMulta()
        {
            var multas = repositorioMulta.SelecionarTodas();

            if (multas.Length == 0 || multas.All(m => m == null || m.Quitada))
            {
                Notificador.ExibirMensagemAviso("Nenhuma multa pendente para quitar.");
                Console.ReadLine();
                return;
            }

            VisualizarTodas();

            Console.Write("\nDigite o ID da multa a quitar: ");
            int id = Validador.LerInteiro(Console.ReadLine());

            Multa multa = repositorioMulta.SelecionarPorId(id);

            if (multa == null)
            {
                Notificador.ExibirMensagemErro("Multa não encontrada.");
                return;
            }

            if (multa.Quitada)
            {
                Notificador.ExibirMensagemAviso("Essa multa já está quitada.");
                return;
            }

            multa.Quitada = true;

            Notificador.ExibirMensagemSucesso("Multa quitada com sucesso!");
        }
    }
}
