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
    class Civilian
    {
        public Vector2 pos;
        public Texture2D tex;
        public int direction;
        Random r = new Random();
        
        public void civilian(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;
            this.direction = r.Next(0, 2);
            if (direction == 0)
            {
                pos.X = 16000;
            }
            else
            {
                pos.X = 0;
            }
        }
        public void Update()
        {
            if (direction == 0)
            {
                pos.X--;
            }
            else
            {
                pos.X++;
            }
        }
    }
}
