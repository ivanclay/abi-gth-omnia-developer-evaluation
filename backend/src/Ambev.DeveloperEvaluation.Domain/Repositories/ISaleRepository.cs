using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Contrato para persistência e consulta de vendas.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adiciona uma nova venda ao repositório.
        /// </summary>
        /// <param name="sale">Entidade de venda a ser persistida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Task representando a operação assíncrona.</returns>
        Task <Sale> AddAsync(Sale sale, CancellationToken cancellationToken);
    }
}