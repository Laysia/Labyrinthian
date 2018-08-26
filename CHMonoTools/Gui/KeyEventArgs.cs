using Microsoft.Xna.Framework.Input;
using System;

namespace CHMonoTools.Gui
{
	public class KeyEventArgs : EventArgs
	{
		public Keys Key { get; set; }

		public KeyEventArgs(Keys Key)
		{
			this.Key = Key;
		}
	}
}