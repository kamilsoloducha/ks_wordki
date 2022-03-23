using System;
using Api.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Api.Tests.Builders.Cards
{
    public static class OwnerBuilder
    {

        public static Guid OwnerId => Guid.Parse("1f4c1993-d017-4b5b-8a44-5d8fb1561d55");

        public static ISingleObjectBuilder<Owner> Default =>
            Builder<Owner>
                .CreateNew();

        public static Owner AddGroup(this Owner owner, Group group)
        {
            owner.Groups.Add(group);
            return owner;
        }

        public static Owner AddDetail(this Owner owner, Detail detail)
        {
            owner.Details.Add(detail);
            return owner;
        }
    }

    public static class DetailBuilder
    {

        public static string Comment => "Comment";
        public static int Counter => 10;
        public static int Drawer => 3;
        public static bool IsTicked => false;
        public static bool LessonIncluded => true;
        public static DateTime NextRepeat => new DateTime(2022, 2, 2);

        public static ISingleObjectBuilder<Detail> Default =>
            Builder<Detail>
                .CreateNew()
                .With(x => x.OwnerId = OwnerBuilder.OwnerId)
                .With(x => x.Comment = Comment)
                .With(x => x.Counter = Counter)
                .With(x => x.Drawer = Drawer)
                .With(x => x.IsTicked = IsTicked)
                .With(x => x.LessonIncluded = LessonIncluded)
                .With(x => x.NextRepeat = NextRepeat);
    }

    public static class GroupBuilder
    {
        public static long Id => 10;
        public static string Name => "GroupName";
        public static int Front => 1;
        public static int Back => 2;

        public static ISingleObjectBuilder<Group> Default =>
            Builder<Group>
                .CreateNew()
                .With(x => x.OwnerId = OwnerBuilder.OwnerId)
                .With(x => x.Name = Name)
                .With(x => x.Front = Front)
                .With(x => x.Back = Back);

        public static Group AddCard(this Group group, Card card)
        {
            var groupCard = new GroupsCard
            {
                GroupsId = group.Id,
                CardsId = card.Id,
                Cards = card,
                Groups = group
            };
            group.GroupsCards.Add(groupCard);
            return group;
        }
    }

    public static class SideBuilder
    {
        public static long Id => 10;
        public static int Type => 1;
        public static string Value => "Value";
        public static string Example => "Example";

        public static ISingleObjectBuilder<Side> Default =>
            Builder<Side>
                .CreateNew()
                .With(x => x.Id = Id)
                .With(x => x.Type = Type)
                .With(x => x.Value = Value)
                .With(x => x.Example = Example);

    }
}