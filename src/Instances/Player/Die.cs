using Godot;
using ld47.Utils;

namespace ld47.Instances.Player
{
    public class Die : Node2D
    {
        public Player Player;
        public bool Enabled;
        public bool Landed;
        public Vector2 PreviousVelocity;
        
        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            HandleLand();
        }

        public void Enable()
        {
            Player.ActionLock.Lock();
            Enabled = true;
            PreviousVelocity = Player.Velocity;
        }

        private void HandleLand()
        {
            if (!Enabled || Landed) return;
            if (PreviousVelocity == Player.Velocity && !Landed)
            {
                Emitter.Instance.EmitSignal(nameof(Emitter.NewPlayerSignal));
                Landed = true;
            }
            PreviousVelocity = Player.Velocity;
        }
    }
}
