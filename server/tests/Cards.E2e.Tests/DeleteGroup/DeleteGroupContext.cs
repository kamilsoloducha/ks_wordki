using System;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.DeleteGroup
{
    public abstract class DeleteGroupContext
    {
        private Group GivenGroup { get; }
        public Owner GivenOwner { get; }
        public virtual long GivenGroupId => GivenGroup.Id; 
    
    
        protected DeleteGroupContext()
        {
            GivenOwner = DataBuilder.SampleUser().Build();
            GivenGroup = DataBuilder.SampleGroup().Build();
            GivenGroup.Cards.Add(DataBuilder.SampleCard().Build());
            GivenOwner.Groups.Add(GivenGroup);
        }

        public virtual int ExpectedSideCount => 2;
        public virtual int ExpectedCardsCount => 1;
        public virtual int ExpectedGroupsCount => 0;
        public virtual int ExpectedDetailsCount => 0;
    }
}