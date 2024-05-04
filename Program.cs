// See https://aka.ms/new-console-template for more information
using Perceptron;
Console.WriteLine("Hello, World!");
int[] tt = [0, 0, 0, 1];
int[][] teste = [[0, 0], [0, 1], [1, 0], [1,1]];
Neuron net = new Neuron(2, 100, 0.3);
net.i_train(tt);
foreach (double i in net.pesos) {
    Console.WriteLine(i);
}
Console.WriteLine("Predict: "+net.predict([0,1]));
Console.WriteLine();
/*0,23195593991873592,0,3620496635148534,-1,2364597124217356
*/