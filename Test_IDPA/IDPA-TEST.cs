using IDPA_Vorprojekt_2024;
using IDPA_Vorprojekt_2024.Classes;
namespace Test_IDPA
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculateAvailableWin()
        {
            //Arrange
            double expected = 35920;
            double result;
            UserValues userValues = new UserValues("35600", "300000", "41200", "2100", "4");
            ValueCalculator valueCalculator = new ValueCalculator(userValues);

            //Act
            result = valueCalculator.CalculateAvailableWin();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestCalculateFinancialStatement()
        {
            //Arrange
            double expected = 37700;
            double result;
            UserValues userValues = new UserValues("35600", "300000", "41200", "2100", "4");
            ValueCalculator valueCalculator = new ValueCalculator(userValues);

            //Act
            result = valueCalculator.CalculateFinancialStatement();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestCalculateAvailableRestForDividend()
        {
            //Arrange
            double expected = 20920;
            double result;
            UserValues userValues = new UserValues("35600", "300000", "41200", "2100", "4");
            ValueCalculator valueCalculator = new ValueCalculator(userValues);

            //Act
            result = valueCalculator.CalculateAvailableRestForDividend();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestCalculateNewProfitCarriedForward()
        {
            //Arrange
            double expected = 7720;
            double result;
            UserValues userValues = new UserValues("35600", "300000", "41200", "2100", "4");
            ValueCalculator valueCalculator = new ValueCalculator(userValues);

            //Act
            result = valueCalculator.CalculateNewProfitCarriedForward();

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}