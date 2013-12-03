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
using Nuclex;

namespace GUILib
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        GUILib Menu;
        GUILib.Button B;

        protected override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Menu = new GUILib(Keyboard.GetState(),spriteBatch);
            B = new GUILib.Button(50, 50, 100, 50, new Texture2D[] { Content.Load<Texture2D>("One"), Content.Load<Texture2D>("Two"), Content.Load<Texture2D>("Three") });
            GUILib.Button BB = new GUILib.Button(250, 50, 100, 50, new Texture2D[] { Content.Load<Texture2D>("One"), Content.Load<Texture2D>("Two"), Content.Load<Texture2D>("Three") });
            
            B.Clicked += delegate()
            { Console.WriteLine("Clicked"); };

            B.MouseArmed += delegate()
            { Console.WriteLine("Armed"); };

            B.MouseEnter += delegate()
            { Console.WriteLine("Mouse enter"); };

            B.MouseLeave += delegate()
            { Console.WriteLine("Mouse left"); };

            BB.Clicked += delegate()
            { Console.WriteLine("Clicked2"); };

            BB.MouseArmed += delegate()
            { Console.WriteLine("Armed2"); };

            BB.MouseEnter += delegate()
            { Console.WriteLine("Mouse enter2"); };

            BB.MouseLeave += delegate()
            { Console.WriteLine("Mouse left2"); };

            Menu.AddComponent(B);
            Menu.AddComponent(BB);

            GUILib.Textbox T = new GUILib.Textbox(200,200,400,60,new Texture2D[] { Content.Load<Texture2D>("TextOne"), Content.Load<Texture2D>("TextTwo")},Content.Load<SpriteFont>("SpriteFont1"),"");
            Menu.AddComponent(T);
            Controls.AddControl("Up", Keys.W);
        }

        protected override void UnloadContent()
        {
        }

        public static string Lel = "";

        protected override void Update(GameTime gameTime)
        {
            LeoHelper.DoAllUpdateStarts(Mouse.GetState(), Keyboard.GetState());
            Menu.StartUpdate(Keyboard.GetState());

            if (Controls.GetKeyState("Up").New)
            {
                Console.WriteLine("You are holding the up control");
            }

            LeoHelper.DoAllUpdateEnds();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            
            Menu.DoRender();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
