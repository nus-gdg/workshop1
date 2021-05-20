using Project.Models;

namespace Project.Controllers
{
    public abstract class Command
    {
        public abstract string Type { get; }
        public abstract string Info { get; }

        public abstract CommandResult Execute(IModel model);

        public override string ToString()
        {
            return $"[{Type}]\n\n{Info}";
        }
    }
}
