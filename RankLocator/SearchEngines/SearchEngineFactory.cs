
using System.Collections.Generic;
using RankLocator.SearchEngines.Google;

namespace RankLocator.SearchEngines
{
    public class SearchEngineNames
    {
        public const string Google = "Google";
    }

    public static class SearchEngineFactory
    {
        ///<summary>
        /// Create search engine by search engine name
        ///<summary>
        public static ISearchEngine Create(string searchEngineName)
        {
            switch (searchEngineName)
            {
                case SearchEngineNames.Google: return new GoogleSearchEngine();
            }
            return null;
        }
    }
}