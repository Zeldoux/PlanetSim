using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using PlanetSim;

namespace Content.Game.Scene
{

    public class Scene
    {
        protected GraphicsDevice _Graphics; // Use the shared GraphicsDevice

        protected ContentManager _Content; // Use the shared ContentManager

        protected SpriteBatch _SpriteBatch; // use the shared SpriteBatch



        // Reference to the main game instance
        protected PlanetSimGame mainGame;



        public Scene (PlanetSimGame pGame)
        {
            mainGame = pGame;

            _Graphics = mainGame.GraphicsDevice; // Reference to the main game's GraphicsDevice

            _Content = mainGame.Content; // Reference to the main game's ContentManager

            _SpriteBatch = mainGame.SpriteBatch;


        }
        public virtual void Load()
        {
            // 
        }
        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime dt)
        {

        }

        public virtual void Draw(GameTime dt)
        {
            _Graphics.Clear(Color.White);

        }
    }


}
