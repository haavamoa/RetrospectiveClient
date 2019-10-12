using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Retrospective.Service.DataModels;

namespace Retrospective.Service.Repositories
{
    public class JsonRepository : IRetroRepository
    {
        private Dictionary<Guid, string> m_retroJsonpathDictionary;

        public JsonRepository(Action<JsonRepositoryConfiguration> configurationFunc = null)
        {
            Configuration = new JsonRepositoryConfiguration();
            configurationFunc?.Invoke(Configuration);
            m_retroJsonpathDictionary = new Dictionary<Guid, string>();
        }

        public JsonRepositoryConfiguration Configuration { get; }

        public async Task Create(Retro retro)
        {
            EnsureJsonPathExists();
            var json = JsonConvert.SerializeObject(retro);
            var filepathWithFileName =
                $"{Configuration.PathForJsonFiles}/{Configuration.JsonFilesPrefix}-{retro.EndTime.ToString(Configuration.JsonDateFormat)}.json";

            if (File.Exists(filepathWithFileName))
            {
                File.Delete(filepathWithFileName);
            }

            using (FileStream fs = File.Create(filepathWithFileName))
            {
                var encoded = Encoding.ASCII.GetBytes(json);
                await fs.WriteAsync(encoded, 0, encoded.Length);
                m_retroJsonpathDictionary.Add(retro.Id, filepathWithFileName);
            }
        }

        private void EnsureJsonPathExists()
        {
            if (!Directory.Exists(Configuration.PathForJsonFiles))
            {
                Directory.CreateDirectory(Configuration.PathForJsonFiles);
            }
        }

        public async Task<ICollection<Retro>> Read()
        {
            var retros = new Collection<Retro>();
            var files = Directory.EnumerateFiles($"{Configuration.PathForJsonFiles}", $"{Configuration.JsonFilesPrefix}*.json");
            foreach (var file in files)
            {
                using (FileStream fs = File.Open(file, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        var fileContent = await sr.ReadToEndAsync();
                        var retro = JsonConvert.DeserializeObject<Retro>(fileContent);
                        if(retro.IsValid)
                        { 
                            retros.Add(retro);
                        }
                    }
                }
            }
            return retros;
        }

        public Task Delete(Retro retro)
        {
            var gotValue = m_retroJsonpathDictionary.TryGetValue(retro.Id, out var jsonPath);
            if (gotValue)
            {
                File.Delete(jsonPath);
                return Task.CompletedTask;
            }

            throw new ArgumentOutOfRangeException(
                                                  $"Could not find and retro with id: {retro.Id} in any json file in : {Configuration.PathForJsonFiles}");
        }
    }
}