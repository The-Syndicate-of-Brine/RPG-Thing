using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgRoughMap
{
    class Mob
    {
        Texture2D Tex;
        public Rectangle Rec;
        public int ScreenNoX;
        float moveSwitch;
        Vector2 move;
        public int health;
        Color MobCol;
        public Texture2D[,] Area;

        public Mob(Texture2D tex, Rectangle rec, int screenNoX, Color mobCol, Texture2D[,] area)
        {
            Area = area;
            Tex = tex;
            Rec = rec;
            ScreenNoX = screenNoX;
            moveSwitch = 1.2f;
            move = new Vector2(5, 0);
            MobCol = mobCol;
            if (MobCol == Color.Blue)
            {
                health = 30;
            }
            else
            {
                health = 10;
            }
        }

        public void UpdateMe(GameTime gt)
        {

            moveSwitch -= (float)gt.ElapsedGameTime.TotalSeconds;
            if (moveSwitch < 0)
            {
                move = -move;
                moveSwitch = 1.2f;
            }
            Rec.X += (int)move.X;
            Rec.Y += (int)move.Y;
        }

        public void BounceBack(GameTime gt)
        {
            move = -move;
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(Tex, Rec, MobCol);
        }
    }
}
