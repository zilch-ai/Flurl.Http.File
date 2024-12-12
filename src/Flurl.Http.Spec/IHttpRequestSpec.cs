namespace Flurl.Http.Spec;

public interface IHttpRequestSpec
{
    string Format { get; }

    string Spec { get; }

    HttpRequestContext Context { get; }

    HttpRequestExecutor this[int index] { get; }

    HttpRequestExecutor this[string key] { get; }
}
