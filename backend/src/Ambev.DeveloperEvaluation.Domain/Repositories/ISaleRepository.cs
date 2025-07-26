using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Contrato para persist�ncia e consulta de vendas.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adiciona uma nova venda ao reposit�rio.
        /// </summary>
        /// <param name="sale">Entidade de venda a ser persistida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Task representando a opera��o ass�ncrona.</returns>
        Task <Sale> AddAsync(Sale sale, CancellationToken cancellationToken);
    }
}