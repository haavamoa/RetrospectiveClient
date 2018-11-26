using System;
using FluentAssertions;
using Moq;
using Retrospective.Service.Repositories;
using Retrospective.Service.Services;
using Retrospective.Service.Tests.Utils;
using Xunit;
using Xunit.Sdk;

namespace Retrospective.Service.Tests.Services
{
    public class RetroServiceTests
    {
        public RetroServiceTests()
        {
            m_retroRepositoryMock = new Mock<IRetroRepository>();
            m_retroBuilder = new RetroBuilder();
            m_cut = new RetroService(m_retroRepositoryMock.Object);
        }

        private readonly RetroService m_cut;
        private readonly RetroBuilder m_retroBuilder;
        private readonly Mock<IRetroRepository> m_retroRepositoryMock;

        [Fact]
        public async void TrySaveAsync_InvalidRetro_RetroWasNotSaved()
        {
            var retro = m_retroBuilder.InvalidRetro().Build();

            var retroWasSaved = await m_cut.TrySaveAsync(retro);

            retroWasSaved.Should().BeFalse("Retro object was invalid");
        }

        [Fact]
        public async void TrySaveAsync_InvalidRetro_RepositoryCreateWasNotCalled()
        {
            var retro = m_retroBuilder.InvalidRetro().Build();

            await m_cut.TrySaveAsync(retro);

            m_retroRepositoryMock.Verify(r => r.Create(retro), Times.Never);
        }

        [Fact]
        public async void TrySaveAsync_ValidRetro_RetroWasSaved()
        {
            var retro = m_retroBuilder.ValidRetro().Build();

            var retroWasSaved = await m_cut.TrySaveAsync(retro);

            retroWasSaved.Should().BeTrue("Retro object was valid");
        }

        [Fact]
        public async void TrySaveAsync_ValidRetro_RepositoryCreateWasCalled()
        {
            var retro = m_retroBuilder.ValidRetro().Build();

            await m_cut.TrySaveAsync(retro);

            m_retroRepositoryMock.Verify(r => r.Create(retro), Times.Once);
        }

        [Fact]
        public async void TrySaveAsync_RepositoryShouldThrow_RepositoryShouldThrow()
        {
            var retro = m_retroBuilder.ValidRetro().Build();
            m_retroRepositoryMock.Setup(r => r.Create(retro)).Throws(new Exception("Something went wrong while trying to create in the repository"));

            try
            {
                await m_cut.TrySaveAsync(retro);
                Assert.False(true);
            }
            catch (Exception)
            {
                Assert.True(true);
            }

        }
    }
}