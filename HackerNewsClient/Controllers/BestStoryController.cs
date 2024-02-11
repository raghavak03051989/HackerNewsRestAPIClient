using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HackerNewsClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoryController : ControllerBase
    {
        // Use in-memory cache  
        private IMemoryCache cache;

        private static HttpClient client = new HttpClient();

        public BestStoryController(IMemoryCache memoryCache)
        {
            cache = memoryCache;
        }

        // TODO Could store in configuration file/data store.
        const string BestStoriesApi = "https://hacker-news.firebaseio.com/v0/beststories.json";
        const string StoryApiTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

        [HttpGet(Name = "GetHackerStories")]
        public async Task<IEnumerable<HackerStorySummary?>> Index(int NumberOfStories)
        {
            List<HackerStorySummary?> stories = new();

            var response = await client.GetAsync(BestStoriesApi);
            if (response.IsSuccessStatusCode)
            {
                var storiesResponse = response.Content.ReadAsStringAsync().Result;
                var bestIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);

                var tasks = bestIds.Select(GetStoryAsync);
                stories = (await Task.WhenAll(tasks)).OrderByDescending(x => x?.score).Take(NumberOfStories).ToList();
            }
            return stories;
        }

        private async Task<HackerStorySummary?> GetStoryAsync(int storyId)
        {
            return await cache.GetOrCreateAsync(storyId,
                async cacheEntry =>
                {
                    HackerStorySummary? story = new HackerStorySummary();

                    var response = await client.GetAsync(string.Format(StoryApiTemplate, storyId));
                    if (response.IsSuccessStatusCode)
                    {
                        var storyResponse = response.Content.ReadAsStringAsync().Result;
                        story = JsonConvert.DeserializeObject<HackerStorySummary>(storyResponse);
                    }
                    else
                    {
                        story.Title = string.Format("ERROR RETRIEVING STORY (ID {0})", storyId);
                    }

                    return story;
                });
        }
    }
}

