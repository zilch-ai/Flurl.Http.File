namespace Flurl.Http.Spec;

public partial class HttpRequestFile
{
    public static HttpRequestFile LoadFromString(string content)
    {
        var (context, executors) = HttpRequestBlocks.All.Parse(content);

        return new()
        {
            Spec = content,
            Context = context,
            executors = executors!,
        };
    }

    public static HttpRequestFile LoadFromFile(string path)
    {
        _ = path.ThrowIfNull();
        var content = File.ReadAllText(path);
        return LoadFromString(content);
    }
}
