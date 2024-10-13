using PlanetSim.Content.Game.Sprites;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content.Game.Squirel
{
    public class Squirel : Entity
    {
        private int[] _walkFrames = { 2, 3 }; // Walk animation frames
        private int[] _idleFrames = { 0, 1 }; // Idle animation frames

        public Squirel(AtlasLoader atlas)
            : base(atlas, new int[] { 0, 1 }, 0.2) // Initial animation is idle
        {
        }

        public void SetWalkAnimation()
        {
            SetAnimation(_walkFrames); // Switch to walk animation
        }

        public void SetIdleAnimation()
        {
            SetAnimation(_idleFrames); // Switch to idle animation
        }
    }

}
