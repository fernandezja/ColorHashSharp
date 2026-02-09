# Custom Hash Function Example

This example demonstrates how to use a custom hash function with ColorHashSharp.

## Basic Usage with Custom Hash Function

```csharp
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Interfaces;

// 1. Create a custom hash function by implementing IHashFunction
public class SimpleCharCodeHash : IHashFunction
{
    public ulong Generate(string value)
    {
        if (string.IsNullOrEmpty(value))
            return 0;

        ulong hash = 0;
        foreach (char c in value)
        {
            hash += (ulong)c;
        }
        return hash;
    }
}

// 2. Use the custom hash function
var customHash = new SimpleCharCodeHash();
var options = new Options
{
    HashFunction = customHash
};

var colorHash = new ColorHash(options);

// Generate colors using your custom hash
var hsl = colorHash.Hsl("Hello World");
var rgb = colorHash.Rgb("Hello World");
var hex = colorHash.Hex("Hello World");

Console.WriteLine($"HSL: H={hsl.H}, S={hsl.S}, L={hsl.L}");
Console.WriteLine($"RGB: R={rgb.R}, G={rgb.G}, B={rgb.B}");
Console.WriteLine($"Hex: {hex}");
```

## Advanced Example: MD5-based Hash Function

```csharp
using System.Security.Cryptography;
using System.Text;

public class MD5HashFunction : IHashFunction
{
    public ulong Generate(string value)
    {
        using (var md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            
            // Convert first 8 bytes to ulong
            ulong hash = 0;
            for (int i = 0; i < 8 && i < hashBytes.Length; i++)
            {
                hash |= ((ulong)hashBytes[i]) << (i * 8);
            }
            return hash;
        }
    }
}

// Use MD5-based hash
var options = new Options
{
    HashFunction = new MD5HashFunction(),
    Lightness = new List<double> { 0.5 },
    Saturation = new List<double> { 0.7 }
};

var colorHash = new ColorHash(options);
var color = colorHash.Hex("SecureString");
```

## Why Use Custom Hash Functions?

- **Consistency across platforms**: Use a specific hash that behaves identically everywhere
- **Security considerations**: Use cryptographic hashes for sensitive data
- **Domain-specific needs**: Implement hash functions optimized for your data patterns
- **Testing**: Create predictable hash functions for unit tests

## Default Behavior

If no custom hash function is specified, ColorHashSharp uses the default BKDRHash algorithm:

```csharp
// These are equivalent:
var colorHash1 = new ColorHash();
var colorHash2 = new ColorHash(new Options()); // Uses default BKDRHash
```
