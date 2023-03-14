using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate
{
    public class Side : Entity
    {
        public Label Label { get; }
        public Example Example { get; }

        protected Side()
        {
        }

        internal Side(Label label, Example example) : this()
        {
        
            Label = label;
            Example = example;
        }
    }
}