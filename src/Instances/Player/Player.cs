using Godot;

namespace ld47.Instances.Player
{
    public class Player : KinematicBody2D
    {
        public Vector2 Velocity = Vector2.Zero;
        public ActionLock ActionLock;
        public Animation Animation;
        public Gravity Gravity;
        public Run Run;
        public Die Die;
        public Suicide Suicide;
        public AnimatedSprite AnimatedSprite;
        public CollisionShape2D CollisionShape2D;

        public override void _Ready()
        {
            base._Ready();
            ActionLock = GetNode<ActionLock>("Controllers/ActionLock");
            Animation = GetNode<Animation>("Controllers/Animation");
            Gravity = GetNode<Gravity>("Controllers/Gravity");
            Run = GetNode<Run>("Controllers/Run");
            Die = GetNode<Die>("Controllers/Die");
            Suicide = GetNode<Suicide>("Controllers/Suicide");
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Velocity = MoveAndSlide(Velocity, Vector2.Up);
        }
    }
}
