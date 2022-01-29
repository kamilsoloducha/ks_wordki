using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;

namespace Cards.Domain2
{
    public class Owner
    {
        private readonly List<Group> _groups;
        private readonly List<Detail> _details;
        public OwnerId Id { get; private set; }
        public IReadOnlyCollection<Group> Groups => _groups;
        public IReadOnlyCollection<Detail> Details => _details;

        private Owner()
        {
            _groups = new();
            _details = new();
        }

        public static Owner Restore(OwnerId id)
            => new Owner
            {
                Id = id
            };

        public GroupId AddGroup(GroupName name, Language front, Language back, ISequenceGenerator sequenceGenerator)
        {
            var newGroup = Group.New(name, front, back, sequenceGenerator);
            _groups.Add(newGroup);

            return newGroup.Id;
        }

        public void MergeGroups(IEnumerable<GroupId> groupIds)
        {
            throw new NotImplementedException();
        }

        public void IncludeToLesson(GroupId groupId, int count, int langauges)
        {
            var group = GetGroup(groupId);
            var random = new Random();

            var detailsPairs = group.Cards.Select(x => new DetailsPair
            {
                Front = GetDetail(x.FrontId),
                Back = GetDetail(x.BackId)
            }).Where(x => !x.Front.LessonIncluded && !x.Back.LessonIncluded)
            .OrderBy(x => random.Next())
            .Take(count);

            foreach (var item in detailsPairs)
            {
                item.Front.IncludeInLesson();
            }
        }

        public void UpdateGroup(GroupId groupId, GroupName name, Language front, Language back)
        {
            var group = GetGroup(groupId);

            group.Update(name, front, back);
        }

        public void TickCard(SideId sideId)
        {
            var detail = GetDetail(sideId);
            detail.Tick();
        }

        public void RemoveGroup(GroupId groupId)
        {
            var group = GetGroup(groupId);
            _groups.Remove(group);
        }

        public CardId AddCard(
            GroupId groupId,
            Label frontValue,
            Label backValue,
            string frontExample,
            string backExample,
            Comment frontComment,
            Comment backComment,
            ISequenceGenerator sequenceGenerator)
        {
            var group = GetGroup(groupId);

            var card = group.AddCard(
                frontValue,
                backValue,
                frontExample,
                backExample,
                sequenceGenerator);

            AddDetails(card, frontComment, backComment);

            return card.Id;
        }

        public CardId UpdateCard(
            GroupId groupId,
            CardId cardId,
            UpdateCardCommand updateCommand,
            ISequenceGenerator sequenceGenerator)
        {
            var result = cardId;
            var group = GetGroup(groupId);
            var card = group.GetCard(cardId);
            var frontDetail = GetDetail(card.FrontId);
            var backDetail = GetDetail(card.BackId);

            if (card.IsPrivate)
            {
                card.Update(
                    updateCommand.Front.Value,
                    updateCommand.Back.Value,
                    updateCommand.Front.Example,
                    updateCommand.Back.Example);
            }
            else
            {
                var newCard = group.AddCard(updateCommand.Front.Value,
                    updateCommand.Back.Value,
                    updateCommand.Front.Example,
                    updateCommand.Back.Example,
                    sequenceGenerator);
                result = newCard.Id;

                group.RemoveCard(cardId);

                frontDetail.AttachNewSide(newCard.FrontId);
                backDetail.AttachNewSide(newCard.BackId);
            }

            frontDetail.UpdateDetails(updateCommand.Front.IncludeLesson, updateCommand.Front.IsTicked);
            backDetail.UpdateDetails(updateCommand.Back.IncludeLesson, updateCommand.Back.IsTicked);

            return result;
        }

        public void RemoveCard(GroupId groupId, CardId cardId)
        {
            var group = GetGroup(groupId);
            var card = group.GetCard(cardId);

            if (card.IsPrivate)
            {
                group.RemoveCard(card);
            }

            var frontDetails = GetDetail(card.FrontId);
            _details.Remove(frontDetails);

            var backDetails = GetDetail(card.BackId);
            _details.Remove(backDetails);
        }

        private Group GetGroup(GroupId id)
        {
            var group = _groups.FirstOrDefault(x => x.Id == id);
            if (group is null) throw new Exception("Group is not found");
            return group;
        }

        private Detail GetDetail(SideId sideId)
        {
            var detail = Details.FirstOrDefault(x => x.SideId == sideId);
            if (detail is null) throw new Exception("Detail is not found");
            return detail;
        }

        private void AddDetails(Card card, Comment frontComment, Comment backComment)
        {
            var frontDetails = Detail.New(this, card.Front, frontComment);
            var backDetails = Detail.New(this, card.Back, backComment);
            _details.AddRange(new[] { frontDetails, backDetails });
        }
    }

    public class UpdateCardCommand
    {
        public Side Front { get; set; }
        public Side Back { get; set; }

        public class Side
        {
            public Label Value { get; set; }
            public string Example { get; set; }
            public Comment Comment { get; set; }
            public bool IncludeLesson { get; set; }
            public bool IsTicked { get; set; }
        }
    }

    internal class DetailsPair
    {
        public Detail Front { get; set; }
        public Detail Back { get; set; }
    }

    public class Group
    {
        private readonly List<Card> _cards;
        public GroupId Id { get; private set; }
        public GroupName Name { get; private set; }
        public Language Front { get; private set; }
        public Language Back { get; private set; }

        public IReadOnlyCollection<Card> Cards => _cards;
        public OwnerId OwnerId { get; private set; }
        public Owner Owner { get; private set; }

