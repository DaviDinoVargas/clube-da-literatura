using ClubeDaLiteratura.ModuloRevista;

namespace ClubeDaLiteratura.ModuloCaixa
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public int DiasEmprestimo;

        public Revista[] Revistas = new Revista[100];
        private int contadorRevistas = 0;

        public Caixa(int id, string etiqueta, string cor, int diasEmprestimo)
        {
            Id = id;
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
        }

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Etiqueta) || Etiqueta.Length < 2)
                erros += "O campo 'Etiqueta' é obrigatório e deve ter pelo menos 2 caracteres.\n";

            if (string.IsNullOrWhiteSpace(Cor))
                erros += "O campo 'Cor' é obrigatório.\n";

            if (DiasEmprestimo <= 0)
                erros += "O campo 'Dias de Empréstimo' deve ser maior que zero.\n";

            return erros;
        }

        public void AdicionarRevista(Revista revista)
        {
            if (contadorRevistas >= Revistas.Length)
                return;

            Revistas[contadorRevistas++] = revista;
        }

        public void RemoverRevista(Revista revista)
        {
            for (int i = 0; i < contadorRevistas; i++)
            {
                if (Revistas[i] == revista)
                {
                    for (int j = i; j < contadorRevistas - 1; j++)
                        Revistas[j] = Revistas[j + 1];

                    Revistas[contadorRevistas - 1] = null;
                    contadorRevistas--;
                    break;
                }
            }
        }
    }
}
