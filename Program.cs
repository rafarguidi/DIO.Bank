using DIO.Bank.Classes;
using DIO.Bank.Enum;
using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        public static List<Conta> contas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcao;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Opções: ");
                Console.WriteLine("1 - Listar Contas");
                Console.WriteLine("2 - Adicionar Conta");
                Console.WriteLine("3 - Transferir");
                Console.WriteLine("4 - Sacar");
                Console.WriteLine("5 - Depositar");
                Console.WriteLine("6 - Limpar Tela");
                Console.WriteLine("x - Sair");
                Console.WriteLine();
                
                opcao = Console.ReadLine();
                
                switch (opcao.ToUpper())
                {
                    case "1": // Listar Contas
                        Console.Clear();
                        if (contas.Count == 0)
                            Console.WriteLine("Não há contas cadastradas!");

                        foreach (var c in contas)
                        {
                            Console.WriteLine(c.ToString());
                        }
                        Console.WriteLine();
                        break;

                    case "2": // Adicionar Conta
                        Console.WriteLine("Informe o Tipo da Conta (1 - Pessoa Física ou 2 - Pessoa Jurídica): ");
                        if (!int.TryParse(Console.ReadLine(), out int tipoConta))
                        {
                            Console.WriteLine("O Tipo informado deve ser um número!");
                            break;
                        }
                        if (tipoConta != 1 && tipoConta != 2)
                        {
                            Console.WriteLine("Tipo de Conta informado inválido!");
                            break;
                        }
                        Console.WriteLine("Informe o nome do Titular: ");
                        var nome = Console.ReadLine();
                        Console.WriteLine("Informe o Saldo: ");
                        if (!double.TryParse(Console.ReadLine(), out double saldo))
                            Console.WriteLine("Não foi possível atribuir o valor informado para o Saldo!");
                        Console.WriteLine("Informe o Crédito: ");
                        if (!double.TryParse(Console.ReadLine(), out double credito))
                            Console.WriteLine("Não foi possível atribuir o valor informado para o Crédito!");

                        Conta conta = new Conta((TipoConta)tipoConta, nome, saldo, credito);
                        contas.Add(conta);
                        Console.WriteLine();
                        Console.WriteLine("Conta adicionada!");
                        Console.WriteLine();
                        break;

                    case "3": // Transferir
                        Console.WriteLine("Informe o ID da conta de origem:");
                        var contaOrigem = BuscarConta();
                        if (contaOrigem == null)
                            break;

                        Console.WriteLine("Informe o ID da conta de destino:");
                        var contaDestino = BuscarConta();
                        if (contaDestino == null)
                            break;

                        double? valor = RecebeValor();
                        if (valor == null)
                            break;

                        contaOrigem.Transferir((double) valor, contaDestino);
                        Console.WriteLine("Fim");

                        break;

                    case "4": // Sacar
                        Console.WriteLine("Informe o ID da conta:");
                        var contaSaque = BuscarConta();
                        if (contaSaque == null)
                            break;

                        double? valorSaque = RecebeValor();
                        if (valorSaque == null)
                            break;

                        contaSaque.Sacar((double) valorSaque);
                        break;

                    case "5": // Depositar
                        Console.WriteLine("Informe o ID da conta:");
                        var contaDeposito = BuscarConta();
                        if (contaDeposito == null)
                            break;

                        double? valorDeposito = RecebeValor();
                        if (valorDeposito == null)
                            break;

                        contaDeposito.Depositar((double)valorDeposito);

                        break;

                    case "6": // Limpar Tela
                        Console.Clear();
                        break;

                    case "X":
                        Console.WriteLine("Até logo!");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();

                }
            } while (opcao.ToUpper() != "X");
        }

        static Conta BuscarConta()
        {
            if (!int.TryParse(Console.ReadLine(), out int idConta))
            {
                Console.WriteLine("Número incorreto! Não foi possível completar a operação.");
                Console.WriteLine();
            }
            var conta = contas.Find(c => c.NumeroConta == idConta);
            if (conta == null)
            {
                Console.WriteLine("Não foi possível encontrar a conta informada!");
                Console.WriteLine();
            }
            return conta;
        }

        static double RecebeValor()
        {
            Console.WriteLine("Informe o valor:");
            if (!double.TryParse(Console.ReadLine(), out double valor))
            {
                Console.WriteLine("Valor incorreto! Não foi possível completar a operação.");
                Console.WriteLine();
            }
            return valor;
        }
    }
}
