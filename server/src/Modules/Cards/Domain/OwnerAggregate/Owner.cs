using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate
{
    public class Owner : Entity, IAggregateRoot
    {
        private readonly List<Group> _groups = new();
        public UserId UserId { get; }
        public virtual IReadOnlyList<Group> Groups => _groups.AsReadOnly();

        protected Owner()
        {
        }

        public Owner(UserId userId) : this()
        {
            UserId = userId;
        }

        public Group CreateGroup(GroupName name, string front, string back)
        {
            var newGroup = new Group(name, front, back, this);

            _groups.Add(newGroup);

            return newGroup;
        }

        public Group UseGroup(Group group)
        {
            if (_groups.Any(x => x.Id == group.Id))
            {
                throw new Exception("Group already exists");
            }

            var newGroup = new Group(group, this);

            foreach (var card in group.Cards)
            {
                newGroup.UseCard(card);
            }
        
            _groups.Add(newGroup);

            return newGroup;
        }

        internal void RemoveGroup(Group group) => _groups.Remove(group);
    }
}