using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retrospective.Service.DataModels;
using Retrospective.Service.Repositories;
using Retrospective.Service.Services.Interfaces;

namespace Retrospective.Service.Services
{
    public class RetroService : IRetroService
    {
        private readonly IRetroRepository m_retroRepository;

        public RetroService(IRetroRepository retroRepository)
        {
            m_retroRepository = retroRepository;
        }
        public async Task<bool> TrySaveAsync(Retro retro)
        {
            if (!retro.IsValid)
            {
                return false;
            }

            {
                try
                {
                    await m_retroRepository.Create(retro);
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<ICollection<Retro>>  ReadAsync()
        {
            try
            {
                return await m_retroRepository.Read();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> TryDeleteAsync(Retro retro)
        {
            try
            {
                await m_retroRepository.Delete(retro);
                return true;
            }
            catch (Exception e)
            {
                if (e is ArgumentOutOfRangeException)
                {
                    return false;
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}