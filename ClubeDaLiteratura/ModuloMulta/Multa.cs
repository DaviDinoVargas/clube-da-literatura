using ClubeDaLiteratura.ModuloAmigo;
using ClubeDaLiteratura.ModuloEmprestimo;
using System;

namespace ClubeDaLiteratura
{
    public class Multa
    {
        public int Id;
        public Amigo Amigo;
        public Emprestimo Emprestimo;
        public DateTime DataGeracao;
        public int DiasAtraso;
        public decimal Valor;
        public bool Quitada;

        public Multa(int id, Amigo amigo, Emprestimo emprestimo, int diasAtraso)
        {
            Id = id;
            Amigo = amigo;
            Emprestimo = emprestimo;
            DataGeracao = DateTime.Now;
            DiasAtraso = diasAtraso;
            Valor = diasAtraso * 2.00m;
            Quitada = false;
        }
    }
}
