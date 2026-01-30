using Fernandezja.ColorHashSharp;
using System;
using Xunit;

namespace ColorHashSharp.Tests
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

        [Fact(DisplayName = "GenerateVersion2_ShouldGenerateHash")]
        public void GenerateVersion2_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var result = hash.GenerateVersion2("test");

            Assert.True(result > 0);
        }

        [Fact(DisplayName = "GenerateVersion2_DifferentStrings_ShouldGenerateDifferentHashes")]
        public void GenerateVersion2_DifferentStrings_ShouldGenerateDifferentHashes()
        {
            var hash = new BKDRHash();
            var result1 = hash.GenerateVersion2("test1");
            var result2 = hash.GenerateVersion2("test2");

            Assert.NotEqual(result1, result2);
        }

        [Fact(DisplayName = "GenerateVersion3_ShouldGenerateHash")]
        public void GenerateVersion3_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var result = hash.GenerateVersion3("test");

            Assert.True(result > 0);
        }

        [Fact(DisplayName = "Generate_EmptyString_ShouldGenerateHash")]
        public void Generate_EmptyString_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("");

            Assert.True(result > 0);
        }

        [Fact(DisplayName = "Generate_SpecialCharacters_ShouldGenerateHash")]
        public void Generate_SpecialCharacters_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("!@#$%^&*()");

            Assert.True(result > 0);
        }

        [Fact(DisplayName = "Generate_UnicodeCharacters_ShouldGenerateHash")]
        public void Generate_UnicodeCharacters_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var result = hash.Generate("你好世界");

            Assert.True(result > 0);
        }

        [Fact(DisplayName = "Generate_LongString_ShouldGenerateHash")]
        public void Generate_LongString_ShouldGenerateHash()
        {
            var hash = new BKDRHash();
            var longString = new string('a', 10000);
            var result = hash.Generate(longString);

            Assert.True(result > 0);
        }
    }
}
