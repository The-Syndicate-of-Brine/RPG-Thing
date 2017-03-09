using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgRoughMap
{
    class Hero
    {
        Texture2D Tex;
        Texture2D WalkForwards;
        Texture2D WalkBack;
        Rectangle sourceRec;
        int sourceRecX;
        int sourceRecY;
        public Rectangle Rec;
        Rectangle newRec;
        Texture2D SwordTex;
        bool attacking;
        int direction;
        public Rectangle swordRec;
        float rotation;
        Vector2 origin;
        public int health;
        public bool invinsibility;
        float invTimer;
        int speed;
        bool still;
        float aniTimer;

        public Hero(Texture2D tex, Rectangle rec, Texture2D swordTex, Texture2D walkForwards, Texture2D walkBack)
        {
            Tex = tex;
            WalkForwards = walkForwards;
            WalkBack = walkBack;
            Rec = rec;
            SwordTex = swordTex;
            direction = 0;
            origin = new Vector2(swordTex.Width / 2f, swordTex.Height / 2f);
            health = 100;
            invinsibility = false;
            invTimer = 1;
            newRec = rec;
            speed = 1;
            still = true;
            sourceRec = new Rectangle(0, 0, 320, 320);
            sourceRecX = 0;
            sourceRecY = 0;
            aniTimer = (float)(1/6);
        }

        public void UpdateMe(GamePadState pad, int screenX,int screenY,GameTime gt,List<Building> buildings)
        {
            still = true;
            newRec = Rec;
            if (invinsibility == true)
            {
                if (invTimer > 0)
                {
                    invTimer -= (float)gt.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    invinsibility = false;
                    invTimer = 1;
                }
            }
            
                if (pad.DPad.Up == ButtonState.Pressed)
                {
                    if (screenY > 0 || newRec.Y > 0)
                    {
                    still = false;
                        newRec.Y -= speed;
                        direction = 0;
                    }
                }
                if (pad.DPad.Down == ButtonState.Pressed)
                {
                    if (screenY < 2 || newRec.Y < 480)
                    {
                    still = false;
                            newRec.Y += speed;
                        direction = 1;
                    }
                }
                if (pad.DPad.Right == ButtonState.Pressed)
                {
                    if (screenX < 2 || newRec.X < 780)
                    {
                    still = false;
                            newRec.X += speed;
                        direction = 2;
                    }
                }
                if (pad.DPad.Left == ButtonState.Pressed)
                {
                    if (screenX > 0 || newRec.X > 0)
                    {
                    still = false;
                            newRec.X -= speed;
                        direction = 3;
                    }
                }
            int buildingCheck = 0;
            for (int i = 0; i < buildings.Count; i++)
            {
                
                if (newRec.Intersects(buildings[i].Rec))
                {
                    buildingCheck++;
                }
                
            }
            if (buildingCheck == 0)
            {
                Rec = newRec;
            }

            if (pad.Triggers.Right == 1f)
            {
                attacking = true;
                switch (direction)
                {
                    case 0:
                        swordRec = new Rectangle(Rec.X, Rec.Y - 10, 20, 20);
                        rotation = 0;
                        break;
                    case 1:
                        swordRec = new Rectangle(Rec.X+20, Rec.Y + 30, 20, 20);
                        rotation = 3;
                        break;
                    case 2:
                        swordRec = new Rectangle(Rec.X+30, Rec.Y, 20, 20);
                        rotation = 1.5f;
                        break;
                    case 3:
                        swordRec = new Rectangle(Rec.X-10, Rec.Y+20, 20, 20);
                        rotation = 4.5f;
                        break;
                        
                }
            }
            else
            {
                attacking = false;
                swordRec = new Rectangle(0, 0, 0, 0);
            }
            if(still == false)
            {
                if (aniTimer <= 0)
                {
                    sourceRec = new Rectangle(320 * sourceRecX,320* sourceRecY, 320, 320);
                    sourceRecX++;
                    if (sourceRecX > 2)
                    {
                        sourceRecY++;
                        if (sourceRecY > 2)
                        {
                            sourceRecY = 0;
                        }
                        sourceRecX = 0;
                    }
                    aniTimer = (float)(1 / 6);
                }else
                {
                    aniTimer -= (float)gt.ElapsedGameTime.TotalSeconds;
                }
                
            }
            else
            {
                sourceRecX = 0;
                sourceRecY = 0;
                aniTimer = (float)(1 / 6);
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            if (invinsibility == false)
            {
                if (still == true)
                {
                    sb.Draw(WalkForwards, Rec, new Rectangle(0, 0, 320, 320), Color.White);
                }
                else if (direction == 1)
                {
                    sb.Draw(WalkForwards, Rec, sourceRec, Color.White);
                }else if (direction == 0)
                {
                    sb.Draw(WalkBack, Rec, sourceRec, Color.White);
                }else
                {
                    sb.Draw(Tex, Rec, Color.White);
                }
            }else
            {
                sb.Draw(Tex, Rec, Color.Green);
            }
            if (attacking == true)
            {
                sb.Draw(SwordTex, swordRec,null, Color.White,rotation,origin,SpriteEffects.None,0f);
            }
            sb.Draw(Tex, new Rectangle(0, 0, 100, 10), Color.Red);
            sb.Draw(Tex, new Rectangle(0, 0, health, 10), Color.Green);
        }
    }
}
