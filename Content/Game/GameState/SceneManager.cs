
using Content.Game.Scene;
using System;


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
            CurrentScene?.Unload(); // Compact check for existing scene
            CurrentScene = ptype switch
            {
                SceneType.MenuScene => new MenuScene(mainGame),
                SceneType.GameScene => new GameScene(mainGame),
                //SceneType.DeadScene => new DeadScene(mainGame), // (Assuming DeadScene exists)
                _ => throw new ArgumentOutOfRangeException()
            };



            CurrentScene.Load();
           
        }
    }
    }
