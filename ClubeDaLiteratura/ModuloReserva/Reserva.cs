using ClubeDaLiteratura.ModuloAmigo;
using ClubeDaLiteratura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura.ModuloReserva
{
    public class Reserva
    {
        public int Id;
        public Amigo Amigo;
        public Revista Revista;
        public DateTime DataReserva;
        public DateTime DataFimReserva;
        public string Status;

        public Reserva(int id, Amigo amigo, Revista revista)
        {
            Id = id;
            Amigo = amigo;
            Revista = revista;
            DataReserva = DateTime.Today;
            DataFimReserva = DataReserva.AddDays(2);
            Status = "Ativa";
        }

        public string Validar(RepositorioMulta repositorioMulta)
        {
            if (Amigo == null)
                return "É necessário selecionar um amigo.";

            if (Revista == null)
                return "É necessário selecionar uma revista.";

            if (Revista.StatusEmprestimo != "Disponível")
                return "A revista deve estar disponível para ser reservada.";

            if (Amigo.TemMultaPendente(repositorioMulta))
                return "Amigo com multa pendente não pode reservar.";

            return "";
        }

        public void Concluir()
        {
            Status = "Concluída";
        }
    }
}