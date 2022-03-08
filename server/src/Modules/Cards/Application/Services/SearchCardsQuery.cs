using System.Collections.Generic;

namespace Cards.Application.Services
{
    public class SearchCardsQuery
    {
        public string SearchingTerm { get; }
        public int Skip { get; }
        public int Take { get; }
        public IEnumerable<int> SearchingDrawers { get; }
        public bool? LessonIncluded { get; }
        public bool? IsTicked { get; }

        private SearchCardsQuery(
            string searchingTerm,
            IEnumerable<int> drawers,
            bool? lessonIncluded,
            bool? isTicked,
            int skip,
            int take)
        {
            SearchingTerm = searchingTerm;
            SearchingDrawers = drawers;
            LessonIncluded = lessonIncluded;
            IsTicked = isTicked;
            Skip = skip;
            Take = take;
        }

        public static SearchCardsQuery Create(
            string searchingTerm,
            IEnumerable<int> drawers,
            bool? lessonIncluded,
            bool? isTicked,
            int pageNumber,
            int pageCount)
        {
            var skip = pageNumber < 1 ? 0 : pageCount * (pageNumber - 1);
            return new SearchCardsQuery(
                searchingTerm,
                drawers,
                lessonIncluded,
                isTicked,
                skip,
                pageCount);
        }

    }
}