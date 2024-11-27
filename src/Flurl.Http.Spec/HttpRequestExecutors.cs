namespace Flurl.Http.Spec;

using System.Collections.Immutable;
using HttpRequestContext = System.Collections.Immutable.ImmutableDictionary<string, string>;
using HttpRequestExecutorList = System.Collections.Immutable.ImmutableList<HttpRequestExecutor>;
using HttpRequestExecutorIndexes = System.Collections.Immutable.ImmutableDictionary<string, int>;

public class HttpRequestExecutors(HttpRequestContext? context = null, HttpRequestExecutorList? list = null, HttpRequestExecutorIndexes? indexes = null)
{
    private readonly HttpRequestExecutorList list = list ?? HttpRequestExecutorList.Empty;

    private readonly HttpRequestExecutorIndexes indexes = indexes ?? HttpRequestExecutorIndexes.Empty;

    public HttpRequestContext Context => context ?? HttpRequestContext.Empty;

    /// <summary>
    /// Gets the <see cref="HttpRequestExecutor"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the request builder to invoke.</param>
    /// <returns>The <see cref="HttpRequestExecutor"/> created by the request builder at the specified index.</returns>
    public HttpRequestExecutor this[int index] => this.list[index];

    /// <summary>
    /// Gets the <see cref="HttpRequestExecutor"/> associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the request builder to invoke.</param>
    /// <returns>The <see cref="HttpRequestExecutor"/> created by the request builder associated with the specified key.</returns>
    public HttpRequestExecutor this[string key] => this.list[this.indexes[key]];

    /// <summary>
    /// Creates a builder for efficient batch operations.
    /// </summary>
    /// <returns>A new instance of the <see cref="Builder"/> class.</returns>
    public static Builder CreateBuilder() => new();

    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Nested public builder class for the immutable collection.")]
    public class Builder()
    {
        private readonly Dictionary<string, string> context = new();
        private readonly List<HttpRequestExecutor> list = new();
        private readonly Dictionary<string, int> indexes = new();

        public Builder AddVariable(string id, string value)
        {
            this.context[id.ThrowIfNull()] = value;
            return this;
        }

        public Builder AddExecutor(Func<IReadOnlyDictionary<string, string>, HttpRequestExecutor> builder, string? key = null)
        {
            _ = builder.ThrowIfNull();
            _ = key?.Throw(paramName => throw new ArgumentOutOfRangeException(paramName, "The key already exists in the indexes."))
                .IfTrue(this.indexes.ContainsKey);

            var index = this.list.Count;
            var executor = builder.Invoke(this.context);
            this.list.Add(executor);
            if (key != null)
            {
                this.indexes[key] = index;
            }

            return this;
        }

        public HttpRequestExecutors Build() => new(
            this.context.ToImmutableDictionary(),
            this.list.ToImmutableList(),
            this.indexes.ToImmutableDictionary());
    }
}
