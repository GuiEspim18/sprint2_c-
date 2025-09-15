namespace InvestYOU.Models;

public class IAData
{
    public string Prompt { get; set; } = string.Empty;
    public int MaxTokens { get; set; }
    public double TextTemperature { get; set; }
    public List<string> Files { get; set; } = new();
}
