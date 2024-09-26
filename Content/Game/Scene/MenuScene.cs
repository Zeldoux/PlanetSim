using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PlanetSim;
using System;


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

        public MenuScene(PlanetSimGame pGame) : base(pGame)
        {
            
            Debug.WriteLine("New Main menu Scene");
        }

        /// <summary>
        /// Called when the scene is loaded. This is where resources specific to this scene are initialized.
        /// </summary>

        public override void Load()
        {
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
            
        }

        /// <summary>
        /// Called every frame to draw the scene's visuals.
        /// </summary>
        /// <param name="dt">GameTime instance for timing and frame management.</param>
        public override void Draw(GameTime dt)
        {
            // First, draw the 3D scene

            base.Draw(dt); // This will call the parent Scene class's Draw method which renders 3D objects

            // Begin the sprite batch for 2D rendering
            _SpriteBatch.Begin();

            // End the sprite batch
            _SpriteBatch.End();


            


        }
   

    }
}