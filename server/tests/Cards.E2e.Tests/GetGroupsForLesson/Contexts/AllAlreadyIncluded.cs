using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetGroupsForLesson.Contexts
{
    public class AllAlreadyIncluded : GetGroupsForLessonContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override IEnumerable<GroupToLessonDto> ExpectedResponse => Enumerable.Empty<GroupToLessonDto>();

        public AllAlreadyIncluded()
        {
            var owner = DataBuilder.SampleUser().Build();
            var group = DataBuilder.SampleGroup()
                .With(x => x.Cards = new List<Card>
                {
                    DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                    {
                        DataBuilder.FrontDetails().With(d => d.IsQuestion = true)
                            .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build(),
                        DataBuilder.BackDetails().With(d => d.IsQuestion = true)
                            .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build()
                    }).Build()
                }).Build();
            owner.Groups.Add(group);

            group = DataBuilder.SampleGroup()
                .With(x => x.Cards = new List<Card>
                {
                    DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                    {
                        DataBuilder.FrontDetails().With(d => d.IsQuestion = true)
                            .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build(),
                        DataBuilder.BackDetails().With(d => d.IsQuestion = true)
                            .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build()
                    }).Build()
                }).Build();
            owner.Groups.Add(group);

            GivenOwners = new[] { owner };
        }
    }
}