using System.Linq;

namespace ClubeDaLiteratura
{
    public class RepositorioMulta
    {
        private Multa[] multas = new Multa[100];
        private int contador = 0;

        public void AdicionarMulta(Multa multa)
        {
            multas[contador++] = multa;
        }

        public Multa[] SelecionarTodas()
        {
            return multas;
        }

        public Multa[] SelecionarMultasPorAmigo(int idAmigo)
        {
            Multa[] resultado = new Multa[100];
            int pos = 0;

            foreach (var multa in multas)
            {
                if (multa != null && multa.Amigo.Id == idAmigo)
                    resultado[pos++] = multa;
            }

            return resultado;
        }

        public bool ExisteMultaParaEmprestimo(int idEmprestimo)
        {
            return multas.Any(multa => multa != null && multa.Emprestimo.Id == idEmprestimo);
        }

        public Multa SelecionarPorId(int id)
        {
            foreach (var multa in multas)
            {
                if (multa != null && multa.Id == id)
                    return multa;
            }
            return null;
        }
    }
}
