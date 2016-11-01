using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DuelIsland.Manager
{
    public static class ResourceManager
    {
        static Dictionary<string, Texture2D> textureBank = new Dictionary<string, Texture2D>();
        static Dictionary<string, SpriteFont> fontBank = new Dictionary<string, SpriteFont>();
        static Dictionary<string, SoundEffect> soundBank = new Dictionary<string, SoundEffect>();
        static ContentManager content;

        public static void Initialize(ContentManager cnt)
        {
            content = cnt;
        }

        public static Texture2D GetTexture(string assetName)
        {
            if (textureBank.ContainsKey(assetName))
            {
                return textureBank[assetName];
            }
            else
            {
                textureBank.Add(assetName, content.Load<Texture2D>(assetName));
                return textureBank[assetName];
            }
        }

        public static SpriteFont GetFont(string assetName)
        {
            if (fontBank.ContainsKey(assetName))
            {
                return fontBank[assetName];
            }
            else
            {
                fontBank.Add(assetName, content.Load<SpriteFont>(assetName));
                return fontBank[assetName];
            }
        }

        public static SoundEffect GetSound(string assetName)
        {
            if (soundBank.ContainsKey(assetName))
            {
                return soundBank[assetName];
            }
            else
            {
                soundBank.Add(assetName, content.Load<SoundEffect>(assetName));
                return soundBank[assetName];
            }
        }
    }
}
