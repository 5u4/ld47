using Godot;

namespace ld47.Environments.Corpse
{
    public class Corpse : KinematicBody2D
    {
        public Vector2 Velocity = Vector2.Zero;
        public AnimatedSprite AnimatedSprite;
        public const float GravityScale = 300;
        
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            AnimatedSprite.Connect("animation_finished", this, nameof(OnAnimationFinished));
            
            AnimatedSprite.Play();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Velocity = MoveAndSlide(Velocity, Vector2.Up);
            HandleFalling(delta);
        }

        private void HandleFalling(float delta)
        {
            Velocity.y += delta * GravityScale;
        }

        private void OnAnimationFinished()
        {
            AnimatedSprite.Stop();
            AnimatedSprite.Frame = AnimatedSprite.Frames.GetFrameCount("Hit") - 1;
        }
    }
}
