using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PlanetSim.Content.Game.Graphics
{
    public class ResolutionManager
    {
        [JsonIgnore] // This prevents serialization of available resolutions
        public List<Resolution> AvailableResolutions { get; private set; }

        public Resolution CurrentResolution { get; set; } // Serialized resolution

        public ResolutionManager()
        {
            RefreshAvailableResolutions();
            
        }
        public void RefreshAvailableResolutions()
        {
            AvailableResolutions = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes
                .Where(m => m.Format == SurfaceFormat.Color) // Only consider color formats
                .Select(m => new Resolution(m.Width, m.Height))
                .Distinct()
                .OrderBy(r => r.Width)
                .ToList();

            // If no resolution is set, default to the first available one
            if (CurrentResolution == null || !AvailableResolutions.Contains(CurrentResolution))
            {
                CurrentResolution = AvailableResolutions.FirstOrDefault() ?? new Resolution(1280, 720); // Fallback resolution
            }
        }
        public bool SetResolution(int width, int height)
        {
            var resolution = AvailableResolutions.FirstOrDefault(r => r.Width == width && r.Height == height);
            if (resolution != null)
            {
                CurrentResolution = resolution;
                return true;
            }
            return false;
        }

        public void ResetToDefault()
        {
            CurrentResolution = AvailableResolutions.FirstOrDefault() ?? new Resolution(1280, 720); // Fallback resolution
        }
    }
}

