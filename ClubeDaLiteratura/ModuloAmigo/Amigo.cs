using ClubeDaLiteratura.ModuloEmprestimo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClubeDaLiteratura.ModuloAmigo
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string Responsavel;
        public string Telefone;

        public Emprestimo[] ObterEmprestimos(RepositorioEmprestimo repositorioEmprestimo)
        {
            return repositorioEmprestimo.SelecionarEmprestimosPorAmigo(this.Id);
        }
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

 
            if (string.IsNullOrWhiteSpace(Responsavel) || Responsavel.Length < 3)
                return "O campo 'Responsável' é obrigatório e precisa conter ao menos 3 caracteres.";

            if (string.IsNullOrWhiteSpace(Telefone))
                return "O campo 'Telefone' é obrigatório.";

            var regexTelefone = new Regex(@"^\(\d{2}\) \d{4,5}-\d{4}$");
            if (!regexTelefone.IsMatch(Telefone))
                return "O campo 'Telefone' deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.";

            return "";
        }
    }
}
