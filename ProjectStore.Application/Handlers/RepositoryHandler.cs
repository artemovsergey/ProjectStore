using System.Linq.Dynamic.Core;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectStore.Application.Generics;
using ProjectStore.Application.Requests;
using ProjectStore.Domen.Models;
using ProjectStore.Infrastructure.Data;
using Newtonsoft.Json.Linq;

namespace ProjectStore.Application.Handlers;

public class RepositoryHandler : IRequestHandler<RepositoryRequest, RepositoryRequest.Response>
{
    private readonly ProjectStoreContext _db;
    private readonly ILogger<RepositoryHandler> _log;
    public RepositoryHandler(ProjectStoreContext db, ILogger<RepositoryHandler> log)
    {
        _db = db;
        _log = log;
    }

    public async Task<RepositoryRequest.Response> Handle(RepositoryRequest request, CancellationToken cancellationToken)
    {
        
        if (!string.IsNullOrEmpty(request.sortColumn) && IsValidProperty(request.sortColumn))
        {
            request = request with
            {
                sortOrder = !string.IsNullOrEmpty(request.sortOrder) && request.sortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC"
            };
        }
        
        IQueryable<Repository> newsList = _db.Repositories;

        if (!string.IsNullOrEmpty(request.filterColumn)
            && !string.IsNullOrEmpty(request.filterQuery)
            && IsValidProperty(request.filterColumn))
        {
            //users = users.Where(u => $"{filterColumn}".Contains($"{filterQuery})"));
            newsList = newsList.Where($"{request.filterColumn}.Contains(@0)", request.filterQuery);

            Console.WriteLine($"Фильтрация: {newsList.Count()}");

            // Если в базе данных нет записей, то поиск по API Github и сохранение в базу
            if (newsList.Count() == 0)
            {
                var githubpublicapi = $"https://api.github.com/search/repositories?q={request.filterQuery}";
                
                HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
                HttpResponseMessage response = await client.GetAsync(githubpublicapi);
                
                string responseBody = await response.Content.ReadAsStringAsync();
                
                var jsonObject = JObject.Parse(responseBody);
                JToken items = jsonObject["items"];
                
                // Todo Настроить hash для уникальности
                foreach (JToken item in items)
                {
                    Console.WriteLine(item["name"]);
                    Console.WriteLine("--------------------------");
                    
                    var repo = new Repository
                    {
                        Name = item["name"].ToString(),
                        Owner = item["owner"]["login"].ToString(),
                        Stargazers = item["stargazers_count"].Value<int>(),
                        Watchers = item["watchers_count"].Value<int>(),
                        Url = item["html_url"].ToString()
                    };
                    _db.Repositories.Add(repo);
                    await _db.SaveChangesAsync();
                }
                
            }
            
            
        }
        
        
        var respositories = await newsList.OrderBy($"{request.sortColumn} {request.sortOrder}")
            .Skip(request.pageIndex * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync();
        
        var apiresult = new ApiResult<Repository>((List<Repository>)respositories,
            _db.Repositories.Count(),
            request.pageIndex,
            request.pageSize,
            request.sortColumn,
            request.sortOrder,
            request.filterColumn,
            request.filterQuery);
        
        return new RepositoryRequest.Response(apiresult);
    }
    
    public static bool IsValidProperty(string propertyName,
        bool throwExceptionIfNotFound = true)
    {
        var prop = typeof(Repository).GetProperty(
            propertyName,

            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.Instance);
        if (prop == null && throwExceptionIfNotFound)
            throw new NotSupportedException(
                string.Format($"ERROR: Property '{propertyName}' does not exist.")
            );
        return prop != null;

    }
    
}