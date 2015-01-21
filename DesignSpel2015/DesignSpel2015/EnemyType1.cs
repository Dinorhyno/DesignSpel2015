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
    class EnemyType1
    {
        public Vector2 pos
        {
            get;
            set;
        }
        public Vector2 playerPos
        {
            get;
            set;
        }
        Texture2D tex;
        public Rectangle eR 
        { 
            get; 
            set; 
        }
        public bool hit = false;
        public int Espeed;
        public int enemyHp = 50;
        public bool Activated = false;
        public bool burned = false;
        public int burnTime = 50;
        public int burnFreq = 5;
        public bool stunned = false;
        public int stunTime;
        public float direction;
        public EnemyType1(Vector2 pos, Texture2D tex, Vector2 playerPos)
        {
            this.pos = pos;
            this.playerPos = playerPos;
            this.tex = tex;
            this.Espeed = 1; 
            this.eR = new Rectangle(
        (int)pos.X,
        (int)pos.Y,
        tex.Width,
        tex.Height);
        }

        public void update()
        {
            this.eR = new Rectangle(
            (int)pos.X,
            (int)pos.Y,
            tex.Width,
            tex.Height);
            float DistanceX = pos.X - playerPos.X;
            float DistanceY = pos.Y - playerPos.Y;
            direction = (float)Math.Atan2(DistanceX, DistanceY);
            if (Activated == true && stunned == false)
            {
                
            }
            if (stunned == true)
            {
                stunTime--;
            }
            if (stunTime <= 0)
            {
                stunned = false;
            }
            if (burned == true)
            {
                burnFreq--;
                if (burnFreq <= 0)
                {
                    enemyHp--;
                    burnFreq = 5;
                }
                burnTime--;
                if (burnTime <= 0) 
                {
                    burned = false;
                    burnTime = 30;
                }
            }

        }
        public void draw(SpriteBatch sprites, SpriteFont GameFont)
        {
            if (Activated == false) sprites.Draw(tex, pos, Color.White);
            else sprites.Draw(tex, pos, Color.Blue);
            sprites.DrawString(GameFont, "hp: " + enemyHp, new Vector2(pos.X + tex.Width/ 2 - 35, pos.Y - 32), Color.Red);
        }
    }
}
