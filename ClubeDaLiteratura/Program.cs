﻿using ClubeDaLiteratura.ModuloAmigo;
using ClubeDaLiteratura.ModuloCaixa;
using ClubeDaLiteratura.ModuloEmprestimo;
using ClubeDaLiteratura.ModuloMulta;
using ClubeDaLiteratura.ModuloRevista;
using System;

namespace ClubeDaLeitura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioRevista repositorioRevista = new RepositorioRevista();
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
            RepositorioMulta repositorioMulta = new RepositorioMulta();


            TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
            TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);
            TelaMulta telaMulta = new TelaMulta(repositorioMulta, repositorioAmigo);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("===== Clube da Leitura - Menu Principal =====");
                Console.WriteLine("1. Gerenciar Amigos");
                Console.WriteLine("2. Gerenciar Caixas");
                Console.WriteLine("3. Gerenciar Revistas");
                Console.WriteLine("4. Gerenciar Empréstimos");
                Console.WriteLine("5. Gerenciar Multas");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        telaAmigo.SubMenu();
                        break;
                    case "2":
                        telaCaixa.SubMenu();
                        break;
                    case "3":
                        telaRevista.SubMenu();
                        break;
                    case "4":
                        telaEmprestimo.SubMenu();
                        break;
                    case "5":
                        telaMulta.SubMenu();
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
