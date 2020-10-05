using Godot;

namespace ld47.Scenes
{
    public class UI : Control
    {
        public void EnableSuicide()
        {
            GetNode<Control>("Instructions/Suicide").Show();
        }
    }
}
