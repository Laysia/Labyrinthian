using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CHMonoTools.Gui
{
	public class Button : MonoControl
	{
		private MouseState currentMouse;
		private MouseState previousMouse;

		/// <summary>
		/// If true this control listens to input
		/// </summary>
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (!value)
				{
					this.isHovering = false;
				}
				this.enabled = value;
			}
		}
		/// <summary>
		/// If true this control listens to keyboard input
		/// </summary>
		public bool IsFocused { get; set; }
		/// <summary>
		/// If true the Control will be drawn
		/// </summary>
		public bool Visible { get; set; } = true;
		public String Text { get; set; } = "Button";
		/// <summary>
		/// List of Keyboard Keys that are checked for input. Default has only Keys.Enter
		/// </summary>
		public List<Keys> RecognizedKeys { get; private set; } = new List<Keys>() { Keys.Enter };

		public Vector2 Position { get; set; }
		public Texture2D Texture { get; set; }
		public Texture2D FocusedTexture { get; set; }
		public Texture2D PressedTexture { get; set; }
		public SpriteFont Font { get; set; }
		public ITransformer Transformer { get; set; }
		public Rectangle Bounds
		{
			get
			{
				return this.Texture.Bounds;
			}
		}

		public Color Color { get; private set; } = Color.White;

		private bool isHovering = false;
		private bool isPressing = false;
		private Keys keyBeingPressed = Keys.None;
		private bool isRightClickingStarted;
		private bool isLeftClickingStarted = false;
		private bool isMiddleClickingStarted = false;
		private bool enabled = true;

		public event EventHandler Click;
		public event EventHandler RightClick;
		public event EventHandler MiddleClick;
		public event EventHandler MouseEnter;
		public event EventHandler MouseLeave;
		public event EventHandler MouseDown;
		public event EventHandler MouseUp;

		public Button(SpriteFont Font, Texture2D Texture)
		{
			this.Texture = Texture;
			this.Font = Font;
		}

		public Button(SpriteFont Font, Texture2D Texture, Vector2 Position) : this(Font, Texture)
		{
			this.Position = Position;
		}

		public Button(SpriteFont Font, Texture2D Texture, ITransformer Transformer) : this(Font, Texture)
		{
			this.currentMouse = Mouse.GetState();
			this.previousMouse = this.currentMouse;
			this.Transformer = Transformer;
		}

		public Button(SpriteFont Font, Texture2D Texture, Vector2 Position, ITransformer Transformer) : this(Font, Texture, Transformer)
		{
			this.Position = Position;
		}

		public new void Initialize()
		{

		}

		public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!this.Visible)
			{
				return;
			}

			if (this.isPressing)
			{
				spriteBatch.Draw(this.PressedTexture ?? this.Texture, this.Bounds, this.Color);
			}
			else if (this.IsFocused ||this.isHovering)
			{
				spriteBatch.Draw(this.FocusedTexture ?? this.Texture, this.Bounds, this.Color);
			}
			else
			{
				spriteBatch.Draw(this.Texture, this.Bounds, this.Color);
			}

			spriteBatch.DrawString(this.Font, this.Text, this.Bounds.Center.ToVector2()	- this.Font.MeasureString(this.Text).ToPoint().ToVector2() / 2, this.Color);
		}

		public new void Update(GameTime gameTime)
		{
			this.currentMouse = Mouse.GetState();

			if (this.Enabled)
			{
				// Keyboard

				checkKeyboard();

				// Mouse
				checkMouse();
			}

			this.previousMouse = this.currentMouse;
		}

		private void checkMouse()
		{
			Matrix invertedTransform = Matrix.Invert(this.Transformer?.TransformationMatrix ?? new Matrix());
			if (this.Bounds.Contains(this.currentMouse.TransformedPosition(invertedTransform)))
			{
				if (!this.isHovering)
				{
					this.isHovering = true;
					this.MouseEnter?.Invoke(this, EventArgs.Empty);
				}
				// Left Click
				if (this.currentMouse.LeftButton == ButtonState.Pressed)
				{
					if (this.previousMouse.LeftButton == ButtonState.Released)
					{
						this.isPressing = true;
						this.isLeftClickingStarted = true;
						this.MouseDown?.Invoke(this, EventArgs.Empty);
					}
				}
				else if (this.currentMouse.LeftButton == ButtonState.Released)
				{
					if (this.previousMouse.LeftButton == ButtonState.Pressed)
					{
						this.isPressing = false;
						if (this.isLeftClickingStarted)
						{
							this.Click?.Invoke(this, EventArgs.Empty);
							this.isLeftClickingStarted = false;
						}
						this.MouseUp?.Invoke(this, EventArgs.Empty);
					}
				}
				// Right Click
				if (this.currentMouse.RightButton == ButtonState.Pressed)
				{
					if (this.previousMouse.RightButton == ButtonState.Released)
					{
						this.isPressing = true;
						this.isRightClickingStarted = true;
						this.MouseDown?.Invoke(this, EventArgs.Empty);
					}
				}
				else if (this.currentMouse.RightButton == ButtonState.Released)
				{
					if (this.previousMouse.RightButton == ButtonState.Pressed)
					{
						this.isPressing = false;
						if (this.isRightClickingStarted)
						{
							this.RightClick?.Invoke(this, EventArgs.Empty);
							this.isRightClickingStarted = false;
						}
						this.MouseUp?.Invoke(this, EventArgs.Empty);
					}
				}
				// Middle Click
				if (this.currentMouse.MiddleButton == ButtonState.Pressed)
				{
					if (this.previousMouse.MiddleButton == ButtonState.Released)
					{
						this.isPressing = true;
						this.isMiddleClickingStarted = true;
						this.MouseDown?.Invoke(this, EventArgs.Empty);
					}
				}
				else if (this.currentMouse.MiddleButton == ButtonState.Released)
				{
					if (this.previousMouse.MiddleButton == ButtonState.Pressed)
					{
						this.isPressing = false;
						if (this.isMiddleClickingStarted)
						{
							this.MiddleClick?.Invoke(this, EventArgs.Empty);
							this.isMiddleClickingStarted = false;
						}
						this.MouseUp?.Invoke(this, EventArgs.Empty);
					}
				}
			}
			else
			{
				if (this.isHovering)
				{
					this.isHovering = false;
					this.isLeftClickingStarted = false;
					this.isRightClickingStarted = false;
					this.isMiddleClickingStarted = false;
					this.MouseLeave?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void checkKeyboard()
		{
			if (this.IsFocused)
			{
				if (this.isPressing)
				{
					if (this.keyBeingPressed != Keys.None)
					{
						if (Keyboard.GetState().IsKeyUp(this.keyBeingPressed))
						{
							this.isPressing = false;
							this.Click?.Invoke(this, new KeyEventArgs(this.keyBeingPressed));
							this.keyBeingPressed = Keys.None;
						}
					}
				}
				else
				{
					foreach (var key in this.RecognizedKeys)
					{
						if (Keyboard.GetState().IsKeyDown(key))
						{
							this.isPressing = true;
							this.keyBeingPressed = key;
							break;
						}
					}
				}
			}
		}
	}
}
