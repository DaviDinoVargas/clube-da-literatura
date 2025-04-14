using ClubeDaLiteratura.ModuloCaixa;
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
    }
}