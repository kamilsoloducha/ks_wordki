using System.Collections.Generic;

namespace Cards.Domain
{
    public class Card
    {
        private List<Group> _groups;
        public CardId Id { get; private set; }
        public Side Front { get; private set; }
        public Side Back { get; private set; }
        public bool IsPrivate { get; internal set; }
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

        internal void Update(Label frontValue, Label backValue, string frontExample, string backExample)
        {
            Front.Update(frontValue, frontExample);
            Back.Update(backValue, backExample);
        }
    }
}