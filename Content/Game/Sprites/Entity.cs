using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content.Game.Sprites
{
    public class Entity
    {
        private int[] _currentFrames; // The current animation frames
        private int _currentFrameIndex; // Index to track the current frame in the animation
        private double _animationSpeed; // The speed of the animation
        private double _timeElapsed; // Time to keep track of animation progression
        private AtlasLoader _atlas; // The sprite atlas loader

        public Entity(AtlasLoader atlas, int[] initialFrames, double animationSpeed)
        {
            _atlas = atlas;
            _currentFrames = initialFrames;
            _animationSpeed = animationSpeed;
            _currentFrameIndex = 0;
            _timeElapsed = 0;
        }

        public void SetAnimation(int[] frames)
        {
            _currentFrames = frames;
            _currentFrameIndex = 0; // Reset to the first frame of the new animation
        }

        public void UpdateAnimation(GameTime dt)
        {
            _timeElapsed += dt.ElapsedGameTime.TotalSeconds;

            if (_timeElapsed >= _animationSpeed)
            {
                _currentFrameIndex++;
                if (_currentFrameIndex >= _currentFrames.Length)
                {
                    _currentFrameIndex = 0; // Loop back to the first frame
                }

                _timeElapsed = 0; // Reset the timer for the next frame
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int currentFrame = _currentFrames[_currentFrameIndex];
            _atlas.Draw(spriteBatch, position, currentFrame);
        }
    }


}
