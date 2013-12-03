using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class GUILib
    {
        public GUILib(KeyboardState K, SpriteBatch SB)
        {
            //GameMouse = M;
            GameKeyboard = K;
            GameSpriteBatch = SB;
        }

        //MouseState GameMouse;
        KeyboardState GameKeyboard;
        SpriteBatch GameSpriteBatch;

        public delegate void GUIEvent();
        public abstract class Component
        {
            public abstract Texture2D GetCurrentTexture(); //Gets the current texure for the component.
            public abstract void Update(); // Run each update loop. Use to handle click events etc
            public abstract void RenderUpdate(KeyboardState K, SpriteBatch SB); // Run each draw loop. Use to handle events like mouseOver causing buttons to change texture etc
            public abstract event GUIEvent MouseArmed;
            public abstract event GUIEvent Clicked;
            public abstract event GUIEvent MouseEnter;
            public abstract event GUIEvent MouseLeave;
            public abstract Rectangle GetXYWidthHeight();
        }
        public void StartUpdate(KeyboardState NewK) //Put at the start of the update method. LeoMouse must be ran before
        {
            //GameMouse = NewM;
            GameKeyboard = NewK;
            foreach (Component X in Components)
            {
                X.Update();
            }
        }
        public void EndUpdate() //Put at the end of the update method
        {

        }
        public void DoRender()
        {
            foreach (Component X in Components)
            {
                X.RenderUpdate(GameKeyboard, GameSpriteBatch);
                //GameSpriteBatch.Draw(X.GetCurrentTexture(), X.GetXYWidthHeight(), Color.White);
            }
        }
        public void AddComponent(Component In)
        {
            Components.Add(In);
        }
        public List<Component> Components = new List<Component>();
        public class Textbox : Component
        {
            int StateIndex = 0;
            Rectangle ItemRectangle;
            Texture2D[] Textures;
            SpriteFont Font;
            String TextData = "";
            public Textbox(int X, int Y, int Width, int Height, Texture2D[] Textures_, SpriteFont Font_, string Data)
            { //Buttontextures: [0] = normal, [1] = hover, [2] = clicked
                ItemRectangle = new Rectangle(X, Y, Width, Height);
                Textures = Textures_;
                Font = Font_;
                TextData = Data;
            }
            public override event GUIEvent MouseArmed; //Held mousebutton clicked on it, not released
            public override event GUIEvent Clicked;
            public override event GUIEvent MouseEnter;
            public override event GUIEvent MouseLeave;
            public override Texture2D GetCurrentTexture()
            {
                return Textures[StateIndex];
            }
            public override void Update()
            {
                if (LeoMouse.JustReleased() && ItemRectangle.Contains(LeoMouse.New.X, LeoMouse.New.Y))
                {
                    StateIndex = 1;
                }
                if (LeoMouse.JustReleased() && !ItemRectangle.Contains(LeoMouse.New.X, LeoMouse.New.Y))
                {
                    StateIndex = 0;
                }
                if (StateIndex == 1)
                {
                    TextData += LeoKeyboard.Text;
                }
            }
            public override void RenderUpdate(KeyboardState K, SpriteBatch SB)
            {
                SB.Draw(GetCurrentTexture(), GetXYWidthHeight(), Color.White);
                SB.DrawString(Font, TextData, new Vector2(ItemRectangle.X, ItemRectangle.Y), Color.Black);
            }
            public override Rectangle GetXYWidthHeight()
            {
                return ItemRectangle;
            }
        }
        public class Button : Component
        {
            int StateIndex = 0;
            Rectangle ItemRectangle;
            Texture2D[] Textures;
            public Button(int X, int Y, int Width, int Height, Texture2D[] ButtonTextures)
            { //Buttontextures: [0] = normal, [1] = hover, [2] = clicked
                ItemRectangle = new Rectangle(X, Y, Width, Height);
                Textures = ButtonTextures;
            }
            public override event GUIEvent MouseArmed; //Held mousebutton clicked on it, not released
            public override event GUIEvent Clicked;
            public override event GUIEvent MouseEnter;
            public override event GUIEvent MouseLeave;
            public override Texture2D GetCurrentTexture()
            {
                return Textures[StateIndex];
            }
            public override void Update()
            {
                // Don't try and understand it, just hope it works!
                if (ItemRectangle.Contains(LeoMouse.New.X, LeoMouse.New.Y))
                {
                    if (!LeoMouse.JustReleased() && (!((LeoMouse.Held() && 
                        !LeoMouse.IsDragging()) || (LeoMouse.Held() && LeoMouse.IsDragging()
                        && ItemRectangle.Contains(LeoMouse.StartDrag.X, LeoMouse.StartDrag.Y))) 
                        && (!LeoMouse.IsDragging() || (LeoMouse.IsDragging() &&
                        ItemRectangle.Contains(LeoMouse.StartDrag.X, LeoMouse.StartDrag.Y)))))
                    {
                        if (StateIndex != 1)
                        {
                            if (StateIndex != 2)
                            {
                                MouseEnter();
                            }
                            StateIndex = 1;
                        }
                    }
                    else if ((LeoMouse.Held() && !LeoMouse.IsDragging()) || 
                        (LeoMouse.Held() && LeoMouse.IsDragging() && 
                        ItemRectangle.Contains(LeoMouse.StartDrag.X, LeoMouse.StartDrag.Y)))
                    {
                        if (StateIndex != 2)
                        {
                            StateIndex = 2;
                            MouseArmed();
                        }
                    }
                    else
                    {
                        Clicked();
                    }
                }
                else
                {
                    if (StateIndex != 0)
                    {
                        StateIndex = 0;
                        MouseLeave();
                    }
                }
            }
            public override void RenderUpdate(KeyboardState K, SpriteBatch SB)
            {
                SB.Draw(GetCurrentTexture(), GetXYWidthHeight(), Color.White);
            }
            public override Rectangle GetXYWidthHeight()
            {
                return ItemRectangle;
            }
        }
    }
}
