using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Popper
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {   GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle w;
        Texture2D upt;
        Texture2D pt;
        Random r;
        List<Rectangle> k;
        List<Vector2> v;
        List<Texture2D> pics;
        List<int> t;
        int teme;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {   int sw = graphics.GraphicsDevice.Viewport.Width;
            int sh = graphics.GraphicsDevice.Viewport.Height;
            w = new Rectangle(0, 0, sw, sh);
            teme = 0;
            r = new Random();
            pics = new List<Texture2D>();
            k = new List<Rectangle>();
            v = new List<Vector2>();
            t = new List<int>();
            k.Add(new Rectangle(240, 120, 20, 20));
            v.Add(new Vector2(4, 5));
            t.Add(0);
            k.Add(new Rectangle(230, 180, 20,20));
            v.Add(new Vector2(4, 5));
            t.Add(0);
            k.Add(new Rectangle(250, 190, 20, 20));
            v.Add(new Vector2(4, 5));
            t.Add(0);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            upt = Content.Load<Texture2D>("no poop");
            pt = Content.Load<Texture2D>("poop");
            pics.Add(upt);
            pics.Add(upt);
            pics.Add(upt);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            teme++;//time written in a funny way
            for (int i = 0; i < k.Count; i++)
            {   int y = k[i].Y + (int)v[i].Y;
                int x = k[i].X + (int)v[i].X;
                k[i] = new Rectangle(x, y, k[i].Width, k[i].Height);
                if (k[i].Y + k[i].Height >= w.Bottom || k[i].Y <= w.Top)
                {
                    v[i] = new Vector2(v[i].X, v[i].Y * -1);
                }
                if (k[i].X + k[i].Width >= w.Right || k[i].X <= w.Left)
                {
                    v[i] = new Vector2(v[i].X * -1, v[i].Y);
                }
                for (int j = i + 1; j < k.Count; j++)
                {   if (k[i].Intersects(k[j]) && t[i] == 0 && t[j] == 0)
                    {
                        t[i] = 45;
                        t[j] = 45;
                        pics[i] = pt;
                        pics[j] = pt;
                    }
                }
            }
            if (teme % 180 == 0)
            {
                int x = r.Next(800);
                int y = r.Next(480);
                int velX = r.Next(6) - 3;
                int velY = r.Next(6) - 3;
                if (velX == 0 && velY == 0)
                {
                    velX++;
                    velY--;
                }
                pics.Add(upt);
                k.Add(new Rectangle(x, y, 20, 20));
                t.Add(0);
                v.Add(new Vector2(velX, velY));
            }
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i] == 1)
                {   pics.RemoveAt(i);
                    v.RemoveAt(i);
                    k.RemoveAt(i);
                    t.RemoveAt(i);
                }
                else if (t[i] > 1)
                {
                    t[i]--;
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {   GraphicsDevice.Clear(Color.Beige);
            spriteBatch.Begin();
            for (int i = 0; i < k.Count; i++)
            {
                spriteBatch.Draw(pics[i], k[i], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}