namespace ClubeDaLiteratura
{
    public class RepositorioCaixa
    {
        private Caixa[] caixas = new Caixa[100];
        private int contador = 0;

        public bool Inserir(Caixa novaCaixa)
        {
            caixas[contador++] = novaCaixa;
            return true;
        }

        public bool Editar(int id, Caixa atualizada)
        {
            for (int i = 0; i < contador; i++)
            {
                if (caixas[i] != null && caixas[i].Id == id)
                {
                    caixas[i] = atualizada;
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (caixas[i] != null && caixas[i].Id == id)
                {
                    caixas[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Caixa[] SelecionarTodos()
        {
            return caixas;
        }

        public Caixa SelecionarPorId(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (caixas[i] != null && caixas[i].Id == id)
                    return caixas[i];
            }
            return null;
        }
    }
}
