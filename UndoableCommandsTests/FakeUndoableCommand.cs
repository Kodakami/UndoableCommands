namespace Kodakami.UndoableCommands.Tests
{
    public class FakeUndoableCommand : UndoableCommand<(List<int> context, int data), object>
    {
        public FakeUndoableCommand(string descriptionSuffix, (List<int> context, int data) args)
            :base(new StaticDescriber("FakeUndoableCommand(" + descriptionSuffix + ")"), args)
        { }

        public override void Execute()
        {
            Args.context.Add(Args.data);
        }

        public override void Undo()
        {
            Args.context.RemoveAt(Args.context.Count - 1);
        }
    }
}