using ClubeDaLiteratura.ModuloCaixa;

namespace ClubeDaLiteratura.ModuloRevista
{
    public class Revista
    {
        public int Id;
        public string Titulo;
        public int NumeroEdicao;
        public int AnoPublicacao;
        public string StatusEmprestimo;
        public Caixa CaixaOrigem;

        public Revista(int id, string titulo, int numeroEdicao, int anoPublicacao, Caixa caixaOrigem)
        {
            Id = id;
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicacao;
            CaixaOrigem = caixaOrigem;
            StatusEmprestimo = "Disponível";
        }

        public string Validar(RepositorioRevista repositorio)
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                return "O campo 'Título' é obrigatório.";

            if (NumeroEdicao <= 0)
                return "O campo 'Número da Edição' deve ser maior que zero.";

            if (AnoPublicacao < 1900 || AnoPublicacao > DateTime.Now.Year)
                return "Ano de publicação inválido.";

            if (CaixaOrigem == null)
                return "Você deve selecionar uma caixa.";

            foreach (var revista in repositorio.SelecionarTodos())
            {
                if (revista != null && revista.Titulo == this.Titulo && revista.NumeroEdicao == this.NumeroEdicao)
                    return "Já existe uma revista com este título e número de edição.";
            }

            return "";
        }

        public void Emprestar() => StatusEmprestimo = "Emprestada";

        public void Devolver() => StatusEmprestimo = "Disponível";

        public void Reservar() => StatusEmprestimo = "Reservada";

        public void RetirarReserva()
        {
            if (StatusEmprestimo != "Reservada")
                throw new InvalidOperationException("A revista não está reservada, não é possível retirar a reserva.");

            StatusEmprestimo = "Disponível";
        }
    }
}
