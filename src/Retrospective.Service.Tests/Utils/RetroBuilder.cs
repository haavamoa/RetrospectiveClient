using System;
using System.Collections.Generic;
using Retrospective.Service.DataModels;
using Action = Retrospective.Service.DataModels.Action;

namespace Retrospective.Service.Tests.Utils
{
    internal class RetroBuilder
    {
        private Retro m_retro;

        public Retro Build()
        {
            if (m_retro == null)
            {
                throw new Exception("Use one of the retrobuilder methods before using Build() to build a retro");
            }

            var retro = m_retro;
            m_retro = null;
            return retro;
        }

        public RetroBuilder InvalidRetro()
        {
            m_retro = ValidRetro().Build();
            m_retro = new Retro(Guid.Empty)
            {
                Writer = null,
                StartTime = default(DateTime),
                EndTime = default(DateTime),
                Positives = m_retro.Positives,
                Negatives = m_retro.Negatives,
                Actions = m_retro.Actions
            };
            return this;
        }

        public RetroBuilder ValidRetro()
        {
            m_retro = new Retro(Guid.NewGuid())
            {
                Positives = CreateValidPositives(),
                Negatives = CreateValidNegatives(),
                Actions = CreateValidActions(),
                Writer = CreateValidWriter(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            return this;
        }

        private static List<Action> CreateValidActions()
        {
            return new List<Action>
            {
                new Action { Text = "My first action retrospective point" },
                new Action { Text = "My second action retrospective point" }
            };
        }

        private static List<Negative> CreateValidNegatives()
        {
            return new List<Negative>
            {
                new Negative { Text = "My first negative retrospective point" },
                new Negative { Text = "My second negative retrospective point" }
            };
        }

        private static List<Positive> CreateValidPositives()
        {
            return new List<Positive>
            {
                new Positive { Text = "My first positive retrospective point" },
                new Positive { Text = "My second positive retrospective point" }
            };
        }

        private static Writer CreateValidWriter()
        {
            var writer = new Writer { Name = "Mr. Test" };

            return writer;
        }
    }
}