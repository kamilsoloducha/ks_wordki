
using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;

namespace Cards.Domain
{
    public class Group : Entity
    {
        public GroupId Id { get; private set; }
        public GroupName Name { get; private set; }
        public Language FrontLanguage { get; private set; }
        public Language BackLanguage { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IList<Card> Cards { get; private set; }

        public Set CardsSet { get; private set; }

        private Group() { }

        internal static Group Create(GroupName name, LanguageType front, LanguageType back)
            => new Group
            {
                Id = GroupId.Create(),
                Name = name,
                FrontLanguage = Language.Create(front),
                BackLanguage = Language.Create(back),
                CreationDate = DateTime.Now,
                Cards = new List<Card>(),
                IsNew = true,
            };

        public void Update(GroupName groupName, LanguageType front, LanguageType back)
        {
            Name = groupName;
            FrontLanguage = Language.Create(front);
            BackLanguage = Language.Create(back);
            IsDirty = true;
        }

        internal void AddCard(Card card)
        {
            Cards.Add(card);
            IsDirty = true;
        }

        internal void RemoveCard(CardId cardId)
        {
            var card = Cards.Single(x => x.Id == cardId);
            Cards.Remove(card);
        }
    }
}