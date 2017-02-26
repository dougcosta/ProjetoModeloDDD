using System.Collections.Generic;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public IEnumerable<Produto> GetByNome(string nome)
        {
            return context.Produtos.Where(p => p.Nome == nome);
        }
    }
}
