using System.Collections;

namespace Kodakami.UndoableCommands
{
    public class UndoStack : IEnumerable<string>
    {
        private List<IUndoableCommand> _undoStack = new();

        // When pointer = undo stack count, then there are no undo actions to be made.
        private int _undoStackPointer = 0;  // <-- points to the number after the last index (the stack count).

        public int Count => _undoStack.Count;
        public int PointerIndex => _undoStackPointer;

        public void ExecuteCommand(IUndoableCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // If undo stack pointer is before the last index of the list, there have been undo actions made.
            if (_undoStackPointer < _undoStack.Count)
            {
                // Overwrite remaining undo stack. Undo stack becomes the first N actions, where N is the pointer value (convenient!).
                _undoStack = _undoStack.GetRange(0, _undoStackPointer);
            }

            command.Execute();

            _undoStack.Add(command);
            _undoStackPointer++;
        }
        public void Undo()
        {
            // If the undo stack pointer is at 0, then there are no more actions in the undo stack.
            if (_undoStackPointer == 0)
            {
                return;
            }

            // Undo the last action.
            var command = _undoStack[_undoStackPointer - 1];

            command.Undo();
            _undoStackPointer--;
        }
        public void Redo()
        {
            // If undo stack pointer = the stack count, there are no actions to redo. Do nothing.
            if (_undoStackPointer == _undoStack.Count)
            {
                return;
            }

            // Do the 'next'(current) action.
            var command = _undoStack[_undoStackPointer];

            command.Execute();
            _undoStackPointer++;
        }
        public void Clear()
        {
            _undoStack.Clear();
            _undoStackPointer = 0;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _undoStack.Select(c => c.Description).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_undoStack.Select(c => c.Description)).GetEnumerator();
        }
        public IReadOnlyList<string> GetAllDescriptions() => _undoStack.ConvertAll(cmd => cmd.Description);
    }
}