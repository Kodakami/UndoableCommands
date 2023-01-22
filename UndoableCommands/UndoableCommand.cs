namespace Kodakami.UndoableCommands
{
    public abstract class UndoableCommand : IUndoableCommand
    {
        private readonly IDescriber _describer;
        private string? _cachedDescription = null;
        
        // If the description hasn't been cached yet, do so.
        public string Description => _cachedDescription ??= _describer.GetDescription();

        public UndoableCommand(IDescriber description)
        {
            _describer = description ?? throw new ArgumentNullException(nameof(description));
        }
        public abstract void Execute();
        public abstract void Undo();

        public sealed class AdHoc : UndoableCommand
        {
            private readonly Action _executeAction;
            private readonly Action _undoAction;

            public AdHoc(IDescriber description, Action executeAction, Action undoAction) : base(description)
            {
                _executeAction = executeAction;
                _undoAction = undoAction;
            }
            public override void Execute() => _executeAction();
            public override void Undo() => _undoAction();
        }
    }
}