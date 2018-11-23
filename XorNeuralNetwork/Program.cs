namespace NeuralNetwork
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var net = new Network();
			net.Train();
			net.Test();
		}
	}
}