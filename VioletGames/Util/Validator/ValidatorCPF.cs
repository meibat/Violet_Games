using System;

namespace VioletGames.Util.Validator
{
    public class ValidatorCPF
    {
        public static bool IsCPF(String cpf)
        {
            string cpfValue = cpf.Replace("-", "");
            cpfValue = cpfValue.Replace(".", "");
            String cpfTemp = cpfValue.Substring(0, 9);
            String digito;

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            int resto;

            if (cpfValue.Length != 11) return false;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpfTemp[i].ToString()) * mult1[i];
            }
            resto = soma % 11;

            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();
            cpfTemp += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpfTemp[i].ToString()) * mult2[i];
            }
            resto = soma % 11;

            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();
            cpfTemp += digito;

            return cpf.EndsWith(digito);
        }
    }
}
