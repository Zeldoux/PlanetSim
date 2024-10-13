using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PlanetSim;
using ImGuiNET;



namespace Content.Game.Scene
{
    internal class GameScene : Scene // herit of Scene class
    {

        /// <summary>
        /// Constructor for the MenuScene.
        /// </summary>
        /// <param name="pGame">Reference to the main game instance.</param>
        /// 
        System.Numerics.Vector4 _colorV4;
        bool _toolActive;
        public GameScene(PlanetSimGame pGame) : base(pGame)
        {

            Debug.WriteLine("New Game Scene");
        }

        /// <summary>
        /// Called when the scene is loaded. This is where resources specific to this scene are initialized.
        /// </summary>

        public override void Load()
        {
            _toolActive = true;
            _colorV4 = Color.CornflowerBlue.ToVector4().ToNumerics();
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
            _Graphics.Clear(Color.Black);

            base.Draw(dt); // This will call the parent Scene class's Draw method which renders 3D objects

        }


    }
}