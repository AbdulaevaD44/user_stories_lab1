namespace GardenApp.BusinessLogic.Commands
{
    public interface ICommand
    {
        void Execute();
        string Description { get; }
    }
}