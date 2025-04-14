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

            foreach (var m in multas)
            {
                if (m != null && m.Amigo.Id == idAmigo)
                    resultado[pos++] = m;
            }

            return resultado;
        }

        public bool ExisteMultaParaEmprestimo(int idEmprestimo)
        {
            return multas.Any(m => m != null && m.Emprestimo.Id == idEmprestimo);
        }

        public Multa SelecionarPorId(int id)
        {
            foreach (var m in multas)
            {
                if (m != null && m.Id == id)
                    return m;
            }
            return null;
        }
    }
}
