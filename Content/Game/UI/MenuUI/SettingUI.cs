using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetSim.Content.Game.Settings;
using PlanetSim.Content.Game.UI.UIclass;
using PlanetSim.Content.Game.Graphics;
using PlanetSim.Content.Game.UI.ElementUI;

namespace PlanetSim.Content.Game.UI.MenuUI
{
    public class SettingUI : UI
    {
        private List<CustomButton> _buttons;

        private ScrollableList _resolutionList;

        private ResolutionManager _resolutionManager;

        private GameSettings _gameSettings;

        private GraphicsDeviceManager _graphicsDeviceManager;

        private bool needsApplyChanges = false;
        private int _currentResolutionIndex; // To track the selected resolution
        private bool _isFullscreenChanged = false;
        private bool _isResolutionChanged = false;

        private bool _isResolutionListVisible = false;

        private Vector2 _uiScaleFactor;

        public SettingUI(Texture2D backgroundTexture, SpriteFont font, GameSettings gameSettings, GraphicsDeviceManager graphicsDeviceManager, ResolutionManager resolutionManager)
            : base(backgroundTexture, font)
        {
            _gameSettings = gameSettings;

            _graphicsDeviceManager = graphicsDeviceManager;

            _buttons = new List<CustomButton>();

            _resolutionManager = resolutionManager;


            // Calculate the scaling factor relative to a base resolution
            Vector2 baseResolution = new Vector2(1920, 1080);
            _uiScaleFactor = new Vector2(
                _graphicsDeviceManager.PreferredBackBufferWidth / baseResolution.X,
                _graphicsDeviceManager.PreferredBackBufferHeight / baseResolution.Y);

            // Create buttons
            var exitButton = new CustomButton(
                backgroundTexture,
                ScaleRectangle(new Rectangle(100, 300, 200, 50)),
                "Return",
                _font);
            exitButton.Clicked += OnExitClicked;
            _buttons.Add(exitButton);

            var fullscreenButton = new CustomButton(
                backgroundTexture,
                ScaleRectangle(new Rectangle(100, 100, 200, 50)),
                "Toggle Fullscreen",
                _font);
            fullscreenButton.Clicked += OnToggleFullscreenClicked;
            _buttons.Add(fullscreenButton);

            var ResolutionButton = new CustomButton(
                backgroundTexture,
                ScaleRectangle(new Rectangle(100, 200, 200, 50)),
                "Select Resolution",
                _font);
            ResolutionButton.Clicked += OnResolutionButtonClicked;
            _buttons.Add(ResolutionButton);


            // Initialize the ScrollableList for resolutions
            _resolutionList = new ScrollableList(backgroundTexture, new Rectangle(250, 200, 200, 140), _font, 5);

            // Populate the resolution list directly using the ScrollableList method
            _resolutionList.PopulateList(
                _resolutionManager.AvailableResolutions,
                resolution => $"{resolution.Width} x {resolution.Height}"); // Convert each resolution to string

            _resolutionList.ItemSelected += OnResolutionSelected;

            var Save = new CustomButton(
               backgroundTexture,
               ScaleRectangle(new Rectangle(100, 400, 200, 50)),
               "Save Changes",
               _font);
            Save.Clicked += OnSaveClicked;
            _buttons.Add(Save);
        }

        private Rectangle ScaleRectangle(Rectangle original)
        {
            // Scale the position and size of the rectangle according to the current screen resolution
            return new Rectangle(
                (int)(original.X * _uiScaleFactor.X),
                (int)(original.Y * _uiScaleFactor.Y),
                (int)(original.Width * _uiScaleFactor.X),
                (int)(original.Height * _uiScaleFactor.Y));
        }
        private void OnResolutionSelected()
        {
            
            var selectedResolution = _resolutionList.GetSelectedResolution();

            if (selectedResolution != null)
            {
                // Change resolution in the SettingsManager
                _resolutionManager.SetResolution(selectedResolution.Width, selectedResolution.Height);

                // Update GameSettings to reflect the new resolution
                _gameSettings.WindowWidth = _resolutionManager.CurrentResolution.Width;
                _gameSettings.WindowHeight = _resolutionManager.CurrentResolution.Height;


            }
        }
        private void OnResolutionButtonClicked()
        {
            _isResolutionListVisible = !_isResolutionListVisible;
        }

