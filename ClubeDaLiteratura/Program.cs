using System;

namespace ClubeDaLeitura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("===== Clube da Leitura - Menu Principal =====");
                Console.WriteLine("1. Gerenciar Amigos");
                Console.WriteLine("2. Gerenciar Caixas");
                Console.WriteLine("3. Gerenciar Revistas");
                Console.WriteLine("4. Gerenciar Empréstimos");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine(">>> Módulo de Amigos");

                        break;
                    case "2":
                        Console.WriteLine(">>> Módulo de Caixas");

                        break;
                    case "3":
                        Console.WriteLine(">>> Módulo de Revistas");

                        break;
                    case "4":
                        Console.WriteLine(">>> Módulo de Empréstimos");

                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("❌ Opção inválida. Pressione Enter para tentar novamente.");
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("Sistema encerrado.");
        }
    }
}
