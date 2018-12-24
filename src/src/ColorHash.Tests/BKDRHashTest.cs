using Fernandezja.ColorHash;
using System;
using Xunit;

namespace ColorHash.Tests
{
    public class BKDRHashTest
    {
        private const char SPACE_CHAR = ' ';

        [Fact(DisplayName = "Generate_HashWithYodaString")]
        public void GenerateHashWithYodaString()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("Yoda");

            Assert.True(result > 0);
            Assert.Equal<ulong>(26461759997, result);
        }

        [Fact(DisplayName = "Generate_HashWithHelloWorldString")]
        public void GenerateHashWithHelloWorldString()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("Hello World");
            ulong expected = ulong.Parse("294020464607729");

            Assert.True(result > 0);
            Assert.Equal<ulong>(expected, result);
        }
        
        [Fact(DisplayName = "Generate_HashWithOneOnlyChar")]
        public void GenerateHashWithOneOnlyChar()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("y");

            Assert.True(result > 0);
            Assert.Equal<ulong>(15971, result);
        }

        [Theory(DisplayName = "Generate_HashWithSpaces")]
        [InlineData(1, 4312)]
        [InlineData(2, 553464)]
        [InlineData(100, 331584897437617)]
        [InlineData(250, 7527402707245979)]
        [InlineData(1000, 128829383441439)]
        public void Generate_HashWithSpaces(int spacesLength, ulong hashExpected)
        {
            var stringToHash = new string(SPACE_CHAR, spacesLength);

            var hash = new BKDRHash();
            var result = hash.Generate(stringToHash);

            Assert.Equal<int>(spacesLength, stringToHash.Length);
            Assert.True(result > 0);
            Assert.Equal<ulong>(hashExpected, result);
        }
    }
}
