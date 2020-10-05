using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class Game1Ending : Area2D
    {
        public AnimatedSprite AnimatedSprite;
        
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            AnimatedSprite.Play();
            Connect("body_entered", this, nameof(OnBodyEntered));
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player)) return;
            Emitter.Instance.EmitSignal(nameof(Emitter.Finish1Game));
            ((Game) GetTree().CurrentScene).Spawn.GlobalPosition = GlobalPosition;
            QueueFree();
        }
    }
}
