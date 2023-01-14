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
    }
    /// <summary>
    /// A command with functionality for executing, undoing, and then redoing an action. It also has functionality for describing the action.
    /// </summary>
    /// <typeparam name="TArgs">The type of argument to pass to the execute method</typeparam>
    /// <typeparam name="TSaved">The type of saved data to pass to the undo method</typeparam>
    public abstract class UndoableCommand<TArgs, TSaved> : UndoableCommand
    {
        protected TArgs? Args { get; private set; }
        protected TSaved? Saved { get; private set; }

        protected UndoableCommand(IDescriber description, TArgs? args)
            :base(description)
        {
            Args = args;
            Saved = default;
        }
        public override void Execute()
        {
            Saved = Execute_ReturnSaved();
        }
        protected virtual TSaved? Execute_ReturnSaved() { return default; }
    }
}