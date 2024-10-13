using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content.Game
{
    public class AtlasLoader
    {
        private Texture2D _atlasTexture;
        private int _spriteWidth;
        private int _spriteHeight;
        private int _atlasWidth;
        private int _columns;

        public AtlasLoader(Texture2D atlasTexture, int spriteWidth, int spriteHeight)
        {
            _atlasTexture = atlasTexture;
            _spriteWidth = spriteWidth;
            _spriteHeight = spriteHeight;

            _atlasWidth = atlasTexture.Width;
            _columns = _atlasWidth / _spriteWidth;  // Number of sprites per row
        }

        public Rectangle GetSpriteRectangle(int index)
        {
            int row = index / _columns;
            int column = index % _columns;

            return new Rectangle(column * _spriteWidth, row * _spriteHeight, _spriteWidth, _spriteHeight);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int spriteIndex)
        {
            Rectangle sourceRectangle = GetSpriteRectangle(spriteIndex);
            spriteBatch.Draw(_atlasTexture, position, sourceRectangle, Color.White);
        }
    }
}
