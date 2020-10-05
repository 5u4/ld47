using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class Game2Ending : Area2D
    {
        public AnimatedSprite AnimatedSprite;
        public CollisionShape2D CollisionShape2D;
    
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
            AnimatedSprite.Play();
            Connect("body_entered", this, nameof(OnBodyEntered));
            Emitter.Instance.Connect(nameof(Emitter.Finish1Game), this, nameof(OnFinish1Game));
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player)) return;
            Emitter.Instance.EmitSignal(nameof(Emitter.Finish2Game));
            ((Game) GetTree().CurrentScene).Spawn.GlobalPosition = GlobalPosition;
            QueueFree();
        }
        
        private void OnFinish1Game()
        {
            Show();
            CollisionShape2D.Disabled = false;
        }
    }
}
