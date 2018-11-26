using System.Collections.Generic;
using System.Threading.Tasks;
using Retrospective.Service.DataModels;

namespace Retrospective.Service.Repositories
{
    public interface IRetroRepository
    {
        Task Create(Retro retro);
        Task<ICollection<Retro>> Read();
        //Task Update(Retro retro);
        Task Delete(Retro retro);
    }
}