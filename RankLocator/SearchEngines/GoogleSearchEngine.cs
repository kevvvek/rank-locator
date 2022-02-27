using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace RankLocator.SearchEngines.Google
{
    public class GoogleSearchEngine : ISearchEngine
    {
        ///<summary>
        /// Maximum records for searching, default 100
        ///</summary>
        public int MaximumResult { get; set; } = 100;

        ///<summary>
        /// Base URL for searching, default https://www.google.co.uk/search
        ///</summary>
        public string BaseUrl { get; set; } = "https://www.google.co.uk/search";


        public SearchResponse Search(SearchRequest request)
        {
            var response = new SearchResponse();

            try
            {
                var raw = GetRawSearchResult(request.Keyword);

                var searchResults = ParseRawSearchResult(raw);

                response.Records.AddRange(searchResults);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        ///<summary>
        /// Get the search url
        ///<summary>
        public string GetSearchUrl(string keyword)
        {
            var escapedKeyword = Uri.EscapeDataString(keyword);
            escapedKeyword = escapedKeyword.Replace("%20", "+"); // replace space to +
            return $"{BaseUrl}?num={MaximumResult}&q={escapedKeyword}";
        }

        ///<summary>
        /// Call the search engine and return the raw HTML
        ///<summary>
        public string GetRawSearchResult(string keyword)
        {
            var url = GetSearchUrl(keyword);

            var client = new WebClient();
            var result = client.DownloadString(url);
            return result;
        }

        ///<summary>
        /// Read the Raw HTML and return the parsed results
        ///<summary>
        public List<SearchRecord> ParseRawSearchResult(string raw)
        {
            var results = new List<SearchRecord>();
            // from the raw file, url found in <a href="(url)"> and followed by <h3 ...><div ...>(title)</div></h3>
            var matches = Regex.Matches(raw, "<a[^>]*href=\"([^\"]*)\">.*?<h3[^>]*>\\S*<div[^>]*>(.*?)</div>\\S*</h3>");

            int rank = 1;
            foreach (Match match in matches)
            {
                results.Add(new SearchRecord()
                {
                    Rank = rank++,
                    Url = match.Groups[1].ToString(),
                    Title = match.Groups[2].ToString()
                });
            }

            return results;
        }
    }
}