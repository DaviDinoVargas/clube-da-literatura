using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura
{
    class GeradorId
    {
        private static int IdAmigos = 0;

        public static int GerarIdAmigo()
        {
            IdAmigos++;
            return IdAmigos;
        }
    }
}
