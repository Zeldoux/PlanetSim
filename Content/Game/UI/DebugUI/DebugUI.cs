using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;

namespace PlanetSim.Content.Game.UI.DebugUI
{
    public class DebugUI : BaseUIGUI
    {
        private int _fps;
        private string _currentScene;
        private Vector2 _mousePosition;
        public bool IsVisible { get; private set; } = false; // Property to control visibility


        public DebugUI(ImGuiRenderer guiRenderer) : base(guiRenderer)
        {
            _currentScene = "None"; // Initialize with a default value
        }

        public override void Load()
        {

            // Load any specific resources if needed
        }

        public override void Update(GameTime dt)
        {
            // Update FPS or other values
            _fps = (int)(1f / dt.ElapsedGameTime.TotalSeconds);
            _mousePosition = Mouse.GetState().Position.ToVector2();
        }

        public override void Draw(GameTime dt)
        {
            if (IsVisible) // Only draw if visible
            {
                ImGui.Begin("Debug Information"); // Start a new ImGui window

                // Display FPS
                ImGui.Text($"FPS: {_fps}");
                // Display current scene
                ImGui.Text($"Current Scene: {_currentScene}");
                // Display mouse position
                ImGui.Text($"Mouse Position: {_mousePosition.X}, {_mousePosition.Y}");

                ImGui.End(); // End the ImGui window
            }
        }

        public void SetCurrentScene(string sceneName)
        {
            _currentScene = sceneName;
        }
        public void ToggleVisibility()
        {
            IsVisible = !IsVisible; // Toggle the visibility
        }
    }
    

}
