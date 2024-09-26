
using Content.Game.Scene;


namespace PlanetSim.Content.Game.GameState
{

    /// <summary>
    /// Manages the different scenes of the game (e.g., main menu, game scene).
    /// Handles switching between scenes.
    /// </summary>
    public class SceneManager
    {
        // scene type enum set 

        /// <summary>
        /// Enum representing the different scenes available in the game.
        /// </summary>
        public enum SceneType
        {
            MenuScene, // Main menu scene
            GameScene,  // Main gameplay scene
            DeadScene, // Scene displayed when the player dies


        }

        // Reference to the main game instance
        protected PlanetSimGame mainGame;


        // Property to store the current active scene
        public Scene CurrentScene { get; set; }

        /// <summary>
        /// Constructor to initialize the GameState manager.
        /// </summary>
        /// <param name="pGame">Reference to the main game instance.</param>

        public SceneManager(PlanetSimGame pGame)
        {
            mainGame = pGame;
        }

        /// <summary>
        /// Switches the current scene to a new scene based on the specified scene type.
        /// Unloads the current scene and loads the new one.
        /// </summary>
        /// <param name="ptype">Type of the scene to switch to.</param>

        // method to switch scene in the GameState handler 
        public void SwitchScene(SceneType ptype)
        {
            // Unload the current scene if it exists
            if (CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }
            // Switch to the new scene based on the scene type
            switch (ptype)
            {
                case SceneType.MenuScene:
                    // create a Mainscene instance
                    CurrentScene = new MenuScene(mainGame);
                    
                    // menu scene where you can go in option // login // quit 
                    break;
                case SceneType.GameScene:
                    // game scene where you see your characters the map other player , enemy and so on ...

                    break;
                case SceneType.DeadScene:
                    // game scene that show up when you loose ? 
                    break;
                default:

                    break;
            }
            // Load the new scene
            CurrentScene.Load();
            mainGame._DebugOverlay.SetCurrentScene(CurrentScene.GetType().Name);
        }
    }
    }
