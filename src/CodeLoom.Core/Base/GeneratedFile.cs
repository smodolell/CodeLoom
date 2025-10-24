namespace CodeLoom.Core.Base;

public class GeneratedFile
{
    public string ResourceName { get; set; } = "";
    public string RawTemplate { get; set; } = "";
    public string FileName { get; set; } = "";
    public string Content { get; set; } = "";
    public List<string> Paths { get; set; } = new List<string>();

    public void SaveFile(bool includeSrc = true)
    {
        var pathRoot = ParameterBase.Instance.PathOutput;
        var solutionName = ParameterBase.Instance.SolutionName;
        var fileName = FileName;
        var content = Content;
        var paths = Paths.ToArray();
        string? pathFinal;

        if (includeSrc && !fileName.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
        {
            pathFinal = Path.Combine(pathRoot, solutionName, "src");
        }
        else
        {
            pathFinal = Path.Combine(pathRoot, solutionName);
        }


        if (!Directory.Exists(pathFinal))
        {
            Directory.CreateDirectory(pathFinal);
        }

        foreach (var path in paths)
        {
            if (string.IsNullOrEmpty(path)) continue;
            pathFinal = Path.Combine(pathFinal, path);
            if (!Directory.Exists(pathFinal))
            {
                Directory.CreateDirectory(pathFinal);
            }
        }
        pathFinal = Path.Combine(pathFinal, fileName);
        try
        {
            File.WriteAllText(pathFinal, content);
            Console.WriteLine($"Archivo Creado: {pathFinal}");
        }
        catch (Exception)
        {
            Console.WriteLine($"ERROR: {pathFinal}");
        }

    }


}