        private Group()
        {
            _cards = new();
        }

        public static Group New(GroupName name, Language front, Language back, ISequenceGenerator sequenceGenerator)
            => new Group()
            {
                Id = GroupId.New(sequenceGenerator),
                Name = name,
                Front = front,
                Back = back
            };

        internal void Update(GroupName name, Language front, Language back)
        {
            Name = name;
            Front = front;
            Back = back;
        }

        internal void UpdateCard(CardId cardId, Side front, Side back)
        {
            var card = Cards.First(x => x.Id == cardId);
            card.Update(front, back);
        }

        internal void RemoveCard(CardId cardId)
        {
            var card = Cards.First(x => x.Id == cardId);
            _cards.Remove(card);
        }

        internal Card AddCard(
            Label frontValue,
            Label backValue,
            string frontExample,
            string backExample,
            ISequenceGenerator sequenceGenerator)
        {
            var card = Card.New(
                frontValue,
                backValue,
                frontExample,
                backExample,
                sequenceGenerator);

            _cards.Add(card);

            return card;
        }

        internal Card GetCard(CardId cardId)
        {
            var card = Cards.FirstOrDefault(x => x.Id == cardId);
            if (card is null)
            {
                throw new System.Exception("Card is not found");
            }
            return card;
        }

        internal void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }
    }

    public class Card
    {
        private List<Group> _groups;
        public CardId Id { get; private set; }
        public Side Front { get; private set; }
        public Side Back { get; private set; }
        public bool IsPrivate { get; private set; }
        public IReadOnlyCollection<Group> Groups => _groups;

        public SideId FrontId { get; private set; }
        public SideId BackId { get; private set; }

        private Card()
        {
            _groups = new();
        }

        public static Card New(Label frontValue, Label backValue, string frontExample, string backExample, ISequenceGenerator sequenceGenerator)
        {
            var cardId = CardId.New(sequenceGenerator);
            var front = Side.New(cardId, SideType.Front, frontValue, frontExample, sequenceGenerator);
            var back = Side.New(cardId, SideType.Back, backValue, backExample, sequenceGenerator);
            return new Card
            {
                Id = cardId,
                Front = front,
                Back = back,
                IsPrivate = true,
                BackId = back.Id,
                FrontId = front.Id
            };
        }

        internal void Update(Side front, Side back)
        {
            Front.Update(front.Value, front.Example);
            Back.Update(back.Value, back.Example);
        }

        internal void Update(Label frontValue, Label backValue, string frontExample, string backExample)
        {
            Front.Update(frontValue, frontExample);
            Back.Update(backValue, backExample);
        }
    }

    public class Side
    {
        public SideId Id { get; private set; }
        public SideType Type { get; private set; }
        public Label Value { get; private set; }
        public string Example { get; private set; }

        private Side()
        { }

        public static Side New(
            CardId cardId,
            SideType type,
            Label value,
            string example,
            ISequenceGenerator sequenceGenerator)
        {
            return new Side
            {
                Id = SideId.New(sequenceGenerator),
                Type = type,
                Value = value,
                Example = example,
            };
        }

        internal void Update(Label value, string example)
        {
            Value = value;
            Example = example;
        }
    }

    public class Detail
    {
        public OwnerId OwnerId { get; private set; }
        public SideId SideId { get; private set; }
        public Drawer Drawer { get; private set; }
        public int Counter { get; private set; }
        public NextRepeatMarker NextRepeat { get; private set; }
        public bool LessonIncluded { get; private set; }
        public bool IsTicked { get; private set; }

        public Comment Comment { get; private set; }
        public Owner Owner { get; private set; }


        private Detail() { }

        public static Detail New(Owner owner, Side side, Comment comment)
            => new Detail()
            {
                OwnerId = owner.Id,
                SideId = side.Id,
                Comment = comment,
                Counter = 0,
                Drawer = Drawer.New(),
                LessonIncluded = false,
                NextRepeat = NextRepeatMarker.New(),
                Owner = owner,
            };

        internal void UpdateLabels(Label value, string example, Comment comment)
        {
            Comment = comment;
        }

        public void Tick()
        {
            IsTicked = true;
        }

        public void RegisterAnswer(int result, INextRepeatCalculator nextRepeatCalculator)
        {
            if (IsCorrect(result))
                Drawer = Drawer.Increase();
            else if (IsWrong(result))
                Drawer = Drawer.New();

            NextRepeat = NextRepeatMarker.Restore(nextRepeatCalculator.Calculate(this, result));

            bool IsCorrect(int result) => result > 0;
            bool IsWrong(int result) => result < 0;
        }

        internal void IncludeInLesson()
        {
            if (LessonIncluded) throw new Exception("Card is already included");

            LessonIncluded = true;
        }

        internal void UpdateDetails(bool includeLesson, bool isTicked)
        {
            LessonIncluded = includeLesson;
            IsTicked = isTicked;
        }

        internal void AttachNewSide(SideId sideId) => SideId = sideId;
    }

    public interface ICardsRepository
    {
        Task<Owner> Get(OwnerId id, CancellationToken cancellationToken);
        Task Update(Owner owner, CancellationToken cancellationToken);
        Task Add(Owner owner, CancellationToken cancellationToken);

        Task<Detail> Get(OwnerId ownerId, SideId sideId, CancellationToken cancellationToken);
        Task Update(CancellationToken cancellationToken);
    }

    public enum SideType
    {
        None,
        Front,
        Back
    }
}