using ClubeDaLiteratura.ModuloAmigo;
using ClubeDaLiteratura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int Id;
        public Amigo Amigo;
        public Revista Revista;
        public DateTime DataEmprestimo;
        public string Situacao; // "Aberto" ou "Devolvido"

        public Emprestimo(int id, Amigo amigo, Revista revista, DateTime dataEmprestimo)
        {
            Id = id;
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = dataEmprestimo;
            Situacao = "Aberto";
        }

        public string Validar()
        {
            if (Amigo == null)
                return "Selecione um amigo válido.";

            if (Revista == null)
                return "Selecione uma revista válida.";

            if (Revista.StatusEmprestimo != "Disponível")
                return "A revista já está emprestada ou reservada.";

            return "";
        }

        public DateTime ObterDataDevolucao()
        {
            return DataEmprestimo.AddDays(Revista.CaixaOrigem.DiasEmprestimo);
        }

        public void RegistrarDevolucao()
        {
            Situacao = "Devolvido";
            Revista.Devolver();
        }
    }
}