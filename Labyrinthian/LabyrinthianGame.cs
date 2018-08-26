using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian
{
	public class LabyrinthianGame : Game
    {
		public static LabyrinthianGame Game = new LabyrinthianGame();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		KeyboardState previousKeyboardState;

		int levelCounter = 0;
		Screen screen;
		internal static PositionComponent PlayerPosition { get; set; }

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
			this.screen = new Screen(this);
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
				PhysicsHitboxComponent.IsHitboxVisible = !PhysicsHitboxComponent.IsHitboxVisible;
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
			this.spriteBatch.DrawString(spriteFont, $"Level: {this.levelCounter}", Vector2.One * 10, Color.White);
			if (PlayerPosition != null)
			{
				this.spriteBatch.DrawString(spriteFont, $"X: {PlayerPosition.Position.X}", Vector2.One * 10 + new Vector2(0, 20), Color.White);
				this.spriteBatch.DrawString(spriteFont, $"Y: {PlayerPosition.Position.Y}", Vector2.One * 10 + new Vector2(0, 40), Color.White);
			}
			this.spriteBatch.DrawString(spriteFont, $"Level: {LevelGameSystem.LevelCount}", Vector2.One * 10 + new Vector2(0, 80), Color.White);

			this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
