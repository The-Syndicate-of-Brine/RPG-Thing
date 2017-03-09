using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgRoughMap
{
    class Building
    {
        Texture2D Tex;
        public Rectangle Rec;
        public Texture[,] Area;
        public int ScreenNo;

        public Building(Texture2D tex, Rectangle rec, int screenNo, Texture[,] area)
        {
            Tex = tex;
            Rec = rec;
            Area = area;
            ScreenNo = screenNo;
        }

        public void DrawMe(SpriteBatch sb)
        {
            sb.Draw(Tex, Rec, Color.Green);
        }
    }
}
