using System.Security.Principal;
using ATMSim;

namespace ATMSimTests
{
    public class LINQPinTests
    {
        public Account Account { get; set; }

        [SetUp]
        public void Setup()
        {
            Account = new Account()
            {
                Balance = 0,
                Name = "Test",
                Pin = 0000
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
