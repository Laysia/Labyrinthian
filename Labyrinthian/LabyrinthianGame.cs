using Labyrinthian.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	public class LabyrinthianGame : Game
    {
		public static LabyrinthianGame Game = new LabyrinthianGame();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		//Level currentlevel;
		int levelCounter = 0;
		Screen screen;
		internal static PositionComponent playerPosition { get; set; }

        public LabyrinthianGame()
        {
			this.graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
			this.Window.Title = "Labyrinthian";
			this.IsMouseVisible = true;
			this.Window.AllowUserResizing = true;
			this.screen = new Screen(this);
            base.Initialize();
			//createNewLevel();
		}

		//private void createNewLevel()
		//{
		//	this.currentlevel = new Level(this);
		//	this.currentlevel.Initialize();
		//	this.currentlevel.LevelComplete += level_LevelComplete;
		//	this.levelCounter++;
		//}

		//private void level_LevelComplete(object sender, EventArgs e)
		//{
		//	createNewLevel();
		//}

		protected override void LoadContent()
        {
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			this.Content.Load<Texture2D>(@"Textures/PlayerFemale");
			this.Content.Load<Texture2D>(@"Textures/GroundSheet");
			this.Content.Load<Texture2D>(@"Textures/Torch");
			this.Content.Load<SpriteFont>(@"Fonts/Default");
		}

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			this.screen.Update(gameTime);

			//this.currentlevel.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			this.GraphicsDevice.Clear(Color.CornflowerBlue);
			//this.currentlevel.Draw(gameTime, this.spriteBatch);
			this.screen.Draw(gameTime, this.spriteBatch);
			// UI
			this.spriteBatch.Begin();
			SpriteFont spriteFont = this.Content.Load<SpriteFont>(@"Fonts/Default");
			this.spriteBatch.DrawString(spriteFont, $"Level: {this.levelCounter}", Vector2.One * 10, Color.White);
			if (playerPosition != null)
			{
				this.spriteBatch.DrawString(spriteFont, $"X: {playerPosition.Position.X}", Vector2.One * 10 + new Vector2(0, 20), Color.White);
				this.spriteBatch.DrawString(spriteFont, $"Y: {playerPosition.Position.Y}", Vector2.One * 10 + new Vector2(0, 40), Color.White);
			}
			this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
