using Content.Game.DebugOverlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetSim.Content.Game;
using PlanetSim.Content.Game.GameState;
using System;
using System.Diagnostics;

namespace PlanetSim
{
    /// <summary>
    /// The MainGame class is the entry point of the game.
    /// It manages the game loop (Initialize, LoadContent, Update, Draw).
    /// </summary>
    public class PlanetSimGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private DebugOverlay _debugOverlay; // debug overlay class 

        public DebugOverlay _DebugOverlay => _debugOverlay; // Read-only property


        private SpriteFont _debugFont; // Add this variable to hold the debug font

        public SpriteBatch SpriteBatch => _spriteBatch; // Read-only property

        // Handles the current game state (e.g., current scene)
        public SceneManager sceneManager;

        // Placeholder for loading textures (e.g., images)
        public Texture2D _texture;

        /// <summary>
        /// Constructor initializes graphics and game state.
        /// </summary>
        public PlanetSimGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            // Set the content root directory
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            // Initialize the game state manager
            sceneManager = new SceneManager(this); // (this) = (client)
        }

        /// <summary>
        /// Called once to initialize the game. Sets the initial scene.
        /// </summary>
        protected override void Initialize()
        {
            // Clear the screen and depth buffer
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);


            // TODO: Add your initialization logic here

            base.Initialize();
        }


        /// <summary>
        /// LoadContent is called once per game to load content (textures, fonts, etc.)
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            

            // Load assets using the AssetManager
            AssetManager.Load(Content);
            
            _debugOverlay = new DebugOverlay(_spriteBatch, AssetManager.DebugFont, this);

            sceneManager.SwitchScene(SceneManager.SceneType.MenuScene);
        }


        /// <summary>
        /// Update is called once per frame to update game logic (e.g., input handling, scene updates).
        /// </summary>
        /// <param name="dt">GameTime instance for timing and frame management.(delta time // gametime  ) g</param>
        protected override void Update(GameTime dt)
        {

            // Check for F1 key press to toggle debug mode
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _debugOverlay.ToggleDebugMode(); // Toggle the debug overlay on F1 key press
                Debug.WriteLine("Key press : A ");
            }
                


            // Update the debug overlay
            _debugOverlay.Update(dt); // Update the debug overlay
            // Exit the game if the Back button on a gamepad or Escape key is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                

            // Update the current scene
            if (sceneManager.CurrentScene != null)
            {
                sceneManager.CurrentScene.Update(dt);
            }
            base.Update(dt);
        }

        /// <summary>
        /// Draw is called once per frame to render the game's visual content.
        /// </summary>
        /// 
        /// <param name="dt">GameTime instance for timing and frame management.</param>
        protected override void Draw(GameTime dt)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           

            // Draw the current scene
            if (sceneManager.CurrentScene != null)
            {
                sceneManager.CurrentScene.Draw(dt);
            }
            base.Draw(dt);
            _debugOverlay.Draw(); // Draw the debug overlay
        }
    }
}
