﻿namespace CHMonoTools.ECS
{
	public interface IComponent : IUpdateable, IInitializable
	{
		Entity Entity { get; set; }
	}
}
