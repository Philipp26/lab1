using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chunkSize">Количество символов для разбиения</param>
        /// <returns>Массив строк</returns>
        public static IEnumerable<string> Split(this string s, int chunkSize)
        {
            int chunkCount = s.Length / chunkSize;
            for (int i = 0; i < chunkCount; i++)
                yield return s.Substring(i * chunkSize, chunkSize);

            if (chunkSize * chunkCount < s.Length)
                yield return s.Substring(chunkSize * chunkCount);
        }
    }

    class Program
    {
        static Dictionary<string, char> GetCryptoAlphabet()
        {
            var setSymbols = new char[] { 'A', 'B', 'C', 'D', 'F', 'G' };
            var ASCIILatinSymbolCodeInLowerCase = 97;
            var cryptMatrix = new Dictionary<string, char>();

            for (int i = 0; i < setSymbols.Length; i++)
                for (int j = 0; j < setSymbols.Length; j++)
                {
                    if (ASCIILatinSymbolCodeInLowerCase > 122)
                        break;
                    var x = setSymbols[i].ToString();
                    var y = setSymbols[j].ToString();

                    cryptMatrix.Add(x + y, (char)ASCIILatinSymbolCodeInLowerCase);
                    ASCIILatinSymbolCodeInLowerCase++;
                }

            return cryptMatrix;
        }

        static string Crypt(string initialString, Dictionary<string, char> cryptMatrix)
        {
            string cryptString = null;

            foreach (var symbol in initialString)
                cryptString += cryptMatrix.Keys
                    .Select(x => x)
                    .Where(y => cryptMatrix[y] == symbol)
                    .First();

            return cryptString;
        }

        static string Encrypt(string initialString, Dictionary<string, char> cryptMatrix)
        {
            var arrKey = initialString.Split(2);
            string cryptString = null;

            foreach (var key in arrKey)            
                cryptString += cryptMatrix.Values
                    .Select(x => x)
                    .Where(y => cryptMatrix[key] == y)
                    .First();            
               
            return cryptString;
        }

        static void Main(string[] args)
        {
            var initialString = "pandemonium";
            Console.WriteLine("Шифруемая строка: {0}", initialString);

            var cryptMatrix = GetCryptoAlphabet();
            var cryptString = Crypt(initialString, cryptMatrix);           

            Console.WriteLine("Строка в зашифрованном виде : {0}", Crypt(initialString, cryptMatrix));

            Console.WriteLine("Строка в расшифрованном виде : {0}", Encrypt(cryptString, cryptMatrix));

            Console.ReadKey();
        }
    }
}
