using Microsoft.AspNetCore.Components.Forms;

namespace MineralCollection.Tests.Unit.TestDoubles;

internal sealed class FakeBrowserFile : IBrowserFile
{
    private readonly byte[] _content;

    public FakeBrowserFile(string name, string contentType, byte[] content)
    {
        Name = name;
        ContentType = contentType;
        _content = content;
    }

    public string Name { get; }

    public DateTimeOffset LastModified { get; } = DateTimeOffset.UtcNow;

    public long Size => _content.Length;

    public string ContentType { get; }

    public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
    {
        if (_content.Length > maxAllowedSize)
        {
            throw new IOException("File size exceeds the allowed test stream size.");
        }

        return new MemoryStream(_content);
    }
}
