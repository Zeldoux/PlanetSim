using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace PlanetSim.Content.Game.UI
{
    public abstract class UI
    {
        protected Texture2D _backgroundTexture;
        protected SpriteFont _font;
        protected bool _isActive; // Boolean to check if the UI is active

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        protected UI(Texture2D backgroundTexture, SpriteFont font)
        {
            _backgroundTexture = backgroundTexture;
            _font = font;
            _isActive = false; // Initialize as inactive
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
    

}
