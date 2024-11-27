namespace Flurl.Http.Spec;

using Antlr4.Runtime;
using AntlrCodeGen;

public class HttpRequestFile : IHttpRequestSpec
{
    private HttpRequestExecutors executors = new();

    /// <inheritdoc/>
    public string Format => "http";

    /// <inheritdoc/>
    public string Spec { get; private set; } = string.Empty;

    /// <inheritdoc/>
    public HttpRequestExecutor this[int index] => this.executors[index];

    /// <inheritdoc/>
    public HttpRequestExecutor this[string key] => string.IsNullOrEmpty(key) ? this.executors[0] : this.executors[key];

    public static HttpRequestFile LoadFromString(string content)
    {
        // Initialize ANTLR4 lexer and parser
        var lexer = new HttpRequestFileLexer(new AntlrInputStream(content));
        var tokens = new CommonTokenStream(lexer);
        tokens.Fill();
        var parser = new HttpRequestFileParser(tokens) { ErrorHandler = new BailErrorStrategy() };
        var tree = parser.file();

        // Walk through the AST (parsing result) in visitor pattern and build the http request executors accordingly
        var builder = HttpRequestExecutors.CreateBuilder();
        var visitor = new HttpRequestFileVisitor(builder);
        var executors = visitor.Visit(tree).Build();
        return new HttpRequestFile
        {
            Spec = content,
            executors = executors,
        };
    }

    public static HttpRequestFile LoadFromFile(string path)
    {
        _ = path.ThrowIfNull();
        var content = File.ReadAllText(path);
        return LoadFromString(content);
    }
}
