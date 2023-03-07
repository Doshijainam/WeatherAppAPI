
namespace APITestCases
{
    [TestClass]
    public class UnitTest1 : Form1 
    {
        [TestMethod]
        public void TestMethod1()
        {
            string expected = "J";
            string actual = "J";
            Assert.AreEqual(expected, actual);
            
        }
    }
}