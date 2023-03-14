using System.Collections.Generic;
using System.Linq;
using Cards.Domain.Commands;
using Cards.Domain.Enums;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate
{
    public class Card : Entity, IAggregateRoot
    {
        private readonly List<Details> _details = new();
        public virtual Side Front { get; private set; }
        public virtual Side Back { get; private set; }

        public virtual IReadOnlyList<Details> Details => _details.AsReadOnly();
        public Details FrontDetails => _details.Single(x => x.SideType == SideType.Front);
        public Details BackDetails => _details.Single(x => x.SideType == SideType.Back);

        public virtual Group Group { get; }

        protected Card()
        {
        }

        public Card(AddCardCommand command, Group group) : this()
        {
            Front = new Side(command.FrontValue, command.FrontExample);
            Back = new Side(command.BackValue, command.BackExample);
            _details.AddRange(new[]
            {
                new Details(SideType.Front, command.FrontIsUsed, this),
                new Details(SideType.Back, command.BackIsUsed, this)
            });
            Group = group;
        }

        public Card(Label front, Label back, Example frontExample, Example backExample, Group group) : this()
        {
            Front = new Side(front, frontExample);
            Back = new Side(back, backExample);
            _details.AddRange(new[]
            {
                new Details(SideType.Front, false, this),
                new Details(SideType.Back, false, this)
            });
            Group = group;
        }

        public Card(Side front, Side back, Group group) : this()
        {
            Front = front;
            Back = back;
            _details.AddRange(new[]
            {
                new Details(SideType.Front, false, this),
                new Details(SideType.Back, false, this)
            });
            Group = group;
        }

        public void Update(UpdateCard command)
        {
            if (ShouldBeUpdated(Front, command.Front))
            {
                Front = new Side(command.Front.Label, command.Front.Example);
            }

            if (ShouldBeUpdated(Back, command.Back))
            {
                Back = new Side(command.Back.Label, command.Back.Example);
            }

            UpdateDetails(FrontDetails, command.Front);
            UpdateDetails(BackDetails, command.Back);
        }

        public void Tick() => _details.ForEach(x => x.IsTicked = true);

        public void Remove() => _details.Clear();

        public void Register(SideType sideType, int result, INextRepeatCalculator nextRepeatCalculator)
        {
            var details = _details.Single(x => x.SideType == sideType);

            switch (result)
            {
                case < 0:
                    details.AnswerWrong();
                    break;
                case > 0: details.AnswerCorrect(nextRepeatCalculator);
                    break;
                default: details.AnswerAccepted();
                    break;
            }
        }

        private bool ShouldBeUpdated(Side side, Commands.Side newSide) =>
            side.Label != newSide.Label || side.Example != newSide.Example;

        private void UpdateDetails(Details details, Commands.Side newSide)
        {
            details.SetQuestionable(newSide.UseAsQuestion);
        }
    }
}