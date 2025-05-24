using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GenerateVerticalSliceArchitecture.Commands;
using GenerateVerticalSliceArchitecture.Endpoints;
using GenerateVerticalSliceArchitecture.Queries;

namespace GenerateVerticalSliceArchitecture
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Uso: gvsa <nome-da-feature>");
                return;
            }

            string name = args[0];
            
            var currentDir = Directory.GetCurrentDirectory();
            string projectDir = Path.GetFullPath(Path.Combine(currentDir, @"..\..\..")); 
            string pathMain = Path.Combine(projectDir, "Features", name);
            
            Directory.CreateDirectory(pathMain);

            // Tenta pegar o namespace base do projeto atual
            string namespaceBase = GetRootNamespace(projectDir) ?? "WebApi.Features";

            // string fullNamespace = $"{namespaceBase}.{name}";
            await Task.WhenAll(
                Task.Run(() => Command.GenerateCommand(namespaceBase, name, pathMain)),
                Task.Run(() => Endpoint.GenerateEndpoint(namespaceBase, name, pathMain)),
                Task.Run(() => Query.GenerateQueries(namespaceBase, name, pathMain))
            );

        }

        static string GetRootNamespace(string projectDir)
        {
            // var teste = Path.GetDirectoryName();
            var csprojFile = Directory.GetFiles(projectDir, "*.csproj").FirstOrDefault();

            if (csprojFile == null)
                return null;

            try
            {
                var xml = XDocument.Load(csprojFile);
                var rootNamespace = xml.Descendants("RootNamespace").FirstOrDefault()?.Value;
                
                if (!string.IsNullOrEmpty(rootNamespace))
                    return rootNamespace;

                // Se não achar, retorna o nome do arquivo .csproj como fallback
                return Path.GetFileNameWithoutExtension(csprojFile);
            }
            catch
            {
                return null;
            }
        }
        
        static string Capitalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }
    }
}