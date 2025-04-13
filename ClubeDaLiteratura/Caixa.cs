namespace ClubeDaLiteratura
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public int DiasEmprestimo;

        public Caixa(int id, string etiqueta, string cor, int diasEmprestimo)
        {
            Id = id;
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
        }

        public string Validar()
        {
            if (string.IsNullOrWhiteSpace(Etiqueta))
                return "O campo 'Etiqueta' é obrigatório.";

            if (string.IsNullOrWhiteSpace(Cor))
                return "O campo 'Cor' é obrigatório.";

            if (DiasEmprestimo <= 0)
                return "O campo 'Dias de Empréstimo' deve ser maior que zero.";

            return "";
        }

        public void AdicionarRevista() { }
        public void RemoverRevista() { }
    }
}
