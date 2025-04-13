using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string Responsavel;
        public string Telefone;

        public Amigo(int id, string nome, string responsavel, string telefone)
        {
            Id = id;
            Nome = nome;
            Responsavel = responsavel;
            Telefone = telefone;
        }

        public string Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3)
                return "O campo 'Nome' é obrigatório e precisa conter ao menos 3 caracteres.";

            if (string.IsNullOrWhiteSpace(Responsavel))
                return "O campo 'Responsável' é obrigatório.";

            if (string.IsNullOrWhiteSpace(Telefone))
                return "O campo 'Telefone' é obrigatório.";

            return ""; // válido
        }

        public void ObterEmprestimos()
        {
            
        }
    }
}
