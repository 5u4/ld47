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

        public override void _Ready()
        {
            base._Ready();

            Player = GetNode<Player>("../..");
            AnimatedSprite = GetNode<AnimatedSprite>("../../AnimatedSprite");
            AnimationTree = GetNode<AnimationTree>("../../AnimationTree");
            StateMachine = AnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

            AnimatedSprite.Play();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            HandleHorizontalFlip();
            var anim = GetAnimationState();
            if (anim != StateMachine.GetCurrentNode()) StateMachine.Travel(anim);
        }

        private void HandleHorizontalFlip()
        {
            var facing = Mathf.Sign(Player.Velocity.x);
            if (facing != 0) AnimatedSprite.FlipH = facing != 1;
        }

        private string GetAnimationState()
        {
            if (Player.IsOnFloor()) return Mathf.Abs(Player.Velocity.x) > RunningAnimThreshold ? "Run" : "Idle";
            if (Player.Velocity.y < 0) return "Jump";
            return Player.Velocity.y > 0 ? "Fall" : null;
        }
    }
}
