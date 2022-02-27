
using System.Collections.Generic;

namespace RankLocator.SearchEngines
{
    public class SearchRequest
    {
        ///<summary>
        /// Keyword for searching
        ///</summary>
        public string Keyword { get; set; }
    }

    public class SearchResponse
    {
        ///<summary>
        /// Indicate search is success or not.
        ///</summary>
        public bool IsSuccess { get; set; }

        ///<summary>
        /// Error message if any.
        ///</summary>
        public string Message { get; set; }

        ///<summary>
        /// Records parsed from search engine result.
        ///</summary>
        public List<SearchRecord> Records { get; set; } = new List<SearchRecord>();
    }

    public class SearchRecord
    {
        ///<summary>
        /// Rank in search engine
        ///</summary>
        public int Rank { get; set; }
        
        ///<summary>
        /// URL captured in search engine
        ///</summary>
        public string Url { get; set; }

        ///<summary>
        /// Title captured in search engine
        ///</summary>
        public string Title { get; set; }
    }

    public interface ISearchEngine
    {
        ///<summary>
        /// Call search engine to search for sepcific keyword
        ///</summary>
        public SearchResponse Search(SearchRequest request);
    }
}