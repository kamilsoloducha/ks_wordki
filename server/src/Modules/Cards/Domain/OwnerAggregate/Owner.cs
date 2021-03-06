using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;

namespace Cards.Domain
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

        public GroupId AppendGroup(Group group, ISequenceGenerator sequenceGenerator)
        {

            var newGroup = Group.New(group.Name, group.Front, group.Back, sequenceGenerator);
            _groups.Add(newGroup);

            foreach (var card in group.Cards)
            {
                card.IsPrivate = false;

                newGroup.AddCard(card);
                AddDetails(card, Comment.Create(string.Empty), Comment.Create(string.Empty));
            }

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
                item.Back.IncludeInLesson();
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

            frontDetail.UpdateDetails(
                updateCommand.Front.IncludeLesson.HasValue ?
                updateCommand.Front.IncludeLesson.Value :
                frontDetail.LessonIncluded,
                updateCommand.Front.IsTicked);
            backDetail.UpdateDetails(
                updateCommand.Back.IncludeLesson.HasValue ?
                updateCommand.Back.IncludeLesson.Value :
                backDetail.LessonIncluded,
                updateCommand.Back.IsTicked);

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
            if (group is null) throw new BuissnessObjectNotFoundException(nameof(group), id);
            return group;
        }

        private Detail GetDetail(SideId sideId)
        {
            var detail = Details.FirstOrDefault(x => x.SideId == sideId);
            if (detail is null) throw new BuissnessObjectNotFoundException(nameof(detail), sideId);
            return detail;
        }

        private void AddDetails(Card card, Comment frontComment, Comment backComment)
        {
            var frontDetails = Detail.New(this, card.Front, frontComment);
            var backDetails = Detail.New(this, card.Back, backComment);
            _details.AddRange(new[] { frontDetails, backDetails });
        }

        internal class DetailsPair
        {
            public Detail Front { get; set; }
            public Detail Back { get; set; }
        }
    }
}