using Godot;
using ld47.Instances.Player;

namespace ld47.Scenes
{
    public class RotationAnimationPlayer : AnimationPlayer
    {
        public void Start(Player player, bool upsideDown = true)
        {
            if (IsPlaying()) return;
            var anim = upsideDown ? "Rotate" : "RotateBack";
            AssignCameraPosition(player, anim);
            Play(anim);
        }

        private void AssignCameraPosition(Node2D target, string animation)
        {
            var anim = GetAnimation(animation);
            var id = anim.FindTrack("MainCamera:position");
            var pos = target.GlobalPosition;
            anim.TrackSetKeyValue(id, anim.TrackFindKey(id, 0), pos);
            anim.TrackSetKeyValue(id, anim.TrackFindKey(id, 8), pos.Rotated(Mathf.Pi));
        }
    }
}
