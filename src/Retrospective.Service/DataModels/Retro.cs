using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Retrospective.Service.Utils;

namespace Retrospective.Service.DataModels
{
    [JsonObject(MemberSerialization.Fields)]
    public class Retro
    {
        public Retro(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; internal set; }

        public List<Positive> Positives { get; set; }
        public List<Negative> Negatives { get; set; }
        public List<Action> Actions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Writer Writer { get; set; }

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (Positives != null)
                {
                    if (Positives.Any(p => !p.IsValid))
                    {
                        return false;
                    }
                }
                if (Negatives != null)
                {
                    if (Negatives.Any(p => !p.IsValid))
                    {
                        return false;
                    }
                }
                if (Actions != null)
                {
                    if (Actions.Any(p => !p.IsValid))
                    {
                        return false;
                    }
                }

                return Id != Guid.Empty || Writer != null || (StartTime != default(DateTime) || EndTime != default(DateTime));
            }
        }
    }
}