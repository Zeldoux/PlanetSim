using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using PlanetSim.Content.Game.Graphics;
using PlanetSim.Content.Game.Settings;
using System;
using System.IO;

public class SettingsManager
{
    private const string SettingsFilePath = "settings.json";

    private GraphicsDeviceManager _graphics;
    public GameSettings _currentSettings;

    public RenderTarget2D _renderTarget;

    private bool _isUpdatingResolution; // Flag to prevent stack overflow during resizing

    public SettingsManager(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;

        try
        {
            _currentSettings = LoadSettings();
            ApplySettings();
        }
        catch (Exception ex)
        {
            // Log or handle loading settings failure
            Console.WriteLine($"Error initializing settings: {ex.Message}");
            _currentSettings = new GameSettings(); // Fallback to default settings
        }

        // Allow user resizing and set up event handler for window size changes
        try
        {
            // Initialize the graphics device settings based on the loaded settings
            ApplyInitialSettings();
        }
        catch (Exception ex)
        {
            // Log or handle graphics settings failure
            Console.WriteLine($"Error applying initial graphics settings: {ex.Message}");
        }
    }

    private void ApplyInitialSettings()
    {
        // Set graphics preferences for the window
        _graphics.PreferredBackBufferWidth = _currentSettings.WindowWidth;
        _graphics.PreferredBackBufferHeight = _currentSettings.WindowHeight;
        _graphics.IsFullScreen = _currentSettings.IsFullscreen;

        // Apply changes initially
        _graphics.ApplyChanges();
    }

    public GameSettings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<GameSettings>(json);
            }
        }
        catch (Exception ex)
        {
            // Log or handle JSON reading/deserialization failure
            Console.WriteLine($"Error loading settings file: {ex.Message}");
        }

        return new GameSettings(); // Return default settings if no file exists or an error occurred
    }

    public void ApplySettings()
    {
        try
        {
            // Apply the current settings
            _graphics.IsFullScreen = _currentSettings.IsFullscreen;

            if (_currentSettings.IsFullscreen)
            {
                // Set to desktop resolution when going fullscreen
                var desktopResolution = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                _graphics.PreferredBackBufferWidth = desktopResolution.Width;
                _graphics.PreferredBackBufferHeight = desktopResolution.Height;
            }
            else
            {
                // Set dimensions based on current settings
                _graphics.PreferredBackBufferWidth = _currentSettings.WindowWidth;
                _graphics.PreferredBackBufferHeight = _currentSettings.WindowHeight;
            }

            // Apply changes
            _graphics.ApplyChanges();

            // Initialize the render target
            InitializeRenderTarget();
        }
        catch (Exception ex)
        {
            // Log or handle settings application failure
            Console.WriteLine($"Error applying settings: {ex.Message}");
        }
    }

    public void InitializeRenderTarget()
    {
        try
        {
            // Dispose of the old render target if it exists
            _renderTarget?.Dispose();

            // Create a new render target with the current window size
            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
                _currentSettings.WindowWidth,
                _currentSettings.WindowHeight);
        }
        catch (Exception ex)
        {
            // Log or handle render target initialization failure
            Console.WriteLine($"Error initializing render target: {ex.Message}");
        }
    }

    public void CheckForSettingsUpdate()
    {
        try
        {
            // Check if window size has changed
            if (_currentSettings.WindowWidth != _graphics.PreferredBackBufferWidth ||
                _currentSettings.WindowHeight != _graphics.PreferredBackBufferHeight)
            {
                // Update the render target since the resolution has changed
                InitializeRenderTarget();

                // Update current settings to reflect the new window size
                _currentSettings.WindowWidth = _graphics.PreferredBackBufferWidth;
                _currentSettings.WindowHeight = _graphics.PreferredBackBufferHeight;
            }
        }
        catch (Exception ex)
        {
            // Log or handle settings update check failure
            Console.WriteLine($"Error checking for settings update: {ex.Message}");
        }
    }

    public void ChangeResolution(int width, int height)
    {
        if (_currentSettings._ResolutionManager.SetResolution(width, height))
        {
            ApplySettings();
            _currentSettings.Save(SettingsFilePath);
        }
    }

    public void HandleWindowResize(int newWidth, int newHeight)
    {
        if (_isUpdatingResolution) return; // Prevent stack overflow
        _isUpdatingResolution = true; // Set the flag

        try
        {
            // Update graphics settings only if they have changed
            if (_graphics.PreferredBackBufferWidth != newWidth || _graphics.PreferredBackBufferHeight != newHeight)
            {
                _graphics.PreferredBackBufferWidth = newWidth;
                _graphics.PreferredBackBufferHeight = newHeight;

                // Apply the changes immediately
                _graphics.ApplyChanges();

                InitializeRenderTarget(); // Update render target to match new size

                // Update the settings to reflect the new size
                _currentSettings.WindowWidth = newWidth;
                _currentSettings.WindowHeight = newHeight;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling window resize: {ex.Message}");
        }
        finally
        {
            _isUpdatingResolution = false; // Reset the flag
        }
    }

    public ResolutionManager ResolutionManager => _currentSettings._ResolutionManager;
    public RenderTarget2D RenderTarget => _renderTarget; // Provide access to the render target
    public GameSettings CurrentSettings => _currentSettings; // Provide access to current settings
}
