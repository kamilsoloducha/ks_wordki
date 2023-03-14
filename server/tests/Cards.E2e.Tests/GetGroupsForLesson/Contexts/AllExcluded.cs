using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetGroupsForLesson.Contexts
{
    public class AllExcluded : GetGroupsForLessonContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override IEnumerable<GroupToLessonDto> ExpectedResponse { get; }

        public AllExcluded()
        {
            var owner = DataBuilder.SampleUser().Build();
            var group = DataBuilder.SampleGroup()
                .With(x => x.Cards = new List<Card>
                {
                    DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                    {
                        DataBuilder.FrontDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build(),
                        DataBuilder.BackDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build()
                    }).Build(),
                    DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                    {
                        DataBuilder.FrontDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build(),
                        DataBuilder.BackDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build()
                    }).Build(),
                    DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                    {
                        DataBuilder.FrontDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build(),
                        DataBuilder.BackDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build()
                    }).Build()
                }).Build();

            owner.Groups.Add(group);

            GivenOwners = new[] { owner };

            ExpectedResponse = new[]
            {
                new GroupToLessonDto(string.Empty, "GroupName", "1", "2", 3, 3)
            };
        }
    }
}