using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLiteratura.ModuloEmprestimo
{
    public class RepositorioEmprestimo
    {
        private Emprestimo[] emprestimos = new Emprestimo[100];
        private int contador = 0;

        public bool Inserir(Emprestimo novoEmprestimo)
        {
            emprestimos[contador++] = novoEmprestimo;
            return true;
        }

        public bool Editar(int id, Emprestimo atualizado)
        {
            for (int i = 0; i < contador; i++)
            {
                if (emprestimos[i] != null && emprestimos[i].Id == id)
                {
                    emprestimos[i] = atualizado;
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (emprestimos[i] != null && emprestimos[i].Id == id)
                {
                    emprestimos[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Emprestimo[] SelecionarTodos()
        {
            return emprestimos;
        }

        public Emprestimo SelecionarPorId(int id)
        {
            for (int i = 0; i < contador; i++)
            {
                if (emprestimos[i] != null && emprestimos[i].Id == id)
                    return emprestimos[i];
            }
            return null;
        }

        public Emprestimo[] SelecionarEmprestimosPorAmigo(int amigoId)
        {
            Emprestimo[] resultado = new Emprestimo[100];
            int pos = 0;

            foreach (var e in emprestimos)
            {
                if (e != null && e.Amigo.Id == amigoId)
                {
                    resultado[pos++] = e;
                }
            }

            return resultado;
        }
    }
}
