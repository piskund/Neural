using System.Collections.Generic;

namespace NeuralNetwork.Layers
{
	internal class InputLayer
	{
        public Dictionary<double[], double[]> TrainSet { get; }

        public InputLayer()
        {
            TrainSet = new Dictionary<double[], double[]>
            {
                { new double[] { 0, 0 }, new double[] { 0, 1 } },
                { new double[] { 0, 1 }, new double[] { 1, 0 } },
                { new double[] { 1, 0 }, new double[] { 1, 0 } },
                { new double[] { 1, 1 }, new double[] { 0, 1 } }
            };
        }
	}
}