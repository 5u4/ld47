using Godot;

namespace ld47.Environments.Traps.Doors
{
    public class Door : StaticBody2D
    {
        [Export] public NodePath ConnectedDoorPath;
        public AnimatedSprite AnimatedSprite;
        public CollisionShape2D CollisionShape2D;
        public Door ConnectedDoor;
        
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
            if (ConnectedDoorPath != null) ConnectedDoor = GetNode<Door>(ConnectedDoorPath);
        }

        public void TurnOn()
        {
            AnimatedSprite.Play("TurnOn");
            CallDeferred(nameof(Turn), true);
            ConnectedDoor?.TurnOn();
        }

        public void TurnOff()
        {
            AnimatedSprite.Play("TurnOff");
            CallDeferred(nameof(Turn), false);
            ConnectedDoor?.TurnOff();
        }

        private void Turn(bool onoff)
        {
            CollisionShape2D.Disabled = onoff;
        }
    }
}
