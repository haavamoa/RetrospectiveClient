using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Retrospective.Service.DataModels;
using Retrospective.Service.Repositories;
using Retrospective.Service.Tests.Utils;
using Xunit;

namespace Retrospective.Service.Tests.Repositories
{
    public class JsonRepositoryTests
    {
        public JsonRepositoryTests()
        {
            m_cut = new JsonRepository(configuration => { configuration.JsonFilesPrefix = "retrospective"; });
            m_retroBuilder = new RetroBuilder();
        }

        private readonly JsonRepository m_cut;
        private readonly RetroBuilder m_retroBuilder;

        private static async Task<Retro> DeserializeFromFile(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var json = lines.Aggregate(string.Empty, (current, line) => current + line);
            var actualRetro = JsonConvert.DeserializeObject<Retro>(json);
            return actualRetro;
        }

        [Fact]
        public async void CreateReadDelete_CreatedJson_ShouldHaveSameId()
        {
            try
            {
                var expectedRetro = m_retroBuilder.ValidRetro().Build();

                await m_cut.Create(expectedRetro);

                var retros = await m_cut.Read();
                if (retros.Count == 1)
                {
                    var readRetro = retros.First();
                    readRetro.Id.Should().Be(expectedRetro.Id);
                }

                await m_cut.Delete(expectedRetro);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}