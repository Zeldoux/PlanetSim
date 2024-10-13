using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlanetSim.Content.Game.Settings;
using PlanetSim.Content.Game.UI;
using PlanetSim.Content.Game.UI.MenuUI;
using System;
using System.Collections.Generic;

public class UIManager
{
    private Vector2 _virtualResolution = new Vector2(1920, 1080);
    private Vector2 _scaleFactor;
    private GraphicsDeviceManager _graphicsDeviceManager;

    // Step 1: Create a private static instance of the class
    private static UIManager _instance;

    // Step 2: Provide a public static property to access the instance
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Step 3: Ensure instance is initialized the first time it is accessed
                throw new Exception("UIManager not initialized. Call Initialize before accessing the Instance.");
            }
            return _instance;
        }
    }

    // Step 4: Provide an initialization method (optional but recommended for initialization with parameters)
    public static void Initialize(Texture2D backgroundTexture, SpriteFont font, GameSettings gameSettings, GraphicsDeviceManager graphicsDeviceManager)
    {
        if (_instance == null)
        {
            _instance = new UIManager(backgroundTexture, font, gameSettings, graphicsDeviceManager);
        }
        else
        {
            throw new Exception("UIManager is already initialized.");
        }
    }

    public enum UIState
    {
        MainMenu,
        Settings,
        InGame
    }

    private Dictionary<UIState, UI> _uiComponents;
    private UIState _currentUIState;

    // Step 5: Make the constructor private to prevent external instantiation
    private UIManager(Texture2D backgroundTexture, SpriteFont font, GameSettings gameSettings, GraphicsDeviceManager graphicsDeviceManager)
    {
        _uiComponents = new Dictionary<UIState, UI>
        {
            { UIState.MainMenu, new MenuUI(backgroundTexture, font) },
            { UIState.Settings, new SettingUI(backgroundTexture, font, gameSettings, graphicsDeviceManager,gameSettings._ResolutionManager) }
            // Add more UI components as needed
        };


        _currentUIState = UIState.MainMenu;
        _graphicsDeviceManager = graphicsDeviceManager;

        // Calculate the initial scale factor
        RecalculateUIScaleFactor();

    }
    private void RecalculateUIScaleFactor()
    {
        _scaleFactor = new Vector2(
            _virtualResolution.X / _graphicsDeviceManager.PreferredBackBufferWidth,
            _virtualResolution.Y / _graphicsDeviceManager.PreferredBackBufferHeight);
    }
    public Rectangle ScaleRectangle(Rectangle original)
    {
        return new Rectangle(
            (int)(original.X * _scaleFactor.X),
            (int)(original.Y * _scaleFactor.Y),
            (int)(original.Width * _scaleFactor.X),
            (int)(original.Height * _scaleFactor.Y));
    }
    public Vector2 AnchorToBottomRight(Vector2 originalSize, int padding = 20)
    {
        return new Vector2(
            _graphicsDeviceManager.PreferredBackBufferWidth - (originalSize.X * _scaleFactor.X) - padding,
            _graphicsDeviceManager.PreferredBackBufferHeight - (originalSize.Y * _scaleFactor.Y) - padding);
    }

    public Vector2 AnchorToCenter(Vector2 originalSize)
    {
        return new Vector2(
            (_graphicsDeviceManager.PreferredBackBufferWidth - (originalSize.X * _scaleFactor.X)) / 2,
            (_graphicsDeviceManager.PreferredBackBufferHeight - (originalSize.Y * _scaleFactor.Y)) / 2);
    }
    public void OnResolutionChanged()
    {
        RecalculateUIScaleFactor();
    }

    public void SwitchUIState(UIState newState)
    {
        _currentUIState = newState;
    }
    public Vector2 ScaleFactor
    {
        get { return _scaleFactor; }
    }

    public void Update(GameTime gameTime)
    {
        _uiComponents[_currentUIState].Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw the current UI state, scaling its elements
        _uiComponents[_currentUIState].Draw(spriteBatch);
       
    }
}
