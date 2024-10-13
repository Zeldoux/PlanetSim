
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using PlanetSim.Content.Game;
using PlanetSim.Content.Game.GameState;
using PlanetSim.Content.Game.Settings;
using PlanetSim.Content.Game.UI.DebugUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using PlanetSim.Content.Game.UI;



namespace PlanetSim
{
    /// <summary>
    /// The MainGame class is the entry point of the game.
    /// It manages the game loop (Initialize, LoadContent, Update, Draw).
    /// </summary>
    public class PlanetSimGame : Game
    {
        public ImGuiRenderer _GuiRenderer; // Renderer for ImGui UI
        public DebugUI _debugUI; // UI for debugging information
        

        public const int virtualWidth = 1280; // Preferred virtual resolution width
        public const int virtualHeight = 720; // Preferred virtual resolution height

        private GraphicsDeviceManager _graphics; // Graphics device manager for rendering
        private SpriteBatch _spriteBatch; // Sprite batch for 2D rendering

        public SpriteBatch SpriteBatch => _spriteBatch; // Property to access sprite batch
        
        public GraphicsDeviceManager GraphicsManager => _graphics; // Property to access graphics manager
       

        public SettingsManager _settingManager; // Manager for game settings
        public SceneManager _sceneManager; // Manager for current game state (e.g., scenes)

        /// <summary>
        /// Constructor initializes graphics and game state.
        /// </summary>
        public PlanetSimGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            _settingManager = new SettingsManager(_graphics);


            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += Window_ClientSizeChanged;

            // Set the content root directory
            Content.RootDirectory = "Content";

            IsMouseVisible = true;


            // Initialize the game state manager
            _sceneManager = new SceneManager(this); // (this) = (client)
        }

        // Event handler for window size changes
        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            // Call the HandleWindowResize method to update the settings manager
            _settingManager.HandleWindowResize(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }

        /// <summary>
        /// Called once to initialize the game. Sets the initial scene.
        /// </summary>
        protected override void Initialize()
        {
            // Clear the screen and depth buffer
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
           
            base.Initialize();

            
        }
        /// <summary>
        /// LoadContent is called once per game to load content (textures, fonts, etc.)
        /// </summary>
        protected override void LoadContent()
        {
            try
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice); // Create a new sprite batch

                // Load assets using the AssetManager
                AssetManager.Load(Content);

                // Apply the settings after loading
                _settingManager.ApplySettings();

                // Initialize ImGui after the settings are applied
                _GuiRenderer = new ImGuiRenderer(this);
                _GuiRenderer.RebuildFontAtlas();

                _debugUI = new DebugUI(_GuiRenderer);
                _debugUI.Load();

                // Initialize the scene manager and switch to the menu scene
                _sceneManager.SwitchScene(SceneManager.SceneType.MenuScene);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading content: {ex.Message}");
            }

        }


        /// <summary>
        /// Update is called once per frame to update game logic (e.g., input handling, scene updates).
        /// </summary>
        /// <param name="dt">GameTime instance for timing and frame management.(delta time // gametime  ) g</param>
        protected override void Update(GameTime dt)
        {
            // Check for updates to the settings
            _settingManager.CheckForSettingsUpdate();
            
            // Check for F1 key press to toggle debug mode
            KeyboardState keyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _debugUI.ToggleVisibility(); // Toggle the debug overlay on F1 key press
                Debug.WriteLine("Key press : A ");
            }
            _debugUI.Update(dt);


            // Exit the game if the Back button on a gamepad or Escape key is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                

            // Update the current scene
            if (_sceneManager.CurrentScene != null)
            {

                _sceneManager.CurrentScene.Update(dt);
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
            try
            {
               
                // Clear the render target
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // Draw the current scene
                if (_sceneManager.CurrentScene != null)
                {
                    _sceneManager.CurrentScene.Draw(dt);
                }

                _GuiRenderer.BeginLayout(dt);
                _debugUI.Draw(dt); // Draw the debug UI
                _GuiRenderer.EndLayout();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during drawing: {ex.Message}");
            }
        }
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            Debug.WriteLine("Game Deactivated");
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            Debug.WriteLine("Game Activated");
        }
    }
}
