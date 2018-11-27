namespace NeuralNetwork.Layers
{
	internal class HiddenLayer : Layer
	{
		public HiddenLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type)
		{
		}

		public override void Recognize(Network net, Layer nextLayer)
		{
			var hidden_out = new double[Neurons.Length];
			for (var i = 0; i < Neurons.Length; ++i)
				hidden_out[i] = Neurons[i].Output;
			nextLayer.Data = hidden_out;
		}

		public override double[] BackwardPass(double[] gr_sums)
		{
			for (var i = 0; i < numofneurons; ++i)
			for (var n = 0; n < numofprevneurons; ++n)
				Neurons[i].Weights[n] += learningrate *
												 Neurons[i].Inputs[n] *
												 Neurons[i]
													 .Gradientor(0,
														 Neurons[i].Derivativator(Neurons[i].Output),
														 gr_sums[i]);
			return gr_sums;
		}
	}
}