namespace ProjectStore.Domen.Models;

public class Repository : Base
{
    public string Name { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public int Stargazers { get; set; } = 0;
    public int Watchers { get; set; } = 0;
    public string Url { get; set; } = string.Empty;
}