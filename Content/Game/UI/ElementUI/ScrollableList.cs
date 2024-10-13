using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlanetSim.Content.Game.Graphics;

namespace PlanetSim.Content.Game.UI.ElementUI
{
    public class ScrollableList
    {
        private List<string> _items; // List of items to display
        private Texture2D _backgroundTexture; // Background for the list
        private Rectangle _bounds; // Bounds of the scrollable list
        private SpriteFont _font; // Font for rendering text
        private int _visibleItemCount; // Number of items visible in the list
        private int _scrollIndex; // Current starting index for scrolling
        private int _itemHeight; // Height of each item
        private int previousScrollValue; // Store the previous scroll value for comparison
        private int _hoveredItemIndex = -1; // Index of the hovered item

        private MouseState previousMouseState; // Track previous mouse state
        private List<Rectangle> _itemBounds; // List to store bounds of each item for hit detection
        private int _selectedItemIndex; // Field to store the selected item index

        // Define an event for item selection
        public event Action ItemSelected;

        /// <summary>
        /// Initializes a new instance of the ScrollableList class.
        /// </summary>
        /// <param name="backgroundTexture">The texture for the background of the list.</param>
        /// <param name="bounds">The bounds of the scrollable area.</param>
        /// <param name="font">The font used for item text.</param>
        /// <param name="visibleItemCount">The number of items visible at once.</param>
        public ScrollableList(Texture2D backgroundTexture, Rectangle bounds, SpriteFont font, int visibleItemCount)
        {
            _items = new List<string>();
            _backgroundTexture = backgroundTexture;
            _bounds = bounds;
            _font = font;
            _visibleItemCount = visibleItemCount;
            _itemHeight = (int)font.LineSpacing; // Get item height based on font size
            _scrollIndex = 0; // Start at the top of the list
            _selectedItemIndex = -1; // Initialize to -1 (no selection)
            _itemBounds = new List<Rectangle>(); // Initialize item bounds list
        }

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(string item)
        {
            if (!string.IsNullOrEmpty(item)) // Security check: ensure item is not null or empty
            {
                _items.Add(item);
            }
        }

        /// <summary>
        /// Populates the list with items from a provided collection, using a converter function.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="items">The collection of items to add.</param>
        /// <param name="itemToString">A function to convert each item to a string.</param>
        public void PopulateList<T>(IEnumerable<T> items, Func<T, string> itemToString)
        {
            foreach (var item in items)
            {
                if (itemToString != null) // Ensure the function is not null
                {
                    AddItem(itemToString(item)); // Convert item to string and add
                }
            }
        }

        /// <summary>
        /// Updates the state of the scrollable list based on mouse input.
        /// </summary>
        /// <param name="mouseState">The current state of the mouse.</param>
        public void Update(MouseState mouseState)
        {
            // Check if the mouse is within the bounds of the scrollable list
            if (_bounds.Contains(mouseState.Position))
            {
                _itemBounds.Clear(); // Clear item bounds for the current frame

                // Iterate over the visible items
                for (int i = 0; i < _visibleItemCount; i++)
                {
                    int itemIndex = _scrollIndex + i; // Calculate the actual item index
                    if (itemIndex < _items.Count) // Check if the index is within bounds
                    {
                        Rectangle itemRect = new Rectangle(_bounds.X, _bounds.Y + i * _itemHeight, _bounds.Width, _itemHeight);
                        _itemBounds.Add(itemRect); // Add the item's bounds for hit detection

                        // Check if the mouse is hovering over this item
                        if (itemRect.Contains(mouseState.Position))
                        {
                            _hoveredItemIndex = itemIndex; // Update hovered index
                        }
                    }
                }

                // Handle clicking on items
                if (mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    // Only select the item if it's within bounds
                    if (_hoveredItemIndex >= 0 && _hoveredItemIndex < _items.Count)
                    {
                        SelectedItemIndex = _hoveredItemIndex; // Set the selected item index
                        ItemSelected?.Invoke(); // Trigger the item selected event
                    }
                }
            }

            // Handle scrolling input (mouse wheel)
            int scrollDifference = mouseState.ScrollWheelValue - previousScrollValue; // Calculate scroll change
            if (scrollDifference > 0 && _scrollIndex > 0) // Scroll up
            {
                _scrollIndex--;
            }
            else if (scrollDifference < 0 && _scrollIndex < Math.Max(0, _items.Count - _visibleItemCount)) // Scroll down
            {
                _scrollIndex++;
            }

            // Clamp scroll index to ensure it stays within bounds
            _scrollIndex = MathHelper.Clamp(_scrollIndex, 0, Math.Max(0, _items.Count - _visibleItemCount));

            // Update the previous scroll value for the next frame
            previousScrollValue = mouseState.ScrollWheelValue;

            // Store current mouse state for the next frame
            previousMouseState = mouseState;
        }

        /// <summary>
        /// Draws the scrollable list.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch used for drawing.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background
            spriteBatch.Draw(_backgroundTexture, _bounds, Color.White);

            _itemBounds.Clear(); // Clear item bounds for drawing

            // Draw each visible item
            for (int i = 0; i < _visibleItemCount; i++)
            {
                int itemIndex = _scrollIndex + i; // Calculate the actual item index
                if (itemIndex < _items.Count) // Check if the index is within bounds
                {
                    var textPosition = new Vector2(_bounds.X, _bounds.Y + i * _itemHeight);

                    // Change color if the item is hovered
                    Color textColor = (itemIndex == _hoveredItemIndex) ? Color.Yellow : Color.Black;

                    spriteBatch.DrawString(_font, _items[itemIndex], textPosition, textColor); // Draw the item text

                    Rectangle itemRect = new Rectangle(_bounds.X, _bounds.Y + i * _itemHeight, _bounds.Width, _itemHeight);
                    _itemBounds.Add(itemRect); // Add the item bounds for future hit detection
                }
            }
        }

        /// <summary>
        /// Gets the currently selected item index.
        /// </summary>
        public int SelectedItemIndex
        {
            get => _selectedItemIndex; // Get the selected item index
            private set => _selectedItemIndex = value; // Set the selected item index
        }

        /// <summary>
        /// Gets the currently selected item as a string.
        /// </summary>
        /// <returns>The selected item or null if none is selected.</returns>
        public string GetSelectedItem()
        {
            // Check if the selected index is within bounds
            if (_selectedItemIndex >= 0 && _selectedItemIndex < _items.Count)
            {
                return _items[_selectedItemIndex]; // Return the selected item
            }
            return null; // Return null if no item is selected
        }

        /// <summary>
        /// Gets the selected resolution based on the currently selected item.
        /// </summary>
        /// <returns>A Resolution struct or default if not found.</returns>
        public Resolution GetSelectedResolution()
        {
            // Check if the selected index is within bounds
            if (_selectedItemIndex >= 0 && _selectedItemIndex < _items.Count)
            {
                // Convert the display string back to a Resolution struct
                string[] dimensions = _items[_selectedItemIndex].Split('x');
                if (dimensions.Length == 2 &&
                    int.TryParse(dimensions[0].Trim(), out int width) &&
                    int.TryParse(dimensions[1].Trim(), out int height))
                {
                    return new Resolution(width, height); // Return the parsed Resolution
                }
            }
            return default; // Return default Resolution if not found
        }
    }
}
