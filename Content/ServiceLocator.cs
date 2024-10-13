using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetSim.Content
{
    public static class ServiceLocator
    {
        private static GraphicsDevice _graphicsDevice;

        public static void Register(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public static T Get<T>()
        {
            if (typeof(T) == typeof(GraphicsDevice))
                return (T)(object)_graphicsDevice;

            throw new InvalidOperationException("Type not registered.");
        }
    }


}
