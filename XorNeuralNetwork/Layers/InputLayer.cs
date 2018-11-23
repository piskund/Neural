namespace NeuralNetwork.Layers
{
	internal class InputLayer
	{
		public (double[], double[])[] TrainSet { get; } =
		{
			(new double[] {0, 0}, new double[] {0, 1}),
			(new double[] {0, 1}, new double[] {1, 0}),
			(new double[] {1, 0}, new double[] {1, 0}),
			(new double[] {1, 1}, new double[] {0, 1})
		};
	}
}