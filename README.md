HackerNews REST API Client

Rest API Client for displaying data from Hacker News API.

PreRequisites

Install .NET 8.0 SDK from https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Steps to run the Project

Download the code from the Github link Open Command Prompt run the command dotnet dev-certs https --trust Cd to the folder API (in the Downloaded Code) run the command dotnet run Cd to the folder client (in the Downloaded Code) run the command ng serve Now we should see a web page (http://localhost:5025/swagger/index.html) which should open swagger and list all the API's available

Development challenge Use httpsgithub.comHackerNewsAPI to generate API using C# and .NET that displays the N number of stories of the current best stories on Hacker News.

Requirement

As a User, I would like to see a list of the best N stories from Hacker News API where N is provided by the user to the API in descending order of score.

Used In-Memory cache. https://docs.microsoft.comen-usaspnetcoreperformancecachingmemory#caching-basics

Improvements

As a User, I would like the ability to search best stories by title. As a User, I would like the ability to search best stories by author. As a User, I would like viewing and searching of best stories to be responsive. I should not have to wait more than a couple seconds for the api to retrieve the data.

Investigate better error handling for BestStoryController.GetStoryAsnc. Currently, if there is an issue retrieving story details, creates a story object with an error message in title then saves to cache. Trouble spot would be on subsequent calls. Even if connection was restored, story id is still in cache and would return object with error in title. Possible solution; on error, do not store error story in cache
