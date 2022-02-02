using System.Collections.Generic;
using System.Linq;

namespace Cards.Domain
{
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
}