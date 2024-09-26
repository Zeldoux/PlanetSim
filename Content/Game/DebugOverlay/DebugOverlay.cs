using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlanetSim;

using System.Diagnostics;

namespace Content.Game.DebugOverlay

{


    public class DebugOverlay
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font; // font style
        private bool _isDebugMode; // debug mode active/unactive
        private int _fps; // FPS 
        private string _currentScene;
        private Texture2D _backgroundDebugTexture;

        // The position and size of the debug overlay bar
        private Rectangle _overlayRectangle;

        // A solid color for the overlay
        private Color _overlayColor = Color.Black;

        // Reference to the main game instance
        protected PlanetSimGame mainGame;


        public DebugOverlay(SpriteBatch spriteBatch, SpriteFont font,PlanetSimGame pGame)
        {
            mainGame = pGame;
            _spriteBatch = spriteBatch;
            _font = font;

            // Create a simple square texture for the button
            _backgroundDebugTexture = new Texture2D(mainGame.GraphicsDevice, 1, 1);
            _backgroundDebugTexture.SetData(new Color[] { Color.Black });

            // Define the size and position of the debug overlay bar
            _overlayRectangle = new Rectangle(0, 0, 800, 50); // Adjust width to match your screen size

            _isDebugMode = false; // Start with debug mode off

        }

        public void Update(GameTime gameTime)
        {
            _fps = (int)(1 / gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void ToggleDebugMode()
        {
            _isDebugMode = !_isDebugMode; // Toggle the debug mode
        }

        public void SetCurrentScene(string sceneName)
        {
            _currentScene = sceneName;
        }

        public void Draw()
        {
            if (_isDebugMode)
            {
                _spriteBatch.Begin();

                // Draw the overlay background with a solid color
                _spriteBatch.Draw(_backgroundDebugTexture, _overlayRectangle, _overlayColor);

                // Draw the current scene name
                Vector2 sceneNamePosition = new Vector2(10, 10); // Starting position
                _spriteBatch.DrawString(_font, _currentScene, sceneNamePosition, Color.White);

                // Draw FPS
                string fpsText = $"FPS: {_fps}";
                _spriteBatch.DrawString(_font, fpsText, new Vector2(10, 30), Color.White);


                

                _spriteBatch.End();
            }
        }
    }

}