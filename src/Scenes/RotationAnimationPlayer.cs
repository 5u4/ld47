using Godot;
using ld47.Instances.Player;

namespace ld47.Scenes
{
    public class RotationAnimationPlayer : AnimationPlayer
    {
        public bool Played;
        
        public void Start(Player player)
        {
            if (Played) return;
            Played = true;
            AssignCameraPosition(player);
            Play("Rotate");
        }

        private void AssignCameraPosition(Node2D target)
        {
            var anim = GetAnimation("Rotate");
            var id = anim.FindTrack("MainCamera:position");
            var pos = target.GlobalPosition;
            anim.TrackSetKeyValue(id, anim.TrackFindKey(id, 0), pos);
            anim.TrackSetKeyValue(id, anim.TrackFindKey(id, 8), pos.Rotated(Mathf.Pi));
        }
    }
}
