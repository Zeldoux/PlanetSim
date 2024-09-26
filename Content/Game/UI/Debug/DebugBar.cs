using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents a debug bar in the game UI.
/// </summary>
/// 
namespace Content.Game.UI.Debug
    {
        internal class DebugBar
    {
        private Texture2D _texture; // background bar texture
        private SpriteBatch _spriteBatch; // SpriteBatch for rendering
        private SpriteFont _font; // Font for displaying text
        private Rectangle _rectangle; // Rectangle bounds for the debug bar
        private Color _backgroundColor; // Background color of the debug bar
        private Color _textColor; // Text color
        private string _currentScene; // Current scene name
        private int _fps; // Frames per second

        /// <summary>
        /// Constructor for DebugBar.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch instance for rendering.</param>
        /// <param name="font">Font for displaying text.</param>
        /// <param name="rectangle">Bounds of the debug bar.</param>
        /// <param name="backgroundColor">Background color of the debug bar.</param>
        /// <param name="textColor">Text color.</param>
        public DebugBar(SpriteBatch spriteBatch, SpriteFont font, Rectangle rectangle, Color backgroundColor, Color textColor)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _rectangle = rectangle;
            _backgroundColor = backgroundColor;
            _textColor = textColor;
            _currentScene = string.Empty; // Initialize with an empty scene name
        }

        /// <summary>
        /// Updates the debug bar with the current FPS.
        /// </summary>
        /// <param name="fps">Current frames per second.</param>
        public void Update(int fps)
        {
            _fps = fps;
        }

        /// <summary>
        /// Sets the current scene name.
        /// </summary>
        /// <param name="sceneName">Name of the current scene.</param>
        public void SetCurrentScene(string sceneName)
        {
            _currentScene = sceneName;
        }

        /// <summary>
        /// Draws the debug bar to the screen.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            // Draw the background rectangle
            _spriteBatch.Draw(_texture, _rectangle, _backgroundColor);
        

            // Draw the current scene name
            _spriteBatch.DrawString(_font, _currentScene, new Vector2(10, 10), _textColor);

            // Draw FPS
            _spriteBatch.DrawString(_font, $"FPS: {_fps}", new Vector2(10, 30), _textColor);

            _spriteBatch.End();
        }
    }

}
