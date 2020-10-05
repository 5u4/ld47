using Godot;

namespace ld47.Instances.Player
{
    public class Suicide : Node2D
    {
        public Player Player;
        public bool Enabled;

        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }

        public override void _Process(float delta)
        {
            if (Player.ActionLock.IsLocked) return;
            base._Process(delta);
            HandleSuicide();
        }
        
        private void HandleSuicide()
        {
            if (!Input.IsActionJustPressed("ui_suicide") || !Enabled) return;
            Player.Die.Enable();
        }
    }
}
