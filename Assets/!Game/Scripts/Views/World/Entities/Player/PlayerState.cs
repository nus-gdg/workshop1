namespace Project.Views.World.Entities
{
    public abstract class PlayerState
    {
        public string name;

        public virtual void Enter(PlayerUi player)
        {

        }

        public virtual void Update(PlayerUi player)
        {

        }
        
        public virtual void Exit(PlayerUi player)
        {
            
        }
    }
}
