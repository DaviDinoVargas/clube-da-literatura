using System;

namespace ClubeDaLiteratura.ModuloAmigo
{
    public class TelaAmigo
    {
        private RepositorioAmigo repositorio;

        public TelaAmigo(RepositorioAmigo repositorio)
        {
            this.repositorio = repositorio;
        }

        public void SubMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("========= Módulo de Amigos =========");
                Console.WriteLine("1 - Cadastrar Amigo");
                Console.WriteLine("2 - Editar Amigo");
                Console.WriteLine("3 - Excluir Amigo");
                Console.WriteLine("4 - Visualizar Amigos");
                Console.WriteLine("S - Voltar ao menu principal");
                Console.WriteLine("====================================");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1": Inserir(); break;
                    case "2": Editar(); break;
                    case "3": Excluir(); break;
                    case "4": VisualizarTodos(); break;
                    case "s":
                    case "S": break;
                    default:
                        Notificador.ExibirMensagemErro("Opção inválida. Pressione Enter para continuar.");
                        break;
                }
            } while (opcao.ToUpper() != "S");
        }

        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Amigo ===\n");

            Console.Write("Nome: ");
            string nome = Console.ReadLine()!;
            Console.Write("Responsável: ");
            string responsavel = Console.ReadLine()!;
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine()!;

            int novoId = GeradorId.GerarIdAmigo();
            Amigo novo = new Amigo(novoId, nome, responsavel, telefone);

            string erro = novo.Validar();

            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            if (repositorio.AdicionarAmigo(novo))
                Notificador.ExibirMensagemSucesso("Amigo cadastrado com sucesso!");
            else
                Notificador.ExibirMensagemErro("Já existe um amigo com este nome e telefone.");
        }

        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Edição de Amigo ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite o ID do amigo que deseja editar: ");
            int id = LerInteiro();

            Amigo amigoAtual = repositorio.SelecionarPorId(id);

            if (amigoAtual == null)
            {
                Notificador.ExibirMensagemErro("Amigo não encontrado!");
                return;
            }

            Console.Write("Novo nome (ENTER para manter): ");
            string nome = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(nome))
                amigoAtual.Nome = nome;

            Console.Write("Novo responsável (ENTER para manter): ");
            string responsavel = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(responsavel))
                amigoAtual.Responsavel = responsavel;

            Console.Write("Novo telefone (ENTER para manter): ");
            string telefone = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(telefone))
                amigoAtual.Telefone = telefone;

            string erro = amigoAtual.Validar();
            if (erro != "")
            {
                Notificador.ExibirMensagemErro(erro);
                return;
            }

            repositorio.EditarAmigo(id, amigoAtual);
            Notificador.ExibirMensagemSucesso("Amigo editado com sucesso!");
        }

        public void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Exclusão de Amigo ===\n");
            VisualizarTodos(false);

            Console.Write("\nDigite o ID do amigo que deseja excluir: ");
            int id = LerInteiro();

            if (repositorio.ExcluirAmigo(id))
                Notificador.ExibirMensagemSucesso("Amigo excluído com sucesso!");
            else
                Notificador.ExibirMensagemErro("Amigo não encontrado!");
        }

        public void VisualizarTodos(bool pausa = true)
        {
            Console.WriteLine("\nLista de Amigos:");
            Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-15}", "ID", "Nome", "Responsável", "Telefone");
            Console.WriteLine("-------------------------------------------------------------");

            Amigo[] amigos = repositorio.SelecionarTodos();
            bool encontrou = false;

            foreach (Amigo amigo in amigos)
            {
                if (amigo == null) continue;

                Console.WriteLine("{0,-5} | {1,-20} | {2,-20} | {3,-15}",
                    amigo.Id, amigo.Nome, amigo.Responsavel, amigo.Telefone);

                encontrou = true;
            }

            if (!encontrou)
                Notificador.ExibirMensagemAviso("Nenhum amigo cadastrado.");

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
