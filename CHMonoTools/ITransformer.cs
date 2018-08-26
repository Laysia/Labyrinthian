using Microsoft.Xna.Framework;

namespace CHMonoTools
{
	public interface ITransformer
	{
		Matrix TransformationMatrix { get; }
	}
}
