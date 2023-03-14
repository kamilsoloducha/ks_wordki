using System.Collections.Generic;
using System.Linq;
using Cards.Domain.Commands;
using Cards.Domain.Enums;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate
{
    public class Group : Entity, IAggregateRoot
    {
        private readonly List<Card> _cards = new();

        public GroupName Name { get; set; }
        public long? ParentId { get; }
        public string Front { get; set; }
        public string Back { get; set; }

        public virtual Owner Owner { get; private set; }
        public virtual IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        protected Group()
        {
        }

        public Group(GroupName name, string front, string back, Owner owner) : this()
        {
            Name = name;
            Front = front;
            Back = back;
            Owner = owner;
        }

        public Group(Group group, Owner owner)
        {
            Name = group.Name;
            Front = group.Front;
            Back = group.Back;
            Owner = owner;
            ParentId = group.ParentId ?? group.Id;
        }

        public Card AddCard(AddCardCommand command)
        {
            var newCard = new Card(command, this);
            _cards.Add(newCard);
            return newCard;
        }

        public Card UseCard(Card card)
        {
            var newCard = new Card(card.Front, card.Back, this);
            _cards.Add(newCard);
            return newCard;
        }

        public void Remove()
        {
            _cards.ForEach(x => x.Remove());

            Owner = null;
        }

        public void IncludeToLesson(int count, string language)
        {
            var searchingSide = GetSideType(language);

            var details = Cards
                .SelectMany(x => x.Details.Where(d => d.SideType == searchingSide && !d.NextRepeat.HasValue))
                .OrderBy(x => x.Card.Id)
                .Take(count);

            foreach (var detail in details)
            {
                detail.SetQuestionable(true);
            }
        }

        private SideType GetSideType(string language)
        {
            if (language == Front) return SideType.Front;
            if (language == Back) return SideType.Back;
            return SideType.None;
        }
    }
}