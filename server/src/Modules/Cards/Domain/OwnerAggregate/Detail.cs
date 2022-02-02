using System;

namespace Cards.Domain
{
    public class Detail
    {
        public OwnerId OwnerId { get; private set; }
        public SideId SideId { get; private set; }
        public Drawer Drawer { get; private set; }
        public int Counter { get; private set; }
        public NextRepeatMarker NextRepeat { get; private set; }
        public bool LessonIncluded { get; private set; }
        public bool IsTicked { get; private set; }

        public Comment Comment { get; private set; }
        public Owner Owner { get; private set; }

        private Detail() { }

        public static Detail New(Owner owner, Side side, Comment comment)
            => new Detail()
            {
                OwnerId = owner.Id,
                SideId = side.Id,
                Comment = comment,
                Counter = 0,
                Drawer = Drawer.New(),
                LessonIncluded = false,
                NextRepeat = NextRepeatMarker.New(),
                Owner = owner,
            };

        internal void UpdateLabels(Label value, string example, Comment comment)
        {
            Comment = comment;
        }

        public void Tick()
        {
            IsTicked = true;
        }

        public void RegisterAnswer(int result, INextRepeatCalculator nextRepeatCalculator)
        {
            UpdateDrawer(result);
            NextRepeat = NextRepeatMarker.Restore(nextRepeatCalculator.Calculate(this, result));
            Counter++;
        }

        internal void IncludeInLesson()
        {
            if (LessonIncluded) throw new Exception("Card is already included");

            LessonIncluded = true;
        }

        internal void UpdateDetails(bool includeLesson, bool isTicked)
        {
            LessonIncluded = includeLesson;
            IsTicked = isTicked;
        }

        private void UpdateDrawer(int result)
        {
            if (IsCorrect(result))
                Drawer = Drawer.Increase(ShouldBeBoosted() ? 2 : 1);
            else if (IsWrong(result))
                Drawer = Drawer.New();

            bool IsCorrect(int result) => result > 0;
            bool IsWrong(int result) => result < 0;
            bool ShouldBeBoosted() => (Counter == 0 && Drawer.CorrectRepeat == 0)
                || (Counter == 1 && Drawer.CorrectRepeat == 2);
        }

        internal void AttachNewSide(SideId sideId) => SideId = sideId;

        internal static Detail Restore(Drawer drawer, int counter)
            => new Detail
            {
                Drawer = drawer,
                Counter = counter
            };
    }
}