﻿using NeuralNetwork.Layers;
using static System.Math;
using static System.Console;

namespace NeuralNetwork
{
	public class Network
	{
		//массив для хранения выхода сети
		public double[] fact = new double[2];

		private readonly HiddenLayer hidden_layer = new HiddenLayer(4, 2, NeuronType.Hidden, nameof(hidden_layer));

		//все слои сети
		private readonly InputLayer input_layer = new InputLayer();
		private readonly OutputLayer output_layer = new OutputLayer(2, 4, NeuronType.Output, nameof(output_layer));

		//ошибка одной итерации обучения
		private double GetMSE(double[] errors)
		{
			double sum = 0;
			for (var i = 0; i < errors.Length; ++i)
				sum += Pow(errors[i], 2);
			return 0.5d * sum;
		}

		//ошибка эпохи
		private double GetCost(double[] mses)
		{
			double sum = 0;
			for (var i = 0; i < mses.Length; ++i)
				sum += mses[i];
			return sum / mses.Length;
		}

		//непосредственно обучение
		public void Train() //backpropagation method
		{
			const double threshold = 0.001d; //порог ошибки
			var temp_mses = new double[4]; //массив для хранения ошибок итераций
			double temp_cost = 0; //текущее значение ошибки по эпохе
			do
			{
				for (var i = 0; i < input_layer.TrainSet.Length; ++i)
				{
					//прямой проход
					hidden_layer.Data = input_layer.TrainSet[i].Item1;
					hidden_layer.Recognize(null, output_layer);
					output_layer.Recognize(this, null);
					//вычисление ошибки по итерации
					var errors = new double[input_layer.TrainSet[i].Item2.Length];
					for (var x = 0; x < errors.Length; ++x)
						errors[x] = input_layer.TrainSet[i].Item2[x] - fact[x];
					temp_mses[i] = GetMSE(errors);
					//обратный проход и коррекция весов
					double[] temp_gsums = output_layer.BackwardPass(errors);
					hidden_layer.BackwardPass(temp_gsums);
				}

				temp_cost = GetCost(temp_mses); //вычисление ошибки по эпохе
				//debugging
				WriteLine($"{temp_cost}");
			} while (temp_cost > threshold);

			//загрузка скорректированных весов в "память"
			hidden_layer.WeightInitialize(MemoryMode.Set, nameof(hidden_layer));
			output_layer.WeightInitialize(MemoryMode.Set, nameof(output_layer));
		}

		//тестирование сети
		public void Test()
		{
			for (var i = 0; i < input_layer.TrainSet.Length; ++i)
			{
				hidden_layer.Data = input_layer.TrainSet[i].Item1;
				hidden_layer.Recognize(null, output_layer);
				output_layer.Recognize(this, null);
				for (var j = 0; j < fact.Length; ++j)
					WriteLine($"{fact[j]}");
				WriteLine();
			}
		}
	}
}