using DIO.Bank.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIO.Bank.Classes
{
    public class Conta
    {
        private TipoConta TipoConta { get; set; }
        public readonly int NumeroConta;
        private string Titular { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private static int Id = 1;

        public Conta(TipoConta tipo, string titular, double saldo, double credito)
        {
            this.TipoConta = tipo;
            this.NumeroConta = GenerateId();
            this.Titular = titular;
            this.Saldo = saldo;
            this.Credito = credito;
        }

        public bool Sacar(double valor)
        {
            if (valor > Saldo + Credito)
            {
                Console.WriteLine("Saldo insuficiente");
                return false;
            } 
            else if (valor > Saldo)
            {
                Saldo -= valor;
                Credito -= Saldo;
                Console.WriteLine($"Saldo Atual: {Saldo} | Credito: {Credito}");
                return true;
            } 
            else
            {
                Saldo -= valor;
                Console.WriteLine($"Saldo Atual: {Saldo} | Credito: {Credito}");
                return true;
            }
        }

        public void Depositar(double valor)
        {
            // Saldo negativo e depósito menor
            if (Saldo < 0 && Saldo < valor*(-1) )
                Credito += valor;
            //Saldo negatigo e deposito maior que dívida
            else if (Saldo < 0 && Saldo > valor * (-1))
                Credito += Saldo * (-1);
            
            Saldo += valor;
            Console.WriteLine($"Saldo Atual: {Saldo} | Credito: {Credito}");
        }

        public void Transferir(double valor, Conta contaDestino)
        {
            if (Sacar(valor))
                contaDestino.Depositar(valor);
        }

        public override string ToString()
        {
            var retorno = $"Tipo da Conta:  {TipoConta}" + "\n" +
                          $"Conta:          {NumeroConta}" + "\n" +
                          $"Titular:        {Titular}" + "\n" +
                          $"Saldo:          {Saldo}" + "\n" +
                          $"Crédito:        {Credito}" + "\n";

            return retorno;
        }

        private static int GenerateId()
        {
            return Id++;
        }
    }
}
