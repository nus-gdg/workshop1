namespace Project.Controllers
{
    public class CommandResult
    {
        public string Message { get; private set; }

        public CommandResult(string message = "")
        {
            Message = message;
        }
    }
}
