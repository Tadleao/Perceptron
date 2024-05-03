// See https://aka.ms/new-console-template for more information
using Perceptron;
Console.WriteLine("Hello, World!");
int[] tt = [-1,-1,-1, 1];
Neuron net = new Neuron(2, 1000, 0.1);
double[] tt2 = [2.326332624967365,2.337650306028151,-3.4944895327531214]; //pesos and
net.itrain(tt);
//net.setPesos(tt2);
foreach(double i in net.pesos) { Console.WriteLine(i); }
Console.WriteLine("Predict: "+net.predict([1,0]));
Console.WriteLine();
/*0,23195593991873592,0,3620496635148534,-1,2364597124217356
*/