namespace ClubeDaLiteratura.ModuloRevista
{
    public class RepositorioRevista
    {
        private Revista[] revistas = new Revista[100];
        private int contador = 0;

        public bool Inserir(Revista novaRevista)
        {
            revistas[contador++] = novaRevista;
            return true;
        }

        public bool Editar(int id, Revista atualizada)
        {
            for (int i = 0; i < contador; i++)
            {
                if (revistas[i] != null && revistas[i].Id == id)
                {
                    revistas[i] = atualizada;
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (revistas[i] != null && revistas[i].Id == id)
                {
                    revistas[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Revista[] SelecionarTodos()
        {
            return revistas;
        }

        public Revista SelecionarPorId(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (revistas[i] != null && revistas[i].Id == id)
                    return revistas[i];
            }
            return null;
        }
    }
}
