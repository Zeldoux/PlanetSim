using Microsoft.Xna.Framework;
using MonoGame.ImGuiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content.Game.UI.DebugUI
{
    public abstract class BaseUIGUI
    {
        protected ImGuiRenderer _guiRenderer;

        public BaseUIGUI(ImGuiRenderer guiRenderer)
        {
            _guiRenderer = guiRenderer;
        }

        public abstract void Load();  // Load resources specific to the UI
        public abstract void Update(GameTime dt);  // Update UI logic
        public abstract void Draw(GameTime dt);  // Draw UI
    }


}
