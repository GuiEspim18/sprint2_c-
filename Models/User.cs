namespace InvestYOU.Models;

/// <summary>
/// Representa um usuário do sistema.
/// </summary>
public class User
{
    /// <summary>
    /// ID do usuário
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Senha do usuário
    /// </summary>
    public string Password { get; set; } = "";
}