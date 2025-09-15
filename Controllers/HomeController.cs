using InvestYOU.Models;

namespace InvestYOU.Controllers;

public class HomeController
{
    public async Task<IAData> LoadAsync()
    {
        return await CsvHelper.LoadIADataAsync() ?? new IAData();
    }

    public async Task SaveAsync(IAData data)
    {
        await CsvHelper.SaveIADataAsync(data);
    }

    public async Task UploadDocumentAsync(string filePath)
    {
        var data = await LoadAsync();
        data.Files.Add(filePath);
        await SaveAsync(data);
    }

    public async Task SavePromptAsync(string prompt)
    {
        var data = await LoadAsync();
        data.Prompt = prompt;
        await SaveAsync(data);
    }

    public async Task SaveMaxTokensAsync(int maxTokens)
    {
        var data = await LoadAsync();
        data.MaxTokens = maxTokens;
        await SaveAsync(data);
    }

    public async Task SaveTextTemperatureAsync(double temperature)
    {
        var data = await LoadAsync();
        data.TextTemperature = temperature;
        await SaveAsync(data);
    }
}
