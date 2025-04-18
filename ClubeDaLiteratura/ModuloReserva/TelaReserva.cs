﻿using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.ModuloEmprestimo;
using ClubeDaLiteratura.ModuloRevista;
using ClubeDaLiteratura.Validadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura.ModuloReserva
{
    public class TelaReserva
    {
        private RepositorioReserva repositorio;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        private RepositorioMulta repositorioMulta;
        private RepositorioEmprestimo repositorioEmprestimo;

        public TelaReserva(RepositorioReserva r, RepositorioAmigo a, RepositorioRevista v, RepositorioMulta m, RepositorioEmprestimo e)
        {
            repositorio = r;
            repositorioAmigo = a;
            repositorioRevista = v;
            repositorioMulta = m;
            repositorioEmprestimo = e;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Módulo de Reservas ===");
                Console.WriteLine("1 - Criar Reserva");
                Console.WriteLine("2 - Cancelar Reserva");
                Console.WriteLine("3 - Visualizar Reservas Ativas");
                Console.WriteLine("4 - Converter Reservva em Empréstimo");
                Console.WriteLine("S - Voltar");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": Criar(); break;
                    case "2": Cancelar(); break;
                    case "3": Visualizar(); break;
                    case "4": ConverterReservaEmEmprestimo(); break;
                }
            } while (opcao.ToUpper() != "S");
        }

        private void Criar()
        {
            Console.Clear();
            Console.WriteLine("=== Nova Reserva ===");

            var amigos = repositorioAmigo.SelecionarTodos();
            var revistas = repositorioRevista.SelecionarTodos();

            if (!Validador.ExisteRevistaDisponivelParaReserva(repositorioRevista))
            {
                Notificador.ExibirMensagemErro("Não há revistas disponíveis para reserva.");
                return;
            }

            if (amigos.All(a => a == null))
            {
                Notificador.ExibirMensagemErro("Cadastre amigos primeiro.");
                return;
            }

            if (revistas.All(r => r == null || r.StatusEmprestimo != "Disponível"))
            {
                Notificador.ExibirMensagemErro("Não há revistas disponíveis.");
                return;
            }

            foreach (var a in amigos)
            {
                if (a != null)
                    Console.WriteLine($"{a.Id} - {a.Nome}");
            }

            int idAmigo = Validador.LerInteiro("ID do Amigo: ");
            var amigo = repositorioAmigo.SelecionarPorId(idAmigo);

            foreach (var r in revistas)
            {
                if (r != null && r.StatusEmprestimo == "Disponível")
                    Console.WriteLine($"{r.Id} - {r.Titulo} ({r.StatusEmprestimo})");
            }

            int idRevista = Validador.LerInteiro("ID da Revista: ");
            var revista = repositorioRevista.SelecionarPorId(idRevista);

            int id = GeradorId.GerarIdReserva();
            var nova = new Reserva(id, amigo, revista);

            string erro = nova.Validar(repositorioMulta);
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }
            revista.StatusEmprestimo = "Reservada";
            repositorio.Inserir(nova);
            Notificador.ExibirMensagemSucesso("Reserva registrada!");
        }

        private void Cancelar()
        {
            Visualizar();

            int id = Validador.LerInteiro("ID da reserva a cancelar: ");
            var reserva = repositorio.SelecionarPorId(id);

            if (reserva != null)
            {

                bool removida = repositorio.Excluir(id);

                if (removida)
                {
                    reserva.Revista.StatusEmprestimo = "Disponível";
                    Notificador.ExibirMensagemSucesso("Reserva cancelada e revista disponível para empréstimo.");
                }
                else
                {
                    Notificador.ExibirMensagemErro("Reserva não encontrada.");
                }
            }
            else
            {
                Notificador.ExibirMensagemErro("Reserva não encontrada.");
            }
        }

        private void Visualizar()
        {
            var reservas = repositorio.SelecionarAtivas();

            if (reservas.Length == 0)
            {
                Notificador.ExibirMensagemAviso("Nenhuma reserva ativa.");
                return;
            }

            foreach (var r in reservas)
            {
                if (Validador.ReservaExpirada(r))
                {
                    r.Status = "Expirada";
                    r.Revista.StatusEmprestimo = "Disponível";
                }
            }

            Console.WriteLine("\nReservas Ativas:");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-12} | {4,-12}", "ID", "Amigo", "Revista", "Início", "Fim");

            foreach (var r in reservas)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3:dd/MM/yyyy} | {4:dd/MM/yyyy}",
                    r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataReserva, r.DataFimReserva);
            }
            Console.ReadLine();
        }
        private void ConverterReservaEmEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Converter Reserva em Empréstimo ===");

            var reservas = repositorio.SelecionarAtivas();

            if (reservas.Length == 0)
            {
                Notificador.ExibirMensagemAviso("Nenhuma reserva ativa para converter.");
                return;
            }

            foreach (var r in reservas)
            {
                Console.WriteLine($"{r.Id} - {r.Revista.Titulo} (Reserva de {r.Amigo.Nome}) - Reservada em {r.DataReserva:dd/MM/yyyy}");
            }

            int id = Validador.LerInteiro("ID da reserva que deseja converter: ");
            var reserva = repositorio.SelecionarPorId(id);

            if (reserva == null || reserva.Status != "Ativa")
            {
                Notificador.ExibirMensagemErro("Reserva inválida ou já concluída.");
                return;
            }

            if (Validador.ReservaExpirada(reserva))
            {
                Notificador.ExibirMensagemErro("Essa reserva expirou.");
                return;
            }
            reserva.Revista.StatusEmprestimo = "Disponível";

            int idEmprestimo = GeradorId.GerarIdEmprestimo();
            var novoEmprestimo = new Emprestimo(idEmprestimo, reserva.Amigo, reserva.Revista, DateTime.Today);

            string erro = novoEmprestimo.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            reserva.Revista.Emprestar();        
            reserva.Concluir(); 
            repositorioEmprestimo.Inserir(novoEmprestimo);

            Notificador.ExibirMensagemSucesso("Reserva convertida em empréstimo com sucesso!");
        }
    }
}
