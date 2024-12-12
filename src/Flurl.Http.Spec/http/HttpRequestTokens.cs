namespace Flurl.Http.Spec;

using Parlot;
using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

public static class HttpRequestTokens
{
    public static readonly Parser<TextSpan> NewLine =
        Capture
        (
            ZeroOrOne(Literals.Char('\r'))
            .And(Literals.Char('\n'))
        );

    public static readonly Parser<TextSpan> Comment =
        OneOf
        (
            Literals.WhiteSpace(includeNewLines: true),
            Literals.Text(@"//", caseInsensitive: false).SkipAnd(AnyCharBefore(HttpRequestTokens.NewLine, canBeEmpty: true, failOnEof: false)).AndSkip(ZeroOrOne(HttpRequestTokens.NewLine)),
            Not(Literals.Text("###")).SkipAnd(Literals.Text("#")).SkipAnd(AnyCharBefore(HttpRequestTokens.NewLine, canBeEmpty: true, failOnEof: false)).AndSkip(ZeroOrOne(HttpRequestTokens.NewLine))
        );

    public static readonly Parser<string> Verb =
        OneOf
        (
            Literals.Text("GET", caseInsensitive: false),
            Literals.Text(@"POST", caseInsensitive: false),
            Literals.Text(@"PUT", caseInsensitive: false),
            Literals.Text(@"PATCH", caseInsensitive: false),
            Literals.Text(@"DELETE", caseInsensitive: false)
        )
        .AndSkip(Literals.WhiteSpace(includeNewLines: true));

    public static readonly Parser<string> Endpoint =
        Capture
        (
            OneOf
            (
                Terms.Text(@"http://", caseInsensitive: true),
                Terms.Text(@"https://", caseInsensitive: true)
            )
            .And(Literals.NonWhiteSpace(includeNewLines: true))
        )
        .Then(result => result.ToString() ?? string.Empty);

    public static readonly Parser<string> Version =
        Terms.Text(@"HTTP/", caseInsensitive: false)
        .SkipAnd
        (
            OneOf
            (
                Capture
                (
                    Literals.Integer()
                    .And
                    (
                        Literals.Char('.')
                        .SkipAnd(Literals.Integer())
                    )
                ).Then(span => span.ToString() ?? string.Empty),
                Literals.Integer().Then(major => $"{major}.0")
            )
        );
}
