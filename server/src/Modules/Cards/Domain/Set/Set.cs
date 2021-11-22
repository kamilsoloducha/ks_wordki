
using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;

namespace Cards.Domain
{
    public class Set : Entity, IAggregateRoot
    {
        public SetId Id { get; private set; }
        public UserId UserId { get; private set; }
        public IList<Group> Groups { get; private set; }

        private Set() { }

        public Group GetGroup(GroupId groupId) => Groups.FirstOrDefault(x => x.Id == groupId);

        public void RemoveGroup(GroupId groupId)
        {
            var item = GetGroup(groupId);
            Groups.Remove(item);
        }

        public static Set Create(UserId userId)
        {
            var newCardsSet = new Set
            {
                Id = SetId.Create(),
                UserId = userId,
                Groups = new Group[0],
                IsNew = true
            };
            return newCardsSet;
        }

        public void UpdateCard(
            GroupId groupId,
            CardId cardId,
            SideLabel frontValue,
            string frontExample,
            bool frontIsUsed,
            SideLabel backValue,
            string backExample,
            bool backIsUsed,
            string comment)
        {
            var group = GetGroup(groupId);

            var card = group.Cards.FirstOrDefault(x => x.Id == cardId);
            card.UpdateFront(frontValue, frontExample, frontIsUsed);
            card.UpdateBack(backValue, backExample, backIsUsed);
            card.UpdateComment(comment);
        }

        public void RemoveCard(GroupId groupId, CardId cardId)
        {
            var group = GetGroup(groupId);
            group.RemoveCard(cardId);
        }

        public GroupId AddGroup(GroupName groupName, LanguageType front, LanguageType back)
        {
            var newGroup = Group.Create(groupName, front, back);
            Groups.Add(newGroup);

            return newGroup.Id;
        }

        public CardId AddCard(
            GroupId groupId,
            SideLabel frontValue,
            string frontExample,
            bool frontIsUsed,
            SideLabel backValue,
            string backExample,
            bool backIsUsed,
            string comment)
        {
            var group = Groups.SingleOrDefault(x => x.Id == groupId);

            if (group is null)
            {
                throw new Exception("group is null");
            }

            var newCard = Card.Create(frontValue, frontExample, frontIsUsed, backValue, backExample, backIsUsed, comment);
            group.AddCard(newCard);

            return newCard.Id;
        }

        public void RegisterAnswer(GroupId groupId, CardId cardId, Side sideType, int result, INextRepeatCalculator nextRepeatCalculator)
        {
            var group = GetGroup(groupId);
            var card = group.Cards.FirstOrDefault(x => x.Id == cardId);

            card.RegisterAnswer(sideType, result, nextRepeatCalculator);
        }

        public void ChangeUsage(CardId cardId, Side side)
        {
            var card = Groups.SelectMany(x => x.Cards).SingleOrDefault(x => x.Id == cardId);
            var cardSide = card.GetSide(side);
            cardSide.ChangeUsage();
        }
    }
}