using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RankLocator.SearchEngines;
using RankLocator.SearchEngines.Google;
using Xunit;

namespace RankLocator.Tests
{
    public class GoogleSearchEngineTest
    {
        protected GoogleSearchEngine CreateGoogleSearchEngine()
        {
            return new GoogleSearchEngine();
        }

        [Fact]
        public void TestGetBaseUrl()
        {
            var searchEngine = CreateGoogleSearchEngine();
            var url = searchEngine.GetSearchUrl("rank locator");
            Assert.Equal("https://www.google.co.uk/search?num=100&q=rank+locator", url);
        }

        [Fact]
        public void TestParseRawSearchResult()
        {
            var searchEngine = CreateGoogleSearchEngine();

            var raw = File.ReadAllText("../../../Resources/google_rank_locator.html");
            var expectedJson = File.ReadAllText("../../../Resources/google_rank_locator_result.json");
            var expectedResults = JsonConvert.DeserializeObject<List<SearchRecord>>(expectedJson);
            var actualResults = searchEngine.ParseRawSearchResult(raw);

            Assert.Equal(expectedResults.Count, actualResults.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.Equal(expectedResults[i].Url, actualResults[i].Url);
                Assert.Equal(expectedResults[i].Title, actualResults[i].Title);
            }
        }
    }
}
