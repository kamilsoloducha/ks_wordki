using System;
using System.Collections.Generic;

namespace Cards.Application.Services
{
    public class SearchCardsQuery
    {
        public Guid OwnerId { get; }
        public string SearchingTerm { get; }
        public int Skip { get; }
        public int Take { get; }
        public IEnumerable<int> SearchingDrawers { get; }
        public bool? LessonIncluded { get; }
        public bool OnlyTicked { get; }

        private SearchCardsQuery(
            Guid ownerId,
            string searchingTerm,
            IEnumerable<int> drawers,
            bool? lessonIncluded,
            bool onlyTicked,
            int skip,
            int take)
        {
            OwnerId = ownerId;
            SearchingTerm = searchingTerm;
            SearchingDrawers = drawers;
            LessonIncluded = lessonIncluded;
            OnlyTicked = onlyTicked;
            Skip = skip;
            Take = take;
        }

        public static SearchCardsQuery Create(
            Guid ownerId,
            string searchingTerm,
            IEnumerable<int> drawers,
            bool? lessonIncluded,
            bool onlyTicked,
            int pageNumber,
            int pageCount)
        {
            var skip = pageNumber < 1 ? 0 : pageCount * (pageNumber - 1);
            return new SearchCardsQuery(
                ownerId,
                searchingTerm,
                drawers,
                lessonIncluded,
                onlyTicked,
                skip,
                pageCount);
        }

    }
}