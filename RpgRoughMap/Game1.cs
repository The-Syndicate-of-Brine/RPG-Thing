using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgRoughMap
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenInt;
        int screenIntX;
        int screenIntY;
        Texture2D white;
        Texture2D[,] map;
        Texture2D[,] area;
        Texture2D[,] dunOne;
        Rectangle doorRec;

        Hero playerOne;

        List<Mob> mobs;
        List<Mob> activeMobs;
        List<Building> buildings;
        List<Building> activeBuildings;
        bool areaLoad;

        GamePadState pad1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Texture2D[3,3];
            dunOne = new Texture2D[3, 3];
            mobs = new List<Mob>();
            activeMobs = new List<Mob>();
            buildings = new List<Building>();
            activeBuildings = new List<Building>();
            areaLoad = false;

            screenIntX = 1;
            screenIntY = 1;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            map[0,0] = Content.Load<Texture2D>("MapTopLeftPNG");
            map[0,1] = Content.Load<Texture2D>("MapTopPNG");
            map[0,2] = Content.Load<Texture2D>("MapTopRightPNG");
            map[1,0] = Content.Load<Texture2D>("MapLeftPNG");
            map[1,1] = Content.Load<Texture2D>("MapCentrePNG");
            map[1,2] = Content.Load<Texture2D>("MapRightPNG");
            map[2,0] = Content.Load<Texture2D>("MapBottomLeftPNG");
            map[2,1] = Content.Load<Texture2D>("MapBottomPNG");
            map[2,2] = Content.Load<Texture2D>("MapBottomRightPNG");
            

            area = map;
            
            dunOne[0, 0] = Content.Load<Texture2D>("MapTopLeftPNG");
            dunOne[0, 1] = Content.Load<Texture2D>("MapTopPNG");
            dunOne[0, 2] = Content.Load<Texture2D>("MapTopRightPNG");
            dunOne[1, 0] = Content.Load<Texture2D>("MapLeftPNG");
            dunOne[1, 1] = Content.Load<Texture2D>("MapCentrePNG");
            dunOne[1, 2] = Content.Load<Texture2D>("MapRightPNG");
            dunOne[2, 0] = Content.Load<Texture2D>("MapBottomLeftPNG");
            dunOne[2, 1] = Content.Load<Texture2D>("Room1");
            dunOne[2, 2] = Content.Load<Texture2D>("MapBottomRightPNG");

            playerOne = new Hero(Content.Load<Texture2D>("white"), new Rectangle(400, 250, 32, 32),Content.Load<Texture2D>("Sword"),Content.Load<Texture2D>("walkForwards"),Content.Load<Texture2D>("walkBack"));

            white = Content.Load<Texture2D>("white");

            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 0,Color.Red,map));
            mobs.Add(new Mob(white, new Rectangle(350, 200, 20, 20), 0,Color.Blue,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 1,Color.Red,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 3,Color.Red,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 6,Color.Blue,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 5,Color.Red,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 8,Color.Red,map));
            mobs.Add(new Mob(white, new Rectangle(400, 250, 20, 20), 2,Color.Blue,map));
            mobs.Add(new Mob(white, new Rectangle(350, 200, 20, 20), 2,Color.Red,map));


            buildings.Add(new Building(white, new Rectangle(0, 0, 300, 200), 0, map));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            pad1 = GamePad.GetState(PlayerIndex.One);

            playerOne.UpdateMe(pad1,screenIntX,screenIntY,gameTime,activeBuildings);

            if (playerOne.Rec.X > 800)
            {
                screenIntX += 1;
                playerOne.Rec.X = 0;
                areaLoad = true;
            }
            if (playerOne.Rec.X < 0)
            {
                screenIntX -= 1;
                playerOne.Rec.X = 780;
                areaLoad = true;
            }
            if (playerOne.Rec.Y > 500)
            {
                screenIntY += 1;
                playerOne.Rec.Y = 0;
                areaLoad = true;
            }
            if (playerOne.Rec.Y < 0)
            {
                screenIntY -= 1;
                playerOne.Rec.Y = 480;
                areaLoad = true;
            }
            
            screenInt=ScreenCheck(screenIntX, screenIntY);
            if ((screenInt == 1) && (area==map))
            {
                doorRec = new Rectangle(400, 20, 20, 40);
                
            }else if((screenInt == 7) && (area == dunOne))
            {
                doorRec = new Rectangle(400, 400, 20, 40);
                
            }else
            {
                doorRec = new Rectangle(0, 0, 0, 0);
            }
            if (playerOne.Rec.Intersects(doorRec))
            {
                if (area == map)
                {
                    screenIntX = 1;
                    screenIntY = 2;
                    area = dunOne;
                    playerOne.Rec.Y = 370;
                }
                else if (area == dunOne)
                {
                    screenIntX = 1;
                    screenIntY = 0;
                    area = map;
                    playerOne.Rec.Y = 50;
                }
            }

            if (areaLoad == true)
            {
                activeMobs.Clear();
                activeBuildings.Clear();
                for(int i = 0; i < mobs.Count; i++)
                {
                    if ((mobs[i].ScreenNoX == screenInt)&&(mobs[i].Area==area))
                    {
                        activeMobs.Add(mobs[i]);
                    }
                }
                for(int i = 0; i < buildings.Count; i++)
                {
                    if ((buildings[i].ScreenNo == screenInt) && (buildings[i].Area == area))
                    {
                        activeBuildings.Add(buildings[i]);
                    }
                }
                areaLoad = false;
            }

            for (int i = 0; i < activeMobs.Count; i++)
            {
                if (screenInt == activeMobs[i].ScreenNoX)
                {
                    activeMobs[i].UpdateMe(gameTime);
                    if (activeMobs[i].Rec.Intersects(playerOne.Rec)&&playerOne.invinsibility==false)
                    {
                        playerOne.health -= 10;
                        playerOne.invinsibility = true;
                        activeMobs[i].BounceBack(gameTime);
                    }
                    if (playerOne.swordRec.Intersects(activeMobs[i].Rec))
                    {
                        activeMobs[i].health -= 10;
                        activeMobs[i].BounceBack(gameTime);
                        
                        if (activeMobs[i].health <= 0)
                        {
                           activeMobs.Remove(activeMobs[i]);
                        }
                        
                    }
                }
            }
            
            base.Update(gameTime);
        }

        private int ScreenCheck(int screenIntX, int screenIntY)
        {
            int screen = 0;
            switch (screenIntX)
            {
                case 0:
                    switch (screenIntY)
                    {
                        case 0:
                            screen = 0;
                            break;
                        case 1:
                            screen = 3;
                            break;
                        case 2:
                            screen = 6;
                            break;
                    }
                    break;
                case 1:
                    switch (screenIntY)
                    {
                        case 0:
                            screen = 1;
                            break;
                        case 1:
                            screen = 4;
                            break;
                        case 2:
                            screen = 7;
                            break;
                    }
                    break;
                case 2:
                    switch (screenIntY)
                    {
                        case 0:
                            screen = 2;
                            break;
                        case 1:
                            screen = 5;
                            break;
                        case 2:
                            screen = 8;
                            break;
                    }
                    break;
            }
            return screen;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(area[screenIntY,screenIntX], new Rectangle(0, 0, 800, 500), Color.White);

            spriteBatch.Draw(Content.Load<Texture2D>("white"), doorRec, Color.Brown);

            playerOne.DrawMe(spriteBatch);
            
            for (int i = 0; i < activeMobs.Count; i++)
            {
                
                    activeMobs[i].DrawMe(spriteBatch);
                
            }
            for(int i = 0; i < activeBuildings.Count; i++)
            {
                activeBuildings[i].DrawMe(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
