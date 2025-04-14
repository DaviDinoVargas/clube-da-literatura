using ClubeDaLiteratura.ModuloAmigo;

public class RepositorioAmigo
{
    private Amigo[] amigos = new Amigo[100];
    private int contador = 0;

    public bool AdicionarAmigo(Amigo novo)
    {
        string nomeNovo = novo.Nome.Trim().ToLower();
        string telefoneNovo = novo.Telefone.Trim();

        foreach (var amigo in amigos)
        {
            if (amigo == null) continue;

            string nomeExistente = amigo.Nome.Trim().ToLower();
            string telefoneExistente = amigo.Telefone.Trim();

            if (nomeExistente == nomeNovo && telefoneExistente == telefoneNovo)
                return false;
        }

        amigos[contador++] = novo;
        return true;
    }

    public bool EditarAmigo(int id, Amigo amigoAtualizado)
    {
        for (int i = 0; i < contador; i++)
        {
            if (amigos[i] != null && amigos[i].Id == id)
            {
                amigos[i] = amigoAtualizado;
                return true;
            }
        }
        return false;
    }

    public bool ExcluirAmigo(int id)
    {
        for (int i = 0; i < contador; i++)
        {
            if (amigos[i] != null && amigos[i].Id == id)
            {
                amigos[i] = null;
                return true;
            }
        }
        return false;
    }

    public Amigo[] SelecionarTodos()
    {
        return amigos;
    }

    public Amigo SelecionarPorId(int id)
    {
        for (int i = 0; i < contador; i++)
        {
            if (amigos[i] != null && amigos[i].Id == id)
                return amigos[i];
        }
        return null;
    }
}
