namespace CodeLoom.Core.Extensions;

public static class StringExtensions
{
    public static string FirstCharToLower(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length == 1)
            return str.ToLower();

        return char.ToLower(str[0]) + str.Substring(1);
    }

    public static string FirstCharToUpper(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length == 1)
            return str.ToUpper();

        return char.ToUpper(str[0]) + str.Substring(1);
    }
    /// <summary>
    /// Convierte el texto a un formato adecuado para versiones: minúsculas, sin espacios y con underscores.
    /// </summary>
    /// <param name="input">Texto de entrada</param>
    /// <returns>Texto formateado o string vacío si es nulo o vacío</returns>
    public static string ToVersionFormat(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input.ToLower()
                   .Trim()
                   .Replace(" ", "_");
    }

    /// <summary>
    /// Convierte el texto a un formato adecuado para sistemas: minúsculas, sin espacios y con underscores.
    /// </summary>
    /// <param name="input">Texto de entrada</param>
    /// <returns>Texto formateado o string vacío si es nulo o vacío</returns>
    public static string ToSystemFormat(this string input)
    {
        // Reutilizamos el mismo método ya que la lógica es idéntica
        return input.ToVersionFormat();
    }

    /// <summary>
    /// Formato más genérico para slugs/identificadores: minúsculas, sin espacios y con underscores.
    /// </summary>
    /// <param name="input">Texto de entrada</param>
    /// <returns>Texto formateado o string vacío si es nulo o vacío</returns>
    public static string ToSlugFormat(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input.ToLower()
                   .Trim()
                   .Replace(" ", "_")
                   .Replace("-", "_")
                   .Replace(".", "_");
    }
}
