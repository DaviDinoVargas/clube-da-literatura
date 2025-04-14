using ClubeDaLiteratura.Compartilhado;
using ClubeDaLiteratura.ModuloCaixa;
using ClubeDaLiteratura.ModuloEmprestimo;
using ClubeDaLiteratura.ModuloRevista;

namespace ClubeDaLiteratura.Validadores
{
    public static class Validador
    {
        public static bool CaixaPodeSerExcluida(int idCaixa, RepositorioRevista repositorioRevista)
        {
            Revista[] revistas = repositorioRevista.SelecionarTodos();

            foreach (var revista in revistas)
            {
                if (revista != null && revista.CaixaOrigem != null && revista.CaixaOrigem.Id == idCaixa)
                {
                    return false; 
                }
            }

            return true; 
        }
        public static bool ExistemCaixasCadastradas(RepositorioCaixa repositorioCaixa)
        {
            Caixa[] caixas = repositorioCaixa.SelecionarTodos();

            foreach (var caixa in caixas)
            {
                if (caixa != null)
                    return true;
            }

            return false;
        }
        public static int LerInteiro(string mensagem = "Digite um número: ")
        {
            int valor;

            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Notificador.ExibirMensagemErro("Digite um número válido!");
                Console.Write("Tente novamente: ");
            }

            return valor;
        }
        public static bool ExistemAmigosCadastrados(RepositorioAmigo repositorioAmigo)
        {
            var amigos = repositorioAmigo.SelecionarTodos();

            foreach (var amigo in amigos)
            {
                if (amigo != null)
                    return true;
            }

            return false;
        }
        public static bool ExistemRevistasCadastradas(RepositorioRevista repositorioRevista)
        {
            Revista[] revistas = repositorioRevista.SelecionarTodos();

            foreach (var revista in revistas)
            {
                if (revista != null)
                    return true;
            }

            return false;
        }
        public static bool AmigoTemEmprestimoEmAberto(int idAmigo, RepositorioEmprestimo repositorioEmprestimo)
        {
            var emprestimos = repositorioEmprestimo.SelecionarTodos();

            foreach (var emprestimo in emprestimos)
            {
                if (emprestimo != null && emprestimo.Amigo.Id == idAmigo && emprestimo.Situacao != "Devolvido")
                {
                    return true;
                }
            }

            return false;
        }
        public static bool AmigoPodeSerExcluido(int idAmigo, RepositorioEmprestimo repositorioEmprestimo)
        {
            var emprestimos = repositorioEmprestimo.SelecionarTodos();

            foreach (var emprestimo in emprestimos)
            {
                if (emprestimo != null && emprestimo.Amigo.Id == idAmigo && emprestimo.Situacao != "Devolvido")
                {
                    return false; 
                }
            }

            return true; 
        }
        public static bool ExisteRevistaDisponivelParaReserva(RepositorioRevista repositorioRevista)
        {
            var revistas = repositorioRevista.SelecionarTodos();

            foreach (var revista in revistas)
            {
                if (revista != null && revista.StatusEmprestimo == "Disponível")
                    return true;
            }

            return false;
        }
    }

}