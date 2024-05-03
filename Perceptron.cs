using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    internal class Neuron
    {
        int[] bits;
        public double[] pesos;
        int entradas;
        double epocas;
        double tda;
        int bias;
        public Neuron(int entradas = 2, double epocas = 2, double tda = 0.3, int bias = 1 ) { 
            bits = new int[entradas+1];
            pesos= new double[entradas+1];
            this.epocas = epocas;
            this.entradas = entradas;
            this.tda = tda;
            this.bias = bias;
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            for( int i = 0; i < pesos.Length; i++ ) {
                pesos[i] = Math.Round(rd.NextDouble(),2);
                bits[i] = 0;
                Console.WriteLine(pesos[i]);
            }
            bits[entradas] = bias;
        }
        public void setPesos(double[] np) {
            pesos = np;
        }
        int[] toBin(int n)
        {
            string binarioStr = Convert.ToString(n, 2); // Converte para binário como string
            int[] binario = new int[entradas+1]; // Cria um array para armazenar os dígitos binários
            int pos = 0;
            // Preenche o array com os dígitos binários
            for (int i = 0; i < entradas; i++)
            {
                if (i > entradas - binarioStr.Length - 1)
                {
                    binario[i] = int.Parse(binarioStr[pos].ToString());pos++;
                }
            }
            binario[entradas] = bias;
            return binario;
        }
        public void NetTrain(int[] tt) {
            int qtde = (int)(Math.Pow(2, entradas)*epocas);
            int var = (int)Math.Pow(2, entradas);
            double soma = 0.0;
            int[] saida = new int[var];
            bool erro = true;
            int i = 0;
            for(i = 0; i < qtde; i++)///while(erro)
            { 
                    bits = toBin(i % var);
                    for (int j = 0; j < bits.Length; j++)
                    {
                        soma += pesos[j] * bits[j];
                    }
                    if (soma > 0) { saida[i % var] = 1; } else { saida[i % var] = 0; }
                    if (saida[i % var] != tt[i % var])
                    {
                        for (int k = 0; k < pesos.Length; k++)
                        {
                            pesos[k] = Math.Round(pesos[k] + (tda * (tt[i % var] - saida[i % var]) * bits[k]),2);
                            ///Console.WriteLine("Alterado");
                        }
                    }
                erro = (tt.SequenceEqual(saida) && i % var == 0) ? false : true;
               /// i++;
                ///Console.WriteLine(i);
            }
        }
        double somar(int[] bin) {
            double soma = 0.0;
            for (int i = 0; i < bin.Length; i++) {
                soma += Math.Round(pesos[i] * bin[i], 2);
            }
            return soma;
        }
        int verify(double soma) {
            return (soma > 0 ? 1 : -1);
        }
        public void train(int[] tt) {
            bool erro = false;
            int saida = 0;
            int var = tt.Length;
            for (int i = 0; i < tt.Length; i++) {
                saida = verify(somar(toBin(i)));
                if (saida != tt[i]) {
                    erro = true;
                    for (int k = 0; k < pesos.Length; k++)
                    {
                        pesos[k] = Math.Round(pesos[k] + (tda * (tt[i % var] - saida) * bits[k]), 2);
                        ///Console.WriteLine("Alterado");
                    }
                }
            }
            if (erro) { train(tt);}
        }
        public void itrain(int[] tt) {
            bool erro = false;
            int saida = 0;
            int var = tt.Length;
            double soma = 0.0;
            do {
                erro = false;
                for (int i = 0; i < tt.Length; i++) {
                    soma = Math.Round(somar(toBin(i)),2);
                    Console.WriteLine("soma = "+soma);
                    saida = verify(soma);
                    if (saida != tt[i]) {
                        erro = true;
                        for (int k = 0; k < pesos.Length; k++)
                        {
                            pesos[k] = Math.Round(pesos[k] + (tda * (tt[i % var] - saida) * bits[k]), 3);
                            ///Console.WriteLine("Alterado");
                        }
                    }
                }
            } while (erro == true);
        }
        public void itrain_epoch(int[] tt)
        {
            bool erro = false;
            int qtde = (int)(Math.Pow(2, entradas) * epocas);
            int saida = 0;
            int var = tt.Length;
            double soma = 0.0;
                for (int i = 0; i < qtde; i++)
                {
                soma = somar(toBin(i % var));
                    saida = verify(soma);
                    if (saida != tt[i%var])
                    {
                        erro = true;
                        for (int k = 0; k < pesos.Length; k++)
                        {
                            pesos[k] = Math.Round(pesos[k] + (tda * (tt[i % var] - saida) * bits[k]), 3);
                            ///Console.WriteLine("Alterado");
                        }
                    }
                }
        }
        public int predict(int[] entrada) {
            double soma = 0.0;
            entrada = entrada.Concat([bias]).ToArray();
            for (int j = 0; j < entrada.Length; j++)
            {
                soma += pesos[j] * entrada[j];
            }
            if (soma >= 0) { return 1; }else { return 0;};
        }
    }
}
