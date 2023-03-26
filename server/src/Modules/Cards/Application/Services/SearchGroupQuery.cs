namespace Cards.Application.Services;

public class SearchGroupsQuery
{
    public string SearchingTerm { get; }
    public int Skip { get; }
    public int Take { get; }

    private SearchGroupsQuery(
        string searchingTerm,
        int skip,
        int take)
    {
        SearchingTerm = searchingTerm;
        Skip = skip;
        Take = take;
    }

    public static SearchGroupsQuery Create(string searchingTerm, int pageNumber, int pageCount)
    {
        var skip = pageNumber < 1 ? 0 : pageCount * (pageNumber - 1);
        return new SearchGroupsQuery(searchingTerm, skip, pageCount);
    }

}