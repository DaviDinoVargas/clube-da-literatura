using ClubeDaLiteratura.Compartilhado;
using System;

namespace ClubeDaLiteratura.ModuloCaixa
{
    public class TelaCaixa
    {
        private RepositorioCaixa repositorio;

        public TelaCaixa(RepositorioCaixa repositorio)
        {
            this.repositorio = repositorio;
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
            Console.Write("Etiqueta: ");
            string etiqueta = Console.ReadLine();
            Console.Write("Cor: ");
            string cor = Console.ReadLine();
            Console.Write("Dias de Empréstimo: ");
            int dias = int.Parse(Console.ReadLine());

            int id = GeradorId.GerarIdCaixa();
            Caixa nova = new Caixa(id, etiqueta, cor, dias);

            string erro = nova.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.Inserir(nova);
            Notificador.ExibirMensagemSucesso("Caixa cadastrada!");
        }

        public void Editar()
        {
            VisualizarTodos(false);
            Console.Write("ID da caixa a editar: ");
            int id = int.Parse(Console.ReadLine());

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
            VisualizarTodos(false);
            Console.Write("ID da caixa a excluir: ");
            int id = int.Parse(Console.ReadLine());

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
    }
}
