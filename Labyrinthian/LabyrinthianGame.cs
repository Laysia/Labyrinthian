using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CHMonoTools.ECS;

namespace Labyrinthian
{
	public class LabyrinthianGame : Game
    {
		public static LabyrinthianGame Game = new LabyrinthianGame();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		KeyboardState previousKeyboardState;

		public int levelCounter = 1;
		Scene screen;
		internal static TilePositionComponent PlayerPosition { get; set; }

		public static int CamerasAdded = 0;
		public static int CamerasRemoved = 0;

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
			this.screen = new Scene(this);
			this.previousKeyboardState = Keyboard.GetState();

			base.Initialize();
		}

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
			if (Keyboard.GetState().IsKeyDown(Keys.F3) && !this.previousKeyboardState.IsKeyDown(Keys.F3))
			{
				//PhysicsHitboxComponent.IsHitboxVisible = !PhysicsHitboxComponent.IsHitboxVisible;
			}
			this.previousKeyboardState = Keyboard.GetState();
			this.screen.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			this.GraphicsDevice.Clear(Color.CornflowerBlue);
			this.screen.Draw(gameTime, this.spriteBatch);
			// UI
			this.spriteBatch.Begin();
			SpriteFont spriteFont = this.Content.Load<SpriteFont>(@"Fonts/Default");
			this.spriteBatch.DrawString(spriteFont, $"FPS: {(1 / (float)gameTime.ElapsedGameTime.TotalSeconds).ToString("0.0")}", Vector2.One * 2, Color.White);
			this.spriteBatch.DrawString(spriteFont, $"Level: {this.levelCounter}", Vector2.One * 2 + new Vector2(0, 20), Color.White);
			if (PlayerPosition != null)
			{
				this.spriteBatch.DrawString(spriteFont, $"X: {PlayerPosition.ActualPosition.X}", Vector2.One * 2 + new Vector2(0, 40), Color.White);
				this.spriteBatch.DrawString(spriteFont, $"Y: {PlayerPosition.ActualPosition.Y}", Vector2.One * 2 + new Vector2(0, 60), Color.White);
				this.spriteBatch.DrawString(spriteFont, $"TileX: {PlayerPosition.TilePosition.X}", Vector2.One * 2 + new Vector2(0, 80), Color.White);
				this.spriteBatch.DrawString(spriteFont, $"TileY: {PlayerPosition.TilePosition.Y}", Vector2.One * 2 + new Vector2(0, 100), Color.White);
			}

			this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
