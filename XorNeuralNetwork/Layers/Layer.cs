using System;
using System.Globalization;
using System.Xml;

namespace NeuralNetwork.Layers
{
	internal abstract class Layer
	{
		protected const double learningrate = 0.1d; //скорость обучения
		protected int numofneurons; //число нейронов текущего слоя
		protected int numofprevneurons; //число нейронов предыдущего слоя
		public Neuron[] Neurons { get; set; }

		public double[] Data //я подал null на входы нейронов, так как
		{
			//сначала нужно будет преобразовать информацию
			set //(видео, изображения, etc.)
			{
				//а загружать input'ы нейронов слоя надо не сразу,
				for (var i = 0; i < Neurons.Length; ++i)
					Neurons[i].Inputs = value;
			} //а только после вычисления выходов предыдущего слоя
		} //type используется для связи с одноимённым полю слоя файлом памяти

		protected Layer(int non, int nopn, NeuronType nt, string type)
		{
			//увидите это в WeightInitialize
			numofneurons = non;
			numofprevneurons = nopn;
			Neurons = new Neuron[non];
			var Weights = WeightInitialize(MemoryMode.Get, type);
			for (var i = 0; i < non; ++i)
			{
				var temp_weights = new double[nopn];
				for (var j = 0; j < nopn; ++j)
					temp_weights[j] = Weights[i, j];
				Neurons[i] = new Neuron(null, temp_weights, nt); //про подачу null на входы ниже
			}
		}

		public double[,] WeightInitialize(MemoryMode mm, string type)
		{
			var _weights = new double[numofneurons, numofprevneurons];
			Console.WriteLine($"{type} weights are being initialized...");
			var memory_doc = new XmlDocument();
			memory_doc.Load($"{type}_memory.xml");
			var memory_el = memory_doc.DocumentElement;
			switch (mm)
			{
				case MemoryMode.Get:
					for (var l = 0; l < _weights.GetLength(0); ++l)
					for (var k = 0; k < _weights.GetLength(1); ++k)
						_weights[l, k] =
							double.Parse(memory_el.ChildNodes.Item(k + _weights.GetLength(1) * l).InnerText.Replace(',', '.'),
								CultureInfo.InvariantCulture); //parsing stuff
					break;
				case MemoryMode.Set:
					for (var l = 0; l < Neurons.Length; ++l)
					for (var k = 0; k < numofprevneurons; ++k)
						memory_el.ChildNodes.Item(k + numofprevneurons * l).InnerText = Neurons[l].Weights[k].ToString();
					break;
			}

			memory_doc.Save($"{type}_memory.xml");
			Console.WriteLine($"{type} weights have been initialized...");
			return _weights;
		}

		public abstract void Recognize(Network net, Layer nextLayer); //для прямых проходов
		public abstract double[] BackwardPass(double[] stuff); //и обратных
	}
}