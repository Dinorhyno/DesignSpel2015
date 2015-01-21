using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace DesignSpel2015
{
    class Camera2D
    {
        public Matrix transform;
        Viewport view;
        public Vector2 centre;
        public bool shake;
        public Random r;
        public int shakeValue;

        public Camera2D(Viewport newView)
        {
            view = newView;
        }
        public void Update(GameTime gametime, Game1 spritePosition)
        {
            r = new Random();
            shakeValue = r.Next(-4, 4);
            if (!shake)
            centre = new Vector2(spritePosition.playerPos.X + (spritePosition.playerTex.Width /2) - 300, 0);
            if (shake)
            centre = new Vector2(spritePosition.playerPos.X + ((spritePosition.playerTex.Width / 2)) - 300 + shakeValue, 0 + shakeValue);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * 
            Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
