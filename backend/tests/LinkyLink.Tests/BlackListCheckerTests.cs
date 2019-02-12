using System;
using System.Threading.Tasks;
using LinkyLink.Infrastructure;
using LinkyLink.Tests.Helpers;
using Xunit;

namespace LinkyLink.Tests
{
    public class EnvironmentBlackListCheckerTests : TestBase
    {
        
        [Fact]
        public async Task Check_Returns_False_On_Empty_Key() {
            EnvironmentBlackListChecker checker =  new EnvironmentBlackListChecker(string.Empty);
            Assert.True(await checker.Check("somevalue"));
        }

        [Fact]
        public async Task Check_Throws_Exception_On_Empty_BlackList_Value()
        {
            // Arrange
            Environment.SetEnvironmentVariable("key", "value");
            string key = "key";
            EnvironmentBlackListChecker checker = new EnvironmentBlackListChecker(key);

            // Act
             await Assert.ThrowsAsync<ArgumentNullException>(() => checker.Check(string.Empty));
        }

        [Fact]
        public async Task Check_Compares_Input_To_Blacklist()
        {
            // Arrange
            Environment.SetEnvironmentVariable("key", "1,2,3,4,5,6");
            string key = "key";
            EnvironmentBlackListChecker checker = new EnvironmentBlackListChecker(key);

            // Act
            bool result_1 = await checker.Check("1");
            bool result_2 = await checker.Check("10");

            // Assert
            Assert.True(result_1);
            Assert.True(!result_2);
        }
    }
}