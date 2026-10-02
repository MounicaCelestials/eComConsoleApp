using System;
using Xunit;

namespace eComConsoleApp.Tests
{
    public class SkuEncoderTests
    {
        private readonly SkuEncoder _encoder = new SkuEncoder();

        [Fact]
        public void Encode_ValidId_ReturnsCorrectSku()
        {
            Assert.Equal("A", _encoder.Encode(1));
            Assert.Equal("Z", _encoder.Encode(26));
            Assert.Equal("AA", _encoder.Encode(27));
            Assert.Equal("AZ", _encoder.Encode(52));
            Assert.Equal("BA", _encoder.Encode(53));
            Assert.Equal("AAA", _encoder.Encode(703));
        }

        [Fact]
        public void Decode_ValidSku_ReturnsCorrectId()
        {
            Assert.Equal(1, _encoder.Decode("A"));
            Assert.Equal(26, _encoder.Decode("Z"));
            Assert.Equal(27, _encoder.Decode("AA"));
            Assert.Equal(52, _encoder.Decode("AZ"));
            Assert.Equal(53, _encoder.Decode("BA"));
            Assert.Equal(703, _encoder.Decode("AAA"));
        }

        [Fact]
        public void Encode_InvalidId_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _encoder.Encode(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => _encoder.Encode(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _encoder.Encode(1_000_001));
        }

        [Fact]
        public void Decode_InvalidSku_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _encoder.Decode(null!));
            Assert.Throws<ArgumentException>(() => _encoder.Decode(""));
            Assert.Throws<ArgumentException>(() => _encoder.Decode(" "));
            Assert.Throws<ArgumentException>(() => _encoder.Decode("a"));
            Assert.Throws<ArgumentException>(() => _encoder.Decode("A1"));
            Assert.Throws<ArgumentException>(() => _encoder.Decode("AA!"));
        }

        [Fact]
        public void RoundTrip_EncodeDecode_ReturnsOriginalId()
        {
            for (int id = 1; id <= 1000; id++)
            {
                string sku = _encoder.Encode(id);
                int decodedId = _encoder.Decode(sku);
                Assert.Equal(id, decodedId);
            }
        }
    }
}
