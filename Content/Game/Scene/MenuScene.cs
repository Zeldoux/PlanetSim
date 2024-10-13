using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PlanetSim;
using PlanetSim.Content.Game.GameState;
using PlanetSim.Content.Game.Squirel;
using PlanetSim.Content.Game;
using System.IO;

using PlanetSim.Content.Game.Settings;
using PlanetSim.Content.Game.UI.MenuUI;
using static UIManager;
using System.Drawing.Printing;
using System;
using SharpDX.Direct2D1;





namespace Content.Game.Scene
{


    /// <summary>
    /// Represents the main menu scene of the game. This is the first screen players see.
    /// Inherits from the Scene class.
    /// </summary>
    internal class MenuScene : Scene // herit of Scene class
    {


        /// <summary>
        /// Constructor for the MenuScene.
        /// </summary>
        /// <param name="pGame">Reference to the main game instance.</param>
        /// 
               

        private MenuUI _menuUI;
        private SettingUI _settingUI;
        private Texture2D _squirrelTexture; // Add this to hold the squirrel texture

        private AtlasLoader _atlasLoader; // Use this for atlas management

        private Squirel _squirrel; // Reference to your Squirrel instance

        private UIManager _uiManager;

        public MenuScene(PlanetSimGame pGame ) : base(pGame)
        {
            
            Debug.WriteLine("New Main menu Scene");
        }

        /// <summary>
        /// Called when the scene is loaded. This is where resources specific to this scene are initialized.
        /// </summary>

        public override void Load()
        {
            
            Texture2D backgroundTexture = CreateWhiteTexture(_Graphics);

            SpriteFont font = AssetManager.MainText;

            // Initialize the UIManager
            UIManager.Initialize(backgroundTexture, font, _SettingsManager._currentSettings, _graphicsManager);

            UIManager.Instance.SwitchUIState(UIManager.UIState.MainMenu);

            

            // Load squirrel sprite from atlas
            string atlasPath = @"A:\Work\C#\PlanetSim\AtlasLoader\Atlas.png";
            using (FileStream stream = new FileStream(atlasPath, FileMode.Open))
            {
                if (stream == null)
                {
                    Debug.WriteLine("Le flux est nul. Vérifiez le chemin d'accès.");
                    return; 
                }
                if (_Graphics == null)
                {
                    Debug.WriteLine("Le GraphicsDevice est nul. Vérifiez son initialisation.");
                    return; // Quitter la méthode si _graphics est nul
                }

                _squirrelTexture = Texture2D.FromStream(_Graphics, stream);

                // Initialize the AtlasLoader with the loaded texture and sprite dimensions
                _atlasLoader = new AtlasLoader(_squirrelTexture, spriteWidth: 64, spriteHeight: 64);

                // Pass the AtlasLoader to the Squirrel
                _squirrel = new Squirel(_atlasLoader);
            }
            base.Load();

            
        }

        /// <summary>
        /// Called when the scene is unloaded. This is where resources are cleaned up.
        /// </summary>
        public override void Unload()
        {
            
            

            Debug.WriteLine("GameScene unloaded");
            base.Unload();
        }

        /// <summary>
        /// Called every frame to update the scene's logic (e.g., input, animations).
        /// </summary>
        /// <param name="dt">GameTime instance for timing and frame management.</param>
        public override void Update(GameTime dt)
        {

            base.Update(dt);

            MouseState mouseState = Mouse.GetState();

            //_menuUI.Update(dt);
            _squirrel.UpdateAnimation(dt);

            UIManager.Instance.Update(dt);
            

        }

        /// <summary>
        /// Called every frame to draw the scene's visuals.
        /// </summary>
        /// <param name="dt">GameTime instance for timing and frame management.</param>
        public override void Draw(GameTime dt)
        {
            // Set the render target to the off-screen texture
            _Graphics.SetRenderTarget(_SettingsManager._renderTarget); // Assuming _renderTarget is initialized somewhere
            _Graphics.Clear(Color.Black);

            // Call the parent Scene class's Draw method which renders 3D objects
            base.Draw(dt);

            // Begin the sprite batch for 2D rendering
            _SpriteBatch.Begin();

            // Draw the squirrel on the menu scene
            _squirrel.Draw(_SpriteBatch, new Vector2(400, 100)); // Adjust the position as needed

            // Draw the UI
            UIManager.Instance.Draw(_SpriteBatch);

            // End the sprite batch
            _SpriteBatch.End();

            // Reset the render target to the back buffer
            _Graphics.SetRenderTarget(null);

            // Now draw the final result from _renderTarget to the back buffer
            _SpriteBatch.Begin();
            _SpriteBatch.Draw(_SettingsManager._renderTarget, new Rectangle(0, 0, PlanetSimGame.virtualWidth, PlanetSimGame.virtualHeight), Color.White);
            _SpriteBatch.End();
        }
        private void NewGameButtonClicked()
        {
            // Handle button click (e.g., switch to login scene)
            mainGame._sceneManager.SwitchScene(SceneManager.SceneType.GameScene);
        }
        Texture2D CreateWhiteTexture(GraphicsDevice graphicsDevice)
        {
            // Create a 1x1 pixel texture
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] data =
            [
                // Set the pixel to white
                Color.White,
            ];
            texture.SetData(data);

            return texture;
        }


    }
}