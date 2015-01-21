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
    class BulletReg
    {
        public Vector2 pos;
        Texture2D texture;
        public Rectangle shotR
        {
            get;
            set;
        }
        public int smokeTimer = 5;
        public float timer = 40;
        public float speed;
        public BulletReg(Vector2 pos, Texture2D texture)
        {
            this.pos = pos;
            this.texture = texture;
            this.speed = 10;
            this.shotR = new Rectangle(
            (int)pos.X,
            (int)pos.Y,
            texture.Width,
            texture.Height);
        }
        public void update()
        {

            timer --;
            if (timer <= 0) speed = 0;
            pos.X += speed;
            shotR = new Rectangle(
            (int)pos.X,
            (int)pos.Y,
            texture.Width,
            texture.Height);
            if (smokeTimer <= 0)
            {
                
            }
        }
        public void draw(SpriteBatch sprites)
        {
            sprites.Draw(texture, pos, Color.White);
            
        }
    }
}
