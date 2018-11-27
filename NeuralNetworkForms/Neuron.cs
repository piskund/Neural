using static System.Math;

namespace NeuralNetwork
{
	internal class Neuron
	{
		private readonly NeuronType _type;
		public double[] Weights { get; set; }

		public double[] Inputs { get; set; }

		public double Output => Activator(Inputs, Weights);

		public Neuron(double[] inputs, double[] weights, NeuronType type)
		{
			_type = type;
			Weights = weights;
			Inputs = inputs;
		}

		private double Activator(double[] i, double[] w) //преобразования
		{
			double sum = 0;
			for (var l = 0; l < i.Length; ++l)
				sum += i[l] * w[l]; //линейные
			return Pow(1 + Exp(-sum), -1); //нелинейные
		}

		public double Derivativator(double outsignal)
		{
			return outsignal * (1 - outsignal);
		}

		public double Gradientor(double error, double dif, double g_sum)
		{
			return _type == NeuronType.Output ? error * dif : g_sum * dif;
		}
	}
}