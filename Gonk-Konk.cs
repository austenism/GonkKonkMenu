using Gonk_Konk.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Gonk_Konk
{
    public class GonkKonk : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;

        private Gonk mainGonk;
        private Gonk[] smallerGonks;
        private GenericSprite[] sprites;

        private Random random;
        /// <summary>
        /// contructs the game
        /// </summary>
        public GonkKonk()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "Gonk Konk";

            random = new Random(DateTime.Now.Second * DateTime.Now.Minute);


            mainGonk = new Gonk(RotationSpeed.None) { Position = new Vector2(650, 250) };
            smallerGonks = new Gonk[]{
                new Gonk((RotationSpeed)random.Next(1, 6), (float)0.25, true) { Position = new Vector2(80, 400)},
                new Gonk((RotationSpeed)random.Next(1, 6), (float)0.25, true) { Position = new Vector2(190, 400)},
                new Gonk((RotationSpeed)random.Next(1, 6), (float)0.25, true) { Position = new Vector2(300, 400)},
                new Gonk((RotationSpeed)random.Next(1, 6), (float)0.25, true) { Position = new Vector2(410, 400)}
            };

            sprites = new GenericSprite[50];

            sprites[0] = new GenericSprite("Konk", new Vector2(500, 290), 256, 408, (float)0.8, false);
            int ran;
            for(int i = 1; i < 50; i++)
            {
                ran = random.Next(1, 3);
                switch (ran)
                {
                    case 1:
                        sprites[i] = new GenericSprite("star1", new Vector2(random.Next(0, 800), random.Next(0, 480)), 3, 3);
                        break;
                    case 2:
                        sprites[i] = new GenericSprite("star2", new Vector2(random.Next(0, 800), random.Next(0, 480)), 3, 3);
                        break;
                    case 3:
                        sprites[i] = new GenericSprite("star3", new Vector2(random.Next(0, 800), random.Next(0, 480)), 3, 3);
                        break;
                }
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("ComicSans");

            mainGonk.LoadContent(Content);
            foreach(Gonk gonk in smallerGonks)
            {
                gonk.LoadContent(Content);
            }
            foreach (GenericSprite s in sprites)
            {
                s.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            //sprites layer 0
            foreach (GenericSprite gs in sprites)
            {
                gs.Draw(gameTime, _spriteBatch);
            }
            foreach (Gonk gonk in smallerGonks)
            {
                gonk.Draw(gameTime, _spriteBatch);
            }

            //words
            _spriteBatch.DrawString(spriteFont, "GONK KONK", new Vector2(65, 90), Color.Blue);
            _spriteBatch.DrawString(spriteFont, "press ESC or B button to exit", new Vector2(0, 0), Color.Blue, 0, new Vector2(0,0), (float)0.25, SpriteEffects.None, 0);
            _spriteBatch.DrawString(spriteFont, "start", new Vector2(65, 200), Color.Blue, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 0);

            //sprites layer 1
            mainGonk.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
