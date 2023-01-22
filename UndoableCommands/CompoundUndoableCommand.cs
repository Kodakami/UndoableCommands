namespace Kodakami.UndoableCommands
{
    public abstract class CompoundUndoableCommand : UndoableCommand
    {
        private readonly UndoStack _internalUndoStack = new();

        protected CompoundUndoableCommand(IDescriber describer) : base(describer) { }

        public override void Execute()
        {
            int redoCount = _internalUndoStack.Count;

            // If we've done this before,
            if (redoCount != 0)
            {
                // Redo the same steps taken before.
                for (int i = 0; i < redoCount; i++)
                {
                    _internalUndoStack.Redo();
                }

                // And that's it.
                return;
            }

            // If we got here, this is the first time we're using this command.
            // If NULL is returned by YieldSubcommands, then the foreach loop just won't do anything.

            foreach (var cmd in YieldSubcommands())
            {
                _internalUndoStack.ExecuteCommand(cmd);
            }
        }

        /// <summary>
        /// Execute the subcommands. Use "yield return" statements.
        /// </summary>
        protected abstract IEnumerable<IUndoableCommand> YieldSubcommands();

        public override void Undo()
        {
            // Undo all the steps taken.
            for (int i = 0; i < _internalUndoStack.Count; i++)
            {
                _internalUndoStack.Undo();
            }
        }

        new public sealed class AdHoc : CompoundUndoableCommand
        {
            private readonly IEnumerable<IUndoableCommand> _deferredSubcommands;

            public AdHoc(IDescriber describer, IEnumerable<IUndoableCommand> subcommands)
                :base(describer)
            {
                // NULL is acceptable because the execution loop just won't happen.
                _deferredSubcommands = subcommands;
            }

            protected override IEnumerable<IUndoableCommand> YieldSubcommands() => _deferredSubcommands;
        }
    }
}