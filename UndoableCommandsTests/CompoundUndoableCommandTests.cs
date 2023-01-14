namespace Kodakami.UndoableCommands.Tests
{
    [TestClass]
    public class CompoundUndoableCommandTests
    {
        [TestMethod]
        public void Execute_ExecutesCorrectly()
        {
            var dummyContext = new List<int>();
            var fullSequence = new List<int>() { 1, 2, 3 };
            var mock = new FakeCompoundUndoableCommand(dummyContext);

            mock.Execute();

            CollectionAssert.AreEqual(fullSequence, dummyContext);
        }

        [TestMethod]
        public void Undo_ExecutesCorrectly()
        {
            var dummyContext = new List<int>();
            var fullSequence = new List<int>() { 1, 2, 3 };
            var emptySequence = new List<int>();
            var mock = new FakeCompoundUndoableCommand(dummyContext);

            mock.Execute();

            CollectionAssert.AreEqual(fullSequence, dummyContext);

            mock.Undo();
            
            CollectionAssert.AreEqual(emptySequence, dummyContext);
        }
    }
}