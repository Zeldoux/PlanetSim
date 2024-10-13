using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PlanetSim.Content.Game.UI.UIclass
{
    public class CustomButton
    {
        private Texture2D _texture;
        private Rectangle _bounds;
        private Color _color;
        private string _text;
        private SpriteFont _font;

        private bool _wasPreviouslyPressed; // To track previous mouse state
        public event Action Clicked;

        public CustomButton(Texture2D texture, Rectangle bounds, string text, SpriteFont font)
        {
            _texture = texture;
            _bounds = bounds;
            _color = Color.White;
            _text = text;
            _font = font;
            _wasPreviouslyPressed = false; // Initially, the button is not pressed
        }

        public void Update(MouseState mouseState)
        {
            // Check if the mouse is over the button
            bool isMouseOverButton = _bounds.Contains(mouseState.X, mouseState.Y);

            // Check if the left mouse button is currently pressed
            bool isCurrentlyPressed = mouseState.LeftButton == ButtonState.Pressed;

            // Trigger click event only if the mouse button is pressed this frame and was not pressed the previous frame
            if (isMouseOverButton && isCurrentlyPressed && !_wasPreviouslyPressed)
            {
                Clicked?.Invoke(); // Call the click event
            }

            // Update the state for the next frame
            _wasPreviouslyPressed = isCurrentlyPressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _bounds, _color);
            var textSize = _font.MeasureString(_text);
            var textPosition = new Vector2(_bounds.X + (_bounds.Width - textSize.X) / 2,
                                            _bounds.Y + (_bounds.Height - textSize.Y) / 2);
            spriteBatch.DrawString(_font, _text, textPosition, Color.Black);
        }

        // New method to update the button's rectangle
        public void UpdateRectangle(int x, int y, int width, int height)
        {
            _bounds = new Rectangle(x, y, width, height);
        }
    }
}
