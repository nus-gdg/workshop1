using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public interface ILogic
    {
        void Init(IModel model, IStorage storage);
        CommandResult Execute(Command command);
    }
}
