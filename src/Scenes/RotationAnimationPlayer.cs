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
            Play("Rotate");
        }
    }
}
