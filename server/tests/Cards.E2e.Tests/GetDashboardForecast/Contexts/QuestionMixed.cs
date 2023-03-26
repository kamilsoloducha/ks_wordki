using System;
using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetDashboardForecast.Contexts;

internal class QuestionMixed : GetDashboardForecastContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<RepeatCount> ExpectedResponse { get; }

    public QuestionMixed()
    {
        var owner = DataBuilder.SampleUser().Build();
        var group = DataBuilder.SampleGroup()
            .With(x => x.Cards = new List<Card>
            {
                DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                {
                    DataBuilder.FrontDetails().With(d => d.IsQuestion = true)
                        .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build(),
                    DataBuilder.BackDetails().With(d => d.IsQuestion = false)
                        .With(d => d.NextRepeat = null).Build()
                }).Build(),
                DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                {
                    DataBuilder.FrontDetails().With(d => d.IsQuestion = true)
                        .With(d => d.NextRepeat = new DateTime(2022, 2, 21)).Build(),
                    DataBuilder.BackDetails().With(d => d.IsQuestion = false)
                        .With(d => d.NextRepeat = null).Build()
                }).Build(),
                DataBuilder.SampleCard().With(c => c.Details = new List<Detail>
                {
                    DataBuilder.FrontDetails().With(d => d.IsQuestion = true)
                        .With(d => d.NextRepeat = new DateTime(2022, 2, 22)).Build(),
                    DataBuilder.BackDetails().With(d => d.IsQuestion = true)
                        .With(d => d.NextRepeat = new DateTime(2022, 2, 26)).Build()
                }).Build()
            }).Build();

        owner.Groups.Add(group);

        GivenOwners = new[] { owner };

        ExpectedResponse = new List<RepeatCount>
        {
            new(2, new DateTime(2022, 2, 21)),
            new(1, new DateTime(2022, 2, 22)),
            new(0, new DateTime(2022, 2, 23)),
            new(0, new DateTime(2022, 2, 24)),
            new(0, new DateTime(2022, 2, 25))
        };
    }
}