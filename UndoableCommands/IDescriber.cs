namespace Kodakami.UndoableCommands
{
    public interface IDescriber
    {
        string GetDescription();
    }
    public sealed class StaticDescriber : IDescriber
    {
        private readonly string _description;

        public StaticDescriber(string description)
        {
            _description = description ?? "";
        }
        public string GetDescription() => _description;
    }
    public sealed class DynamicDescriber : IDescriber
    {
        private readonly string _whenSuccessful;
        private readonly string _whenFailure;
        private readonly Func<bool> _checkForSuccess;

        public DynamicDescriber(string whenSuccessful, string whenFailure, Func<bool>? checkForSuccess = null)
        {
            _whenSuccessful = whenSuccessful ?? "";
            _whenFailure = whenFailure ?? "";
            _checkForSuccess = checkForSuccess ?? (() => true);
        }
        public DynamicDescriber(string whenSuccessful, Func<bool>? checkForSuccess = null)
            : this(whenSuccessful, "No effect.", checkForSuccess) { }
        public string GetDescription() => _checkForSuccess() ? _whenSuccessful : _whenFailure;
    }
}