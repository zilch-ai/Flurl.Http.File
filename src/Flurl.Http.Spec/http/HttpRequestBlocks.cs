namespace Flurl.Http.Spec;

using System.Collections.Immutable;
using Fluid;
using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

public static class HttpRequestBlocks
{
    public static readonly Parser<HttpRequestContext> Variables =
        ZeroOrMany(HttpRequestStatements.Variable)
        .Then
        (
            results => results.Aggregate
            (
                new HttpRequestContext(),
                (c, kvp) => c = c.AddVariable(name: kvp.Item1, value: kvp.Item2)
            )
        );

    public static readonly Parser<HttpRequestExecutor> Request =
        HttpRequestStatements.Command
        .And(HttpRequestStatements.Headers)
        .And(HttpRequestStatements.Body)
        .Then(result =>
        {
            var verb = result.Item1.Item1;
            var endpoint = result.Item1.Item2;
            var version = result.Item1.Item3;
            var headers = result.Item2.ToImmutableDictionary
            (
                kvp => HttpRequestContext.Template.Parse(kvp!.Item1.ToString() ?? string.Empty),
                kvp => HttpRequestContext.Template.Parse(kvp!.Item2.ToString()?.Trim() ?? string.Empty)
            );
            var body = HttpRequestContext.Template.Parse(result.Item3.ToString() ?? string.Empty);
            return new HttpRequestExecutor(verb, endpoint)
            {
                Version = version ?? "1.1",
                Headers = headers,
                Body = body,
            };
        });

    public static readonly Parser<HttpRequestExecutors> Requests =
        ZeroOrOne(Request!)
        .And
        (
            ZeroOrMany
            (
                HttpRequestTokens.NewLine
                .SkipAnd(HttpRequestStatements.RequestTag!)
                .And(Request)
            )
        )
        .Then(result =>
        {
            var builder = new HttpRequestExecutors.Builder();

            var firstRequest = result.Item1;
            if (firstRequest != null)
            {
                _ = builder.AddExecutor(firstRequest);
            }

            var moreRequests = result.Item2;
            foreach (var (moreRequestId, moreRequest) in moreRequests)
            {
                _ = builder.AddExecutor(moreRequest, moreRequestId.ToString() ?? string.Empty);
            }

            return builder.Build();
        });

    public static readonly Parser<(HttpRequestContext, HttpRequestExecutors)> All =
        HttpRequestBlocks.Variables
        .And(HttpRequestBlocks.Requests)
        .Compile();
}
