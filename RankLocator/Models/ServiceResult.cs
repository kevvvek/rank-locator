using System.Collections.Generic;

namespace RankLocator.Models
{
    ///<summary>
    /// Request class for RankController.Search
    ///<summary>
    public class RankSearchRequest
    {
        ///<summary>
        /// Search engine name
        ///</summary>
        public string Engine { get; set; }

        ///<summary>
        /// Keyword to search
        ///</summary>
        public string Keyword { get; set; }

        ///<summary>
        /// URL for ranking
        ///</summary>
        public string Url { get; set; }
    }

    ///<summary>
    /// Response class for RankController.Search
    ///<summary>
    public class RankSearchResponse
    {
        ///<summary>
        /// Search is success or not
        ///</summary>
        public bool IsSuccess { get; set; }

        ///<summary>
        /// Error message if any
        ///</summary>
        public string Message { get; set; }

        ///<summary>
        /// The result ranking
        ///</summary>
        public List<int> Ranks { get; set; }
    }
}