namespace NeuralNetwork.Layers
{
	internal class OutputLayer : Layer
	{
		public OutputLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type)
		{
		}

		public override void Recognize(Network net, Layer nextLayer)
		{
			for (var i = 0; i < Neurons.Length; ++i)
				net.fact[i] = Neurons[i].Output;
		}

		public override double[] BackwardPass(double[] errors)
		{
			var gr_sum = new double[numofprevneurons];
			for (var j = 0; j < gr_sum.Length; ++j) //вычисление градиентных сумм выходного слоя
			{
				double sum = 0;
				for (var k = 0; k < Neurons.Length; ++k)
					sum += Neurons[k].Weights[j] *
							 Neurons[k]
								 .Gradientor(errors[k],
									 Neurons[k].Derivativator(Neurons[k].Output),
									 0); //через ошибку и производную
				gr_sum[j] = sum;
			}

			for (var i = 0; i < numofneurons; ++i)
			for (var n = 0; n < numofprevneurons; ++n)
				Neurons[i].Weights[n] += learningrate *
												 Neurons[i].Inputs[n] *
												 Neurons[i]
													 .Gradientor(errors[i],
														 Neurons[i].Derivativator(Neurons[i].Output),
														 0); //коррекция весов
			return gr_sum;
		}
	}
}