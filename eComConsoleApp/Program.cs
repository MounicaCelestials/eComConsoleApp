using System;

public static class SkuConverter
{
    private const int MinId = 1;
    private const int MaxId = 1_000_000;

    public static string Encode(int id)
    {
        if (id < MinId || id > MaxId)
            throw new ArgumentOutOfRangeException(nameof(id), $"ID must be between {MinId} and {MaxId}.");

        string result = string.Empty;
        while (id > 0)
        {
            id--; // shift to 0-based
            int remainder = id % 26;
            result = (char)('A' + remainder) + result;
            id /= 26;
        }
        return result;
    }

    public static int Decode(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU cannot be null, empty, or whitespace.");

        int value = 0;
        foreach (char c in sku)
        {
            if (c < 'A' || c > 'Z')
                throw new ArgumentException("SKU must contain only uppercase A-Z letters.");

            value = value * 26 + (c - 'A' + 1);
        }

        if (value < MinId || value > MaxId)
            throw new ArgumentOutOfRangeException(nameof(sku), $"Decoded ID must be between {MinId} and {MaxId}.");

        return value;
    }

    static void Main(string[] args)
    {
        // Example usage
        int id = 123455;
        string sku = Encode(id);
        Console.WriteLine($"Encoded ID {id} to SKU: {sku}");
        int decodedId = Decode(sku);
        Console.WriteLine($"Decoded SKU {sku} back to ID: {decodedId}");
    }
}
