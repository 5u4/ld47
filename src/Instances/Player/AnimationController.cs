using Godot;

namespace ld47.Instances.Player
{
    public class AnimationController : Node2D
    {
        public Player Player;
        public AnimatedSprite AnimatedSprite;
        
        public override void _Ready()
        {
            base._Ready();

            Player = GetNode<Player>("../..");
            AnimatedSprite = GetNode<AnimatedSprite>("../../AnimatedSprite");

            AnimatedSprite.Play();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            
            HandleHorizontalFlip();
        }

        private void HandleHorizontalFlip()
        {
            var facing = Mathf.Sign(Player.Velocity.x);
            if (facing != 0) AnimatedSprite.FlipH = facing != 1;
        }
    }
}
