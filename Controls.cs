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
namespace GUILib
{
    static class Controls
    {
        //Keycodes used for XNA are http://msdn.microsoft.com/en-us/library/0z084th3(v=vs.90).aspx
        //Remaind me to add in support for runtime control changing
        public static KeyboardState New; 
        public static KeyboardState Old;
        public class Entry
        {
            public Keys Key;
            public bool New;
            public bool Old;
            public bool Held() { return New; }
            public bool JustPressed() { return (New && !Old); }
            public bool JustReleased() { return (!New && Old); }
            public void StartUpdate(KeyboardState In)
            {
                New = In.IsKeyDown(Key);
            }
            public void EndUpdate()
            {
                Old = New;
            }
            public Entry(Keys K)
            {
                Key = K;
                New = false;
                Old = false;
            }
            public Entry(int Keycode)
            {
                Key = (Keys)Keycode;
                New = false;
                Old = false;
            }
        }
        static Dictionary<string, Entry> AllControls = new Dictionary<string, Entry>();
        public static void AddControl(string Name, Keys Key)
        {
            AllControls.Add(Name, new Entry(Key));
        }
        public static Entry GetKeyState(string Name)
        {
            return AllControls[Name];
        }
        public static void StartUpdate(KeyboardState KS)
        {
            New = KS;
            foreach (KeyValuePair<string, Entry> X in AllControls)
            {
                X.Value.StartUpdate(KS);
            }
        }
        public static void Init(KeyboardState KS)
        {
            New = KS;
            Old = KS;
        }
        public static void EndUpdate()
        {
            Old = New;
            foreach (KeyValuePair<string, Entry> X in AllControls)
            {
                X.Value.EndUpdate();
            }
        }
    }
}
