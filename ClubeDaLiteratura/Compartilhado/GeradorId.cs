using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClubeDaLiteratura.Compartilhado
{
    class GeradorId
    {
        private static int IdAmigos = 0;
        private static int IdCaixas = 0;
        private static int IdRevistas = 0;
        private static int IdEmprestimo = 0;
        private static int IdReserva = 1;

        public static int GerarIdAmigo()
        {
            IdAmigos++;
            return IdAmigos;
        }
        public static int GerarIdCaixa()
        {
            IdCaixas++;
            return IdCaixas;
        }
        public static int GerarIdRevista()
        {
            return ++IdRevistas;
        }
        public static int GerarIdEmprestimo()
        {
            return ++IdEmprestimo;
        }
        public static int GerarIdReserva()
        {
            return IdReserva++;
        }
    }
}
