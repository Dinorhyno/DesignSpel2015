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
    public class Smoke
    {
        Vector2 pos;
        Texture2D texture;
        public int timer = 5;
        public Smoke(Vector2 pos, Texture2D texture)
        {
            this.pos = pos;
            this.texture = texture;
        }
        public void Update()
        {
            timer--;
        }
        public void draw(SpriteBatch sprites)
        {
            sprites.Draw(texture, pos,null, Color.White, timer,
        new Vector2(texture.Width / 2, texture.Height / 2), timer / 3f, SpriteEffects.None, 0f);
        }
    }
}
//new Vector2(pos.X - texture.Width / 2, pos.Y - texture.Height / 2)