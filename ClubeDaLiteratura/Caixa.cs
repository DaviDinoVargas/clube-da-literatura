using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Validar()
        {
            
        }

        public void AdicionarRevista()
        {
            
        }

        public void RemoverRevista()
        {
            
        }
    }
}