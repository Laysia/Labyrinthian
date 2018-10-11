namespace CHMonoTools.ECS
{
	public class RigidBodyComponent : Component
	{
		public BodyType BodyType { get; set; }
		public bool CollisionEnabled { get; set; } = true;
		public RigidBodyComponent(BodyType bodyType)
		{
			this.BodyType = bodyType;
		}
	}

	/// <summary>
	/// Defines the different Types of RigidBodies
	/// </summary>
	public enum BodyType
	{
		/// <summary>
		/// Immovable Body
		/// </summary>
		Static,
		/// <summary>
		/// Movable Body
		/// </summary>
		Dynamic
	}
}
