namespace Labyrinthian
{
	public static class Config
	{
		/// <summary>
		/// Amount of Update-Ticks per second. If less are performed the game will automatically scale movement (etc.) to compensate.
		/// Increase to speed up game, decrease to slow down game.
		/// </summary>
		/// <value>Default: 60.0f</value>
		public static float TickRate { get; set; } = 60.0f;
	}
}
