
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blueprints.Domain;
using Utils;

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
                CreationDate = SystemClock.Now,
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

        internal void TickCard(CardId cardId)
        {
            var card = Cards.Single(x => x.Id == cardId);
            card.Tick();
        }

        internal void AppendCards(int count, int langauges)
        {
            Func<Card, bool> func = null;
            switch (langauges)
            {
                case 1: func = card => { return !card.Front.IsUsed; }; break;
                case 2: func = card => { return !card.Back.IsUsed; }; break;
                case 3: func = card => { return !card.Front.IsUsed && !card.Back.IsUsed; }; break;
                default: break;
            }
            if (func == null) return;

            var cardsToAppend = Cards.Where(func).OrderBy(x => x, new AppendCardsComparer(langauges)).Take(count);
            foreach (var item in cardsToAppend)
            {
                switch (langauges)
                {
                    case 1: appendFront(item); break;
                    case 2: appendBack(item); break;
                    case 3: appendFront(item); appendBack(item); break;
                    default: break;
                }
            }

            void appendFront(Card card) => card.UpdateFront(card.Front.Value, card.Front.Example, true);

            void appendBack(Card card) => card.UpdateBack(card.Back.Value, card.Back.Example, true);

        }
    }

    public class AppendCardsComparer : IComparer<Card>
    {
        private readonly int _languages;

        public AppendCardsComparer(int languages)
        {
            _languages = languages;
        }

        public int Compare(Card x, Card y)
        {
            switch (_languages)
            {
                case 1: return (x.Back.IsUsed ? 0 : 1) - (y.Back.IsUsed ? 0 : 1);
                case 2: return (x.Front.IsUsed ? 0 : 1) - (y.Front.IsUsed ? 0 : 1);
                case 3: return x.CreationDate.CompareTo(y.CreationDate);
                default: return 0;
            }
        }
    }
}