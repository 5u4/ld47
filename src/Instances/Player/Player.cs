using Godot;

namespace ld47.Instances.Player
{
    public class Player : KinematicBody2D
    {
        public Vector2 Velocity = Vector2.Zero;
        public AnimationController AnimationController;

        public override void _Ready()
        {
            base._Ready();
            AnimationController = GetNode<AnimationController>("Controllers/AnimationController");
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Velocity = MoveAndSlide(Velocity, Vector2.Up);
        }
    }
}
