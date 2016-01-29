namespace Battleships.Logic
{
    public interface IUserInterface
    {
        string GetUserInput();
        void RenderMessage(string message);
    }
}