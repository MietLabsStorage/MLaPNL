using System.Text.RegularExpressions;

namespace Regexes
{
    internal class Program
    {
        public static string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static int AlphabetIdx = 0;
        public static string NextLiteral => Alphabet[AlphabetIdx++].ToString();

        static void Main()
        {
            // Заменить в арифметическом выражении все числа на буквы a,b,c и т. д.
            AlphabetIdx = 0;
            var input = "(22+3)*4";
            var pattern = @"\d+";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => NextLiteral));

            // в числах может быть десятичная точка 2.5+4 → a+b
            AlphabetIdx = 0;
            input = "(22.55-3.1)/4";
            pattern = @"\d+(.\d+)?";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => NextLiteral));

            // одинаковые числа заменяются на одинаковые буквы
            AlphabetIdx = 0;
            input = "2+(2+3)";
            pattern = @"\d";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => ((char)((int)'a' + int.Parse(x.Value))).ToString()));

            // множители перед и после скобок не заменяются
            AlphabetIdx = 0;
            input = "2(3+4)5+11(22)+(33)44";
            //pattern = @"(\d+(.\d+)?){1}(?!q)";
            //pattern = @"((?<!\))(\d|(?(?<=\d)\d|\D)){1}(?!\())";
            //pattern = @"((?(?<=\d)\d(?!\()|(?(?=\d)\d|~)))+";
            pattern = @"((?<!\))(\d+){1}(?!\())";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => NextLiteral));

            // двузначные и более числа заменяются, а однозначные — нет
            AlphabetIdx = 0;
            input = "2*13+7*56";
            pattern = @"\d{2,}";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => NextLiteral));

            // первый множитель в каждой паре не заменяется, удаляется лишний знак умножения
            AlphabetIdx = 0;
            input = "2*13+7*56";
            pattern = @"\*\d+";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x => NextLiteral));

            // арифметическое выражение переводится из инфиксной в постфиксую запись
            AlphabetIdx = 0;
            input = "22*3+31";
            pattern = @"[\+\*]?\d+";
            Console.Write(input + " -> ");
            Console.WriteLine(new Regex(pattern).Replace(input, x =>
            {
                var s = x.Value.First();
                if (s == '*' || s == '+') { return NextLiteral + s; }
                return NextLiteral;
            }));
        }
    }
}