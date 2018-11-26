using System.Collections.Generic;
using System.Threading.Tasks;
using Retrospective.Service.DataModels;

namespace Retrospective.Service.Services.Interfaces
{
    public interface IRetroService
    {
        /// <summary>
        /// Tries to save a retrospective to the repository
        /// </summary>
        /// <param name="retro">The retrospective to try to save</param>
        /// <returns>A boolean to mark if it was saved or not</returns>
        Task<bool> TrySaveAsync(Retro retro);
        /// <summary>
        ///  Read all retrospectives from the repository
        /// </summary>
        /// <returns>A collection of all the retrospectives in the repository</returns>
        Task<ICollection<Retro>> ReadAsync();
        /// <summary>
        /// Tries to delete a specific retrospective
        /// </summary>
        /// <param name="retro"></param>
        /// <returns>A boolean to mark if it was deleted or not</returns>
        Task<bool> TryDeleteAsync(Retro retro);
    }
}