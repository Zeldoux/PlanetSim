using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlanetSim.Content.Game
{
    internal class AssetManager

    {
        /// <summary>
        /// Manages the loading and storage of game assets (e.g., fonts, textures).
        /// Provides a centralized way to access these assets.
        /// </summary>
        // static to be kind of structure to store some data and not really a class (no need to instantiate )
        public static SpriteFont MainText { get; private set; }
        public static SpriteFont SecondaryText { get; private set; }

        public static SpriteFont DebugFont { get; private set; } // Add a property for the debug font

        // only staticm ethod can access static data

        /// <summary>
        /// Loads the game assets and stores them in static properties for global access.
        /// </summary>
        /// <param name="pContent">ContentManager instance for loading assets.</param>
        public static void Load(ContentManager pContent)
        {
            // LOAD FONTS ASSETS HERE 
            try
            {
                // Load the fonts from the Content pipeline
                MainText = pContent.Load<SpriteFont>("Assets/Fonts/MainFont");

                SecondaryText = pContent.Load<SpriteFont>("Assets/Fonts/SecondaryFont");

                DebugFont = pContent.Load<SpriteFont>("Assets/Fonts/DebugFont"); // Load the debug font

                if (DebugFont == null )
                {
                    throw new Exception("Debug font not loaded correctly.");
                }
                
                if (MainText == null)
                {
                    throw new Exception("MainFont not loaded correctly.");
                }

                if (SecondaryText == null)
                {
                    throw new Exception("Medieval font not loaded correctly.");
                }
                
            }
            catch (ContentLoadException e)
            {
                throw new Exception("Failed to load content: " + e.Message);
            }
            
        }

    }


}