        private void OnToggleFullscreenClicked()
        {
            _isFullscreenChanged = true; // Mark as changed

            _gameSettings.IsFullscreen = !_gameSettings.IsFullscreen; // Toggle fullscreen state

        }

        private void OnExitClicked()
        {
            UIManager.Instance.SwitchUIState(UIManager.UIState.MainMenu);
            // Logic to exit the game
            Debug.WriteLine("Exit Clicked");
        }
        private void OnSaveClicked()
        {
            if (_isFullscreenChanged || _resolutionList.SelectedItemIndex != -1)
            {
                // Update fullscreen mode
                _graphicsDeviceManager.IsFullScreen = _gameSettings.IsFullscreen;

                // Determine the resolution settings
                if (_gameSettings.IsFullscreen)
                {
                    // Set the back buffer size to the desktop resolution
                    var desktopResolution = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                    _gameSettings.WindowWidth = desktopResolution.Width;
                    _gameSettings.WindowHeight = desktopResolution.Height;
                }
                else
                {
                    // Use the selected resolution from the dropdown list
                    if (_resolutionList.SelectedItemIndex >= 0 &&
                        _resolutionList.SelectedItemIndex < _resolutionManager.AvailableResolutions.Count)
                    {
                        var selectedResolution = _resolutionManager.AvailableResolutions[_resolutionList.SelectedItemIndex];
                        _gameSettings.WindowWidth = selectedResolution.Width;
                        _gameSettings.WindowHeight = selectedResolution.Height;
                    }
                }

                // Apply the new back buffer dimensions
                _graphicsDeviceManager.PreferredBackBufferWidth = _gameSettings.WindowWidth;
                _graphicsDeviceManager.PreferredBackBufferHeight = _gameSettings.WindowHeight;

                // Apply the changes to the graphics device
                _graphicsDeviceManager.ApplyChanges();

                // Log changes for debugging
                Debug.WriteLine($"Graphics settings applied: Fullscreen = {_gameSettings.IsFullscreen}, Width = {_gameSettings.WindowWidth}, Height = {_gameSettings.WindowHeight}");

                // Save the new settings to file
                _gameSettings.Save("settings.json");

                // Reset flags after saving
                _isFullscreenChanged = false;

            }
        }
    
        public override void Update(GameTime dt)
        {

            // Update the ScrollableList
            MouseState mouseState = Mouse.GetState();
            foreach (var button in _buttons)
            {
               
                button.Update(mouseState);
            }
            _resolutionList.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           

            // Draw background
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            // Draw UI elements with scaling
            spriteBatch.DrawString(_font, "Settings", new Vector2(100 * _uiScaleFactor.X, 50 * _uiScaleFactor.Y), Color.White);

            string modeText = _gameSettings.IsFullscreen ? "Fullscreen" : "Windowed";
            spriteBatch.DrawString(_font, "Display Mode: " + modeText, new Vector2(100 * _uiScaleFactor.X, 150 * _uiScaleFactor.Y), Color.White);

            spriteBatch.DrawString(_font, $"Resolution: {_resolutionManager.CurrentResolution.Width} x {_resolutionManager.CurrentResolution.Height}",
                new Vector2(100 * _uiScaleFactor.X, 250 * _uiScaleFactor.Y), Color.White);

            // Draw the ScrollableList on button click 
            if (_isResolutionListVisible)
            {
                _resolutionList.Draw(spriteBatch);
            }

            // Draw buttons
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
