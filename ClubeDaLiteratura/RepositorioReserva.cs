using ClubeDaLiteratura.Validadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura
{
    public class RepositorioReserva
    {
        private Reserva[] reservas = new Reserva[100];
        private int contador = 0;

        public void Inserir(Reserva reserva)
        {
            reservas[contador++] = reserva;
        }

        public bool Excluir(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (reservas[i] != null && reservas[i].Id == id)
                {
                    reservas[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Reserva[] SelecionarTodas()
        {
            return reservas;
        }

        public Reserva SelecionarPorId(int id)
        {
            return reservas.FirstOrDefault(r => r != null && r.Id == id);
        }

        public Reserva SelecionarAtivaPorRevista(int idRevista)
        {
            return reservas.FirstOrDefault(r => r != null && r.Revista.Id == idRevista && r.Status == "Ativa");
        }

        public Reserva[] SelecionarAtivas()
        {
            return reservas.Where(r => r != null && r.Status == "Ativa").ToArray();
        }

        public void RemoverExpiradas()
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] != null && reservas[i].Status == "Ativa" &&
                    Validador.ReservaExpirada(reservas[i]))
                {
                    reservas[i].Revista.StatusEmprestimo = "Disponível";
                    reservas[i] = null;
                }
            }
        }
    }
}
