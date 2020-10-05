using Godot;

namespace ld47.Instances.Player
{
    public class Animation : Node2D
    {
        public Player Player;
        public AnimatedSprite AnimatedSprite;
        public AnimationTree AnimationTree;
        public AnimationNodeStateMachinePlayback StateMachine;
        public const float RunningAnimThreshold = 10f;
        public bool Fade;

        public override void _Ready()
        {
            base._Ready();

            Player = GetNode<Player>("../..");
            AnimatedSprite = GetNode<AnimatedSprite>("../../AnimatedSprite");
            AnimationTree = GetNode<AnimationTree>("../../AnimationTree");
            StateMachine = AnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

            AnimatedSprite.Play();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            HandleHorizontalFlip();
            if (Player.Jump.Coyote < -0.6 && Player.IsOnFloor())
            {
                Land();
                return;
            }
            var anim = GetAnimationState();
            if (anim == StateMachine.GetCurrentNode()) return;
            StateMachine.Travel(anim);
            if (anim == "Jump") Jump();
        }

        private void HandleHorizontalFlip()
        {
            var facing = Mathf.Sign(Player.Velocity.x);
            if (facing != 0) AnimatedSprite.FlipH = facing != 1;
        }

        private string GetAnimationState()
        {
            if (Fade) return "Fade";
            if (Player.Die.Landed) return "Dead";
            if (Player.Die.Enabled) return "Hit";
            if (Player.IsOnFloor()) return Mathf.Abs(Player.Velocity.x) > RunningAnimThreshold ? "Run" : "Idle";
            if (Player.Velocity.y < 0) return "Jump";
            return Player.Velocity.y > 0 ? "Fall" : null;
        }

        private void Land()
        {
            var tween = Player.AnimatedSprite.GetNode<Tween>("Tween");
            tween.InterpolateProperty(Player.AnimatedSprite, "scale", Player.AnimatedSprite.Scale,
                new Vector2(0.5f, 0.3f), 0.04f);
            tween.InterpolateProperty(Player.AnimatedSprite, "scale", new Vector2(0.5f, 0.3f),
                new Vector2(0.38f, 0.38f), 0.26f, Tween.TransitionType.Linear, Tween.EaseType.In, 0.04f);
            tween.Start();
        }

        private void Jump()
        {
            var tween = Player.AnimatedSprite.GetNode<Tween>("Tween");
            tween.InterpolateProperty(Player.AnimatedSprite, "scale", Player.AnimatedSprite.Scale,
                new Vector2(0.3f, 0.5f), 0.04f);
            tween.InterpolateProperty(Player.AnimatedSprite, "scale", new Vector2(0.3f, 0.5f),
                new Vector2(0.38f, 0.38f), 0.26f, Tween.TransitionType.Linear, Tween.EaseType.Out, 0.04f);
            tween.Start();
        }
    }
}
