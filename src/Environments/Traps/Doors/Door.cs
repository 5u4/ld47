using Godot;

namespace ld47.Environments.Traps.Doors
{
    public class Door : StaticBody2D
    {
        public AnimatedSprite AnimatedSprite;
        public CollisionShape2D CollisionShape2D;
        
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        }

        public void TurnOn()
        {
            AnimatedSprite.Play("TurnOn");
            CallDeferred(nameof(Turn), true);
        }

        public void TurnOff()
        {
            AnimatedSprite.Play("TurnOff");
            CallDeferred(nameof(Turn), false);
        }

        private void Turn(bool onoff)
        {
            CollisionShape2D.Disabled = onoff;
        }
    }
}
