using InvestYOU.Models;

namespace InvestYOU.Controllers;

/// <summary>
/// Controlador responsável por gerenciar os dados da IA, como prompt, tokens, temperatura e arquivos.
/// Utiliza CsvHelper para persistência.
/// </summary>
public class HomeController
{
    /// <summary>
    /// Carrega os dados da IA.
    /// </summary>
    /// <returns>Objeto IAData com as informações atuais</returns>
    public async Task<IAData> LoadAsync()
    {
        return await CsvHelper.LoadIADataAsync() ?? new IAData();
    }

    /// <summary>
    /// Salva os dados da IA.
    /// </summary>
    /// <param name="data">Objeto IAData com os dados a serem salvos</param>
    public async Task SaveAsync(IAData data)
    {
        await CsvHelper.SaveIADataAsync(data);
    }

    /// <summary>
    /// Adiciona um documento à lista de arquivos da IA e salva.
    /// </summary>
    /// <param name="filePath">Caminho do arquivo</param>
    public async Task UploadDocumentAsync(string filePath)
    {
        var data = await LoadAsync();
        data.Files.Add(filePath);
        await SaveAsync(data);
    }

    /// <summary>
    /// Salva apenas o prompt da IA.
    /// </summary>
    /// <param name="prompt">Texto do prompt</param>
    public async Task SavePromptAsync(string prompt)
    {
        var data = await LoadAsync();
        data.Prompt = prompt;
        await SaveAsync(data);
    }

    /// <summary>
    /// Salva apenas o valor de MaxTokens da IA.
    /// </summary>
    /// <param name="maxTokens">Valor máximo de tokens</param>
    public async Task SaveMaxTokensAsync(int maxTokens)
    {
        var data = await LoadAsync();
        data.MaxTokens = maxTokens;
        await SaveAsync(data);
    }

    /// <summary>
    /// Salva apenas o valor de TextTemperature da IA.
    /// </summary>
    /// <param name="temperature">Temperatura do texto</param>
    public async Task SaveTextTemperatureAsync(double temperature)
    {
        var data = await LoadAsync();
        data.TextTemperature = temperature;
        await SaveAsync(data);
    }
}
