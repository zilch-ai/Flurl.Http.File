namespace Flurl.Http.Spec;

using Parlot;
using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

public static class HttpRequestStatements
{
    public static readonly Parser<(string, string)> Variable =
        ZeroOrMany(HttpRequestTokens.Comment)
        .SkipAnd(Literals.Char('@'))
        .SkipAnd(Terms.Identifier())
        .AndSkip(ZeroOrOne(Terms.Char('=')))
        .And(AnyCharBefore(HttpRequestTokens.NewLine, canBeEmpty: false, failOnEof: false))
        .AndSkip(ZeroOrOne(HttpRequestTokens.NewLine))
        .Then<(string, string)>(result => new
        (
            result.Item1.ToString() ?? string.Empty,
            result.Item2.ToString()?.Trim() ?? string.Empty
        ));

    public static readonly Parser<(string, string, string)> Command =
        ZeroOrMany(HttpRequestTokens.Comment)
        .SkipAnd(HttpRequestTokens.Verb)
        .And(HttpRequestTokens.Endpoint)
        .And(ZeroOrOne(HttpRequestTokens.Version));

    public static readonly Parser<string> RequestTag =
        HttpRequestTokens.NewLine
        .SkipAnd(Literals.Text("###", caseInsensitive: false))
        .SkipAnd
        (
            ZeroOrOne
            (
                AnyCharBefore
                (
                    HttpRequestTokens.NewLine,
                    canBeEmpty: false,
                    failOnEof: false
                )
                .Then(result => result.ToString()?.Trim() ?? string.Empty)
            )
        );

    // See in Section 5 in [RFC 9110](https://datatracker.ietf.org/doc/html/rfc9110#name-fields)
    // > field-name     = token
    // > token          = 1*tchar
    // > tchar          = "!" / "#" / "$" / "%" / "&" / "'" / "*"
    // >                / "+" / "-" / "." / "^" / "_" / "`" / "|" / "~"
    // >                / DIGIT / ALPHA
    // >                ; any VCHAR, except delimiters
    //
    // See in Section 3.1.2 in [RFC 822](https://datatracker.ietf.org/doc/html/rfc822).
    // - Once a field has been unfolded, it may be viewed as being composed of a field-name followed by a colon(":"), followed by a field-body, and terminated  by a carriage-return/line-feed.
    // - The field-name must be composed of printable ASCII characters (i.e., characters that have values between 33 and 126 in decimal, except colon).
    // - The field-body may be composed of any ASCII characters, except CR or LF. (While CR and/or LF may be present in the actual text, they are removed by the action of unfolding the field.)
    //
    // See in Section 3.2 in [RFC 7230](https://datatracker.ietf.org/doc/html/rfc7230).
    // > header-field   = field-name ":" OWS field-value OWS
    // > field-name     = token
    // > field-value    = *(field-content / obs-fold )
    // > field-content  = field-vchar[1 * (SP / HTAB) field - vchar]
    // > field-vchar    = VCHAR / obs-text
    // > obs-fold       = CRLF 1*(SP / HTAB )
    // >                ; obsolete line folding
    // >                ; see Section 3.2.4
    // > OWS            = *( SP / HTAB )
    // >                ; optional whitespace
    public static readonly Parser<(TextSpan, TextSpan)> Header =
        Literals.Pattern(c => c is >= '!' and <= '~' and not ':')
        .AndSkip(Literals.Char(':'))
        .And(AnyCharBefore(HttpRequestTokens.NewLine, failOnEof: false, canBeEmpty: false));

    public static readonly Parser<IReadOnlyList<(TextSpan, TextSpan)>> Headers =
        ZeroOrMany
        (
            ZeroOrMany(HttpRequestTokens.Comment)
            .SkipAnd(Header!)
        );

    public static readonly Parser<string> Body =
        HttpRequestTokens.NewLine
        .SkipAnd
        (
            AnyCharBefore
            (
                HttpRequestTokens.NewLine.And(Literals.Text("###")),
                failOnEof: false,
                canBeEmpty: true
            )
        )
        .Then(result => result.ToString()?.Trim() ?? string.Empty);
}
