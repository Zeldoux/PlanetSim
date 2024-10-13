using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content.Game.Graphics
{
    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        // Override Equals and GetHashCode to compare resolutions easily
        public override bool Equals(object obj)
        {
            if (obj is Resolution other)
                return this.Width == other.Width && this.Height == other.Height;

            return false;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }
    }



}
