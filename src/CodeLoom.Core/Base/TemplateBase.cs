using CodeLoom.Core.Extensions;
using DotLiquid;
using System.Text;

namespace CodeLoom.Core.Base;

public abstract class TemplateBase : Drop
{
    public string SolutionName => $"{ParameterBase.Instance.SolutionName}";
    public string NameSpaceBase => $"{ParameterBase.Instance.NameSpaceBase}";
    public string ApplicationName => $"{ParameterBase.Instance.ApplicationName}";

    protected abstract List<string> GetManifestResourceNames();

    public List<GeneratedFile> GetFiles()
    {
        var files = new List<GeneratedFile>();
        foreach (var resourceName in GetManifestResourceNames())
        {
            using var stream = GetType().Assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), $"Resource stream for '{resourceName}' could not be found.");
            }
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var templateText = reader.ReadToEnd();
            Template template = Template.Parse(templateText);

            var renderedText = template.Render(Hash.FromAnonymousObject(new
            {
                context = this
            }));


            var lines = renderedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var _fileName = "";
            var _content = "";
            var _paths = new List<string>();

            var sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("FileName:"))
                {
                    _fileName = lines[i].Replace("FileName:", "");
                }
                else if (lines[i].StartsWith("Paths:"))
                {
                    _paths = lines[i].Replace("Paths:", "").Split("|").ToList();
                }
                else
                {
                    sb.AppendLine(lines[i]);
                }
            }
            _content = sb.ToString();

            var content = _content;
            if (_fileName.EndsWith(".cs"))
            {
                content = content.FormatCode();
            }

            files.Add(new GeneratedFile
            {
                ResourceName = resourceName,
                RawTemplate = templateText,
                FileName = _fileName,
                Content = content,
                Paths = _paths
            });
        }
        return files;
    }
}


public static class TemplateConfig
{
    public static void Configure()
    {
        // Esto hace que DotLiquid respete el naming original
        Template.NamingConvention = new DotLiquid.NamingConventions.CSharpNamingConvention();
    }
}