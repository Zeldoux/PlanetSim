using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using PlanetSim.Content.Game.UI.UIclass;
using System;

namespace PlanetSim.Content.Game.UI.MenuUI
{
    public class MenuUI : UI
    {
        private List<CustomButton> _buttons;
        

        public MenuUI(Texture2D backgroundTexture, SpriteFont font) : base(backgroundTexture, font)
        {
            _buttons = new List<CustomButton>();
            _backgroundTexture = backgroundTexture;
            _font = font;


            // Create buttons directly in the constructor
            var startButton = new CustomButton(_backgroundTexture, new Rectangle(100, 100, 200, 50), "Start Game", _font);
            startButton.Clicked += OnStartGameClicked;
            _buttons.Add(startButton);

            var optionsButton = new CustomButton(_backgroundTexture, new Rectangle(100, 200, 200, 50), "Options", _font);
            optionsButton.Clicked += OnOptionsClicked;
            _buttons.Add(optionsButton);

            var exitButton = new CustomButton(_backgroundTexture, new Rectangle(100, 300, 200, 50), "Exit", _font);
            exitButton.Clicked += OnExitClicked;
            _buttons.Add(exitButton);
        }


        private void OnStartGameClicked()
        {
            // Logic to switch to the game scene
            Debug.WriteLine("Start Game Clicked");
        }

        private void OnOptionsClicked()
        {
            UIManager.Instance.SwitchUIState(UIManager.UIState.Settings);
            // Logic to open options
            Debug.WriteLine("Options Clicked");
        }

        private void OnExitClicked()
        {
            // Close the game client
            
            Environment.Exit(0); // Exit the application
            // Logic to exit the game
            Debug.WriteLine("Exit Clicked");
        }

        public override void Update(GameTime gameTime)
        {
            

            MouseState mouseState = Mouse.GetState();
            foreach (var button in _buttons)
            {
                button.Update(mouseState);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            // Draw buttons
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
