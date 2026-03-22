#pragma warning disable SKEXP0001

using Microsoft.Extensions.VectorData;

namespace Mojo.Domain.AI;

public class DocumentChunk
{
    [VectorStoreKey]
    public string Id { get; set; } = string.Empty;

    [VectorStoreData]
    public string Text { get; set; } = string.Empty;

    [VectorStoreData]
    public string SourceFile { get; set; } = string.Empty;

    [VectorStoreData]
    public string Collection { get; set; } = string.Empty;

    // 768 dimensions = nomic-embed-text
    [VectorStoreVector(768)]
    public ReadOnlyMemory<float> Embedding { get; set; }
}