namespace Kodakami.UndoableCommands.Tests
{
    public class FakeCompoundUndoableCommand : CompoundUndoableCommand<List<int>, object?>
    {
        public FakeCompoundUndoableCommand(List<int> context) : base(new StaticDescriber("FakeCompoundUndoableCommand"), context)
        {
        }

        protected override IEnumerable<IUndoableCommand> YieldSubcommands()
        {
            yield return new FakeUndoableCommand("1", (Args!, 1));
            yield return new FakeUndoableCommand("2", (Args!, 2));
            yield return new FakeUndoableCommand("3", (Args!, 3));
        }
    }
}