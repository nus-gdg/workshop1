namespace DefaultNamespace
{
    public class ScriptDemo : BaseDemo
    {
        private string[] names =
        {
            "Gabe",
            "Ernest",
            "Charlie",
            "Dave",
            "Eve",
            "Faythe",
            "Greg",
            "Heidi",
            "Ivan",
            "Judy",
        };

        private string message = "Hope you all like this tutorial!";


        public override string GetName(int i)
        {
            return names[i];
        }

        public override string GetMessage()
        {
            return message;
        }
    }
}
