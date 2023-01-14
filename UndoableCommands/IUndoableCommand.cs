namespace Kodakami.UndoableCommands
{
    public interface IUndoableCommand
    {
        string Description { get; }
        void Execute();
        void Undo();
    }
}