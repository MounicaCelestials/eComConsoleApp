using System;

public class SkuEncoder
{
    public string Encode(int id)
    {
        return SkuConverter.Encode(id);
    }

    public int Decode(string sku)
    {
        return SkuConverter.Decode(sku);
    }
}
