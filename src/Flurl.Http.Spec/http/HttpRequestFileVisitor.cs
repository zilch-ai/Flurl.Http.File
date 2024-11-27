namespace Flurl.Http.Spec;

using System.Collections.Immutable;
using AntlrCodeGen;
using Builder = Flurl.Http.Spec.HttpRequestExecutors.Builder;

internal sealed class HttpRequestFileVisitor(Builder builder)
    : HttpRequestFileBaseVisitor<Builder>
{
    private readonly Builder builder = builder;

    public override Builder VisitAssignment(HttpRequestFileParser.AssignmentContext context)
    {
        var variable = context.variable().GetText();
        var value = context.value().GetText();
        return this.builder.AddVariable(variable, value);
    }

    public override Builder VisitFirst(HttpRequestFileParser.FirstContext context)
    {
        _ = context.ThrowIfNull();
        _ = this.builder.AddExecutor(c => ToExecutor(c, context.request()), string.Empty);
        return this.builder;
    }

    public override Builder VisitMore(HttpRequestFileParser.MoreContext context)
    {
        _ = context.ThrowIfNull();
        var id = context.requestId()?.GetText() ?? string.Empty;
        _ = this.builder.AddExecutor(c => ToExecutor(c, context.request()), id);
        return this.builder;
    }

    private static HttpRequestExecutor ToExecutor(IReadOnlyDictionary<string, string> context, HttpRequestFileParser.RequestContext request)
        => new(request.verb().GetText(), request.url().GetText())
        {
            Version = request.version()?.GetText() ?? "HTTP/1.1",
            Headers = request.header()
                ?.Where(h => !string.IsNullOrEmpty(h.headerKey().GetText()))
                ?.Select(h => new KeyValuePair<string, string>(h.headerKey().GetText(), h.headerValue().GetText()))
                ?.ToImmutableDictionary()
                ?? ImmutableDictionary<string, string>.Empty,
            Body = request.body()?.GetText(),
        };
}
