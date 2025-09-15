namespace InvestYOU.Models;

/// <summary>
/// Representa os dados da IA, incluindo prompt, tokens, temperatura e arquivos associados.
/// </summary>
public class IAData
{
    /// <summary>
    /// Texto do prompt
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// Máximo de tokens a serem usados
    /// </summary>
    public int MaxTokens { get; set; }

    /// <summary>
    /// Temperatura do texto
    /// </summary>
    public double TextTemperature { get; set; }

    /// <summary>
    /// Lista de arquivos relacionados
    /// </summary>
    public List<string> Files { get; set; } = new();
}
