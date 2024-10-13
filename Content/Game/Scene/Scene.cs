using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlanetSim;
using MonoGame.ImGuiNet;
using SharpDX;
using PlanetSim.Content.Game.UI;
using System;
using System.Diagnostics;
using PlanetSim.Content.Game.UI.DebugUI;
using PlanetSim.Content.Game.Settings;
using System.Drawing;


namespace Content.Game.Scene
{

    public class Scene
    {
        protected GraphicsDevice _Graphics; // Use the shared GraphicsDevice

        protected GraphicsDeviceManager _graphicsManager;

        protected SettingsManager _SettingsManager;
  
        protected ContentManager _Content; // Use the shared ContentManager

        protected SpriteBatch _SpriteBatch; // use the shared SpriteBatch
        

        // Store references to the settings 
        protected GameSettings _gameSettings;

        protected ImGuiRenderer _guiRenderer;

        protected DebugUI _debugUI;

        // Reference to the main game instance
        protected PlanetSimGame mainGame;

             


        public Scene (PlanetSimGame pGame)
        {
            mainGame = pGame ?? throw new ArgumentNullException(nameof(pGame));

            Initialize(); // Initialize Graphics, Content, etc.





        }
        private void Initialize()
        {
            
            _graphicsManager = mainGame.GraphicsManager ?? throw new ArgumentNullException("Game GraphicManager not initialized");
            _SettingsManager = mainGame._settingManager ?? throw new InvalidOperationException("Game Settings not initialized.");
            _Graphics = mainGame.GraphicsDevice ?? throw new InvalidOperationException("GraphicsDevice not initialized.");
            _Content = mainGame.Content ?? throw new InvalidOperationException("ContentManager not initialized.");
            _SpriteBatch = mainGame.SpriteBatch ?? throw new InvalidOperationException("SpriteBatch not initialized.");
            _guiRenderer = mainGame._GuiRenderer ?? throw new InvalidOperationException("GuiRenderer not initialized.");

        }
        public virtual void Load()
        {
    

        }
        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime dt)
        {

        }

        public virtual void Draw(GameTime dt)
        {
           

        }
    }


}
