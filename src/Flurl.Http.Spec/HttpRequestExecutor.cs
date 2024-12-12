namespace Flurl.Http.Spec;

using HttpRequestHeaders = System.Collections.Immutable.ImmutableDictionary<string, string>;

public class HttpRequestExecutor(string verb, string endpoint)
{
    public string Verb { get; } = verb;

    public string Endpoint { get; } = endpoint;

    public string Version { get; init; } = "1.1";

    public HttpRequestHeaders Headers { get; init; } = HttpRequestHeaders.Empty;

    public string Body { get; init; } = string.Empty;

    public Action<IFlurlRequest>? Configurator { get; set; }

    public async Task<IFlurlResponse> ExecuteAsync(HttpRequestContext? context = null)
    {
        // Render template variables in the url, headers & body
        var verb = this.Verb;
        var url = context != null ? await context.RenderAsync(this.Endpoint).ConfigureAwait(false) : this.Endpoint;
        var version = this.Version;
        var headers = this.Headers.Aggregate
        (
            new Dictionary<string, string>(),
            (map, kvp) =>
            {
                var key = context != null ? context.Render(kvp.Key) : kvp.Key;
                var value = context != null ? context.Render(kvp.Value) : kvp.Value;
                map.Add(key, value);
                return map;
            }
        );
        var body = context != null ? await context.RenderAsync(this.Body).ConfigureAwait(false) : this.Body;

        // Create a new FlurlRequest instance with customized configuration
        var request = url
            .WithSettings(settings =>
            {
                settings.HttpVersion = this.Version;
                settings.Timeout = TimeSpan.FromSeconds(10);
                settings.AllowedHttpStatusRange = "*";
                settings.Redirects.Enabled = true;
            })
            .WithHeaders(headers);
        this.Configurator?.Invoke(request);

        // Send request and get proper response asynchronously
        var method = new HttpMethod(verb);
        using var content = body != null ? new StringContent(body) : null;
        return await request.SendAsync(method, content).ConfigureAwait(false);
    }
}
