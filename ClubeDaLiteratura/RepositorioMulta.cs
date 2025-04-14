﻿namespace ClubeDaLiteratura
{
    public class RepositorioMulta
    {
        private Multa[] multas = new Multa[100];
        private int contador = 0;

        public void AdicionarMulta(Multa multa)
        {
            multas[contador++] = multa;
        }

        public Multa[] SelecionarMultasPorAmigo(int idAmigo)
        {
            Multa[] resultado = new Multa[100];
            int pos = 0;

            foreach (var m in multas)
            {
                if (m != null && m.Amigo.Id == idAmigo)
                    resultado[pos++] = m;
            }

            return resultado;
        }
    }
}
