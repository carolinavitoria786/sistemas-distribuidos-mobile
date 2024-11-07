using BookOnTable.Models;
public class OpenLibraryService
{
    private readonly HttpClient _httpClient;

    public OpenLibraryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Book?> GetBookInfoByIdAsync(string bookId)
    {
        var response = await _httpClient.GetAsync($"https://openlibrary.org/works/{bookId}.json");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        return null;
    }

    public async Task<Book?> GetBookInfoByISBNAsync(string isbn)
    {
        var response = await _httpClient.GetAsync($"https://openlibrary.org/isbn/{isbn}.json");
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        return null;
    }
}