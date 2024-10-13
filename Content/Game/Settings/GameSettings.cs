using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using PlanetSim.Content.Game.Graphics;
using PlanetSim.Content.Game.UI;

namespace PlanetSim.Content.Game.Settings
{
    public class GameSettings
    {
        public ResolutionManager _ResolutionManager { get; private set; }

        public bool IsFullscreen { get; set; } // Indicates if the game is in fullscreen mode
        public float Volume { get; set; }       // Volume level (0.0f to 1.0f)
        public Dictionary<string, Keys> KeyBindings { get; set; } // Key bindings for the game controls


        public int WindowWidth { get; set; }    // Width of the game window
        public int WindowHeight { get; set; }   // Height of the game window


        // Constructor to set default values for game settings
        public GameSettings()
        {
            _ResolutionManager = new ResolutionManager(); // Initialize the resolution manager

            IsFullscreen = false; // Default to windowed mode

            Volume = 1.0f; // Default volume level

            KeyBindings = new Dictionary<string, Keys>
            {
                { "MoveUp", Keys.W },
                { "MoveDown", Keys.S },
                { "MoveLeft", Keys.A },
                { "MoveRight", Keys.D },
                { "ToggleFullscreen", Keys.F }
            };

        }

        // Save the current settings to a JSON file
        public void Save(string filePath)
        {
            try
            {
                // Ensure the current resolution is saved into WindowWidth and WindowHeight
                WindowWidth = _ResolutionManager.CurrentResolution.Width;
                WindowHeight = _ResolutionManager.CurrentResolution.Height;

                // Serialize the current settings to a JSON string
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);

                // Write the JSON string to the specified file path
                File.WriteAllText(filePath, json);

                Debug.WriteLine("Settings saved successfully.");

                // Add debugging message for saved settings
                Debug.WriteLine($"Saved GameSettings: {json}");
            }
            catch (Exception ex)
            {
                // Log or handle any errors that occur during saving
                Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }

        // Load game settings from a JSON file
        public static GameSettings Load(string filePath)
        {
            // Check if the settings file exists
            if (!File.Exists(filePath))
            {
                // Return default settings if the file doesn't exist
                Debug.WriteLine("Settings file not found. Loading default settings.");
                return new GameSettings();
            }

            try
            {
                // Read the JSON string from the specified file path
                var json = File.ReadAllText(filePath);
                // Deserialize the JSON string into a GameSettings object
                Debug.WriteLine("loaded setting existing configuration");
                return JsonConvert.DeserializeObject<GameSettings>(json);
            }
            catch (Exception ex)
            {
                // Log or handle any errors that occur during loading
                Debug.WriteLine($"Error loading settings: {ex.Message}");
                return new GameSettings(); // Return default settings in case of error
            }
        }
    }
}
