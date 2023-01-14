namespace Kodakami.UndoableCommands.Tests
{
    [TestClass]
    public class DescriberTests
    {
        [TestClass]
        public class StaticDescriberTests
        {
            // It's just a wrapper for a string - not much can go wrong.

            [DataRow(null, DisplayName = "Null")]
            [DataRow("", DisplayName = "Empty String")]
            [DataRow("a", DisplayName = "Non-empty String")]
            [TestMethod]
            public void Ctor_ReturnsCorrectResult(string? str)
            {
                var mock = new StaticDescriber(str);

                Assert.IsNotNull(mock);
            }
            [TestMethod]
            public void GetDescription_ResturnsCorrectResult()
            {
                string dummyStr = "a";
                var mock = new StaticDescriber(dummyStr);

                var result = mock.GetDescription();

                Assert.AreEqual(dummyStr, result);
            }
        }
        [TestClass]
        public class DynamicDescriberTests
        {
            [DataRow(null, null, null, DisplayName = "Null")]
            [TestMethod]
            public void Ctor_ReturnsCorrectResult(string? whenSuccessful, string? whenFailure, Func<bool>? checkForSuccess)
            {
                var mock = new DynamicDescriber(whenSuccessful, whenFailure, checkForSuccess);

                Assert.IsNotNull(mock);
            }
            [DataRow("a", "b", true, "a", DisplayName = "With Always True Func")]
            [DataRow("a", "b", false, "b", DisplayName = "With Always False Func")]
            [TestMethod]
            public void GetDescription_ReturnsCorrectResult(string whenSuccessful, string whenFailure, bool funcResult, string expected)
            {
                static bool checkForSuccess(bool funcResult) => funcResult;
                
                var mock = new DynamicDescriber(whenSuccessful, whenFailure, () => checkForSuccess(funcResult));

                var result = mock.GetDescription();

                Assert.AreEqual(expected, result);
            }
            [TestMethod]
            public void GetDescription_WithNullFuncArgInCtor_ReturnsAsIfTrue()
            {
                string whenSuccessful = "a";
                string whenFailure = "b";
                string expected = whenSuccessful;

                var mock = new DynamicDescriber(whenSuccessful, whenFailure, null);

                var result = mock.GetDescription();

                Assert.AreEqual(expected, result);
            }
        }
    }
}