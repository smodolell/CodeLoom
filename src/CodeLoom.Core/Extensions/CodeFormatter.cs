using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;

namespace CodeLoom.Core.Extensions;

public static class CodeFormatter
{
    public static string FormatCode(this string unformattedCode)
    {
        // 1. Crear el árbol de sintaxis
        var syntaxTree = CSharpSyntaxTree.ParseText(unformattedCode);
        var root = syntaxTree.GetRoot();

        // 2. Crear un workspace temporal
        var workspace = new AdhocWorkspace();

        // 3. Obtener opciones de formato (pueden personalizarse)
        var options = workspace.Options;

        // 4. Formatear el árbol de sintaxis
        var formattedRoot = Formatter.Format(root, workspace, options);

        // 5. Devolver el código como string
        return formattedRoot.ToFullString();
    }
}
