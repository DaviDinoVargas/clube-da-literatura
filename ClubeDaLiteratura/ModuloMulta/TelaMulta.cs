using ClubeDaLiteratura.Compartilhado;
using System;
using System.Linq;

namespace ClubeDaLiteratura.ModuloMulta
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
                Console.WriteLine("S - Voltar");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine();

                switch (opcao.ToUpper())
                {
                    case "1": VisualizarTodas(); break;
                    case "2": VisualizarPorAmigo(); break;
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
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-6} | {5,-6}",
                "ID", "Amigo", "Revista", "Data", "Dias", "Valor");

            foreach (var multa in multas)
            {
                if (multa == null) continue;

                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-6} | R$ {5:N2}",
                    multa.Id,
                    multa.Amigo.Nome,
                    multa.Emprestimo.Revista.Titulo,
                    multa.DataGeracao.ToShortDateString(),
                    multa.DiasAtraso,
                    multa.Valor);
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
            int id = int.Parse(Console.ReadLine());

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
            Console.WriteLine("{0,-5} | {1,-20} | {2,-12} | {3,-6} | {4,-6}",
                "ID", "Revista", "Data", "Dias", "Valor");

            foreach (var multa in multas)
            {
                if (multa == null) continue;

                Console.WriteLine("{0,-5} | {1,-20} | {2,-12} | {3,-6} | R$ {4:N2}",
                    multa.Id,
                    multa.Emprestimo.Revista.Titulo,
                    multa.DataGeracao.ToShortDateString(),
                    multa.DiasAtraso,
                    multa.Valor);
            }

            Console.ReadLine();
        }
    }
}
