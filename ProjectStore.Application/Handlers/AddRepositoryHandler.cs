using System.Net.Http.Json;
using MediatR;
using ProjectStore.Application.Requests;

namespace ProjectStore.Application.Handlers;

public class AddRepositoryHandler : IRequestHandler<AddRepositoryRequest,AddRepositoryRequest.Response>
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "https://localhost:7214";
    
    public AddRepositoryHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<AddRepositoryRequest.Response> Handle(AddRepositoryRequest request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Работает метод Handle из Handler");
        var response = await _httpClient.PostAsJsonAsync<AddRepositoryRequest>($"{BaseUrl}/{AddRepositoryRequest.RouteTemplate}", request, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var userId = await response.Content.ReadFromJsonAsync<int>(cancellationToken:cancellationToken);
            return new AddRepositoryRequest.Response(userId);
        }
        else
        {
            return new AddRepositoryRequest.Response(-1);
        }
    }
}