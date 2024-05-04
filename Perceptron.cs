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
            //Geração de pesos aleatórios
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            for( int i = 0; i < pesos.Length; i++ ) {
                pesos[i] = rd.NextDouble();
                bits[i] = 0;
            }
            bits[entradas] = bias;
        }
        //Setar pesos
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
        //Efetuar soma do neurônio baseado na entrada
        double somarBits() {
            double soma = 0.0;
            for (int i = 0; i < bits.Length; i++) {
                soma += pesos[i] * bits[i];
            }
            return soma;
        }
        //Verificar a saída (Classificacao do neuronio)
        int verify(double soma) {
            return (soma > 0 ? 1 : 0);
        }
        //Treino recursivo portas logicas ate aprender
        public void rec_train(int[] tt) {
            bool erro = false;
            int saida = 0;
            int var = tt.Length;
            for (int i = 0; i < tt.Length; i++) {
                bits = toBin(i);
                saida = verify(somarBits());
                if (saida != tt[i]) {
                    erro = true;
                    for (int k = 0; k < pesos.Length; k++)
                    {
                        pesos[k] = pesos[k] + (tda * (tt[i % var] - saida) * bits[k]);
                    }
                }
            }
            if (erro) { rec_train(tt);}
        }
        //Treino iterativo portas logicas ate aprender
        public void i_train(int[] tt) {
            bool erro = false;
            int saida = 0;
            int var = tt.Length;
            double soma = 0.0;
            do {
                erro = false;
                for (int i = 0; i < tt.Length; i++) {
                    bits = toBin(i);
                    saida = verify(somarBits());
                    if (saida != tt[i]) {
                        erro = true;
                        for (int k = 0; k < pesos.Length; k++)
                        {
                            pesos[k] = pesos[k] + (tda * (tt[i % var] - saida) * bits[k]);
                            ///Console.WriteLine("Alterado");
                        }
                    }
                }
            } while (erro == true);
        }
        //Treino iterativo portas logicas por epocas
        public void i_train_epoch(int[] tt)
        {
            int qtde = (int)(Math.Pow(2, entradas) * epocas); //Iteracoes do for para portas logicas
            int saida = 0;
            int var = tt.Length;
            double soma = 0.0;
                for (int i = 0; i < qtde; i++)
                {
                //Geracao do dataset para portas logicas
                bits = toBin(i % var);
                //Verificacao da saida e ajuste dos pesos
                soma = somarBits();
                saida = verify(soma);
                    if (saida != tt[i%var])
                    {
                        for (int k = 0; k < pesos.Length; k++)
                        {
                            pesos[k] = pesos[k] + (tda * (tt[i % var] - saida) * bits[k]);
                            ///Console.WriteLine("Alterado");
                        }
                    }
                }
        }
        double somar(int[] amostra) {
            double soma = 0.0;
            for (int i = 0; i < amostra.Length; i++) {
                soma += amostra[i] * pesos[i];
            }
            soma += bias * pesos[pesos.Length - 1];
            return soma;
        }
        void corrigirPesos(int[] amostra, int saidaE, int saidaC) { 
            for (int i = 0;i<amostra.Length;i++)
            {
                pesos[i] = pesos[i] + (tda * (saidaC - saidaE) * amostra[i]);
            }
            pesos[amostra.Length] = pesos[amostra.Length] + (tda * (saidaC - saidaE) * bias);
        }
        public void train(int[][] amostras, int[] saidas) {
            double soma = 0.0;
            int saida;
            for (double i = 0;i < epocas;)
            {
                for (int k = 0; k < amostras.Length; k++) {
                    soma = somar(amostras[k]);
                    saida = verify(soma);
                    if (saida != saidas[k]) {
                        corrigirPesos(amostras[k], saida, saidas[k]);
                    }
                    i += 1.0 / (double)amostras.Length;
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
