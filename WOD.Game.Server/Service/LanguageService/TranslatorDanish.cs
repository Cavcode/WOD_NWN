using System.Text;

namespace WOD.Game.Server.Service.LanguageService
{
    public class TranslatorDanish : ITranslator
    {
        public string Translate(string message)
        {
            var sb = new StringBuilder();

            foreach (var ch in message)
            {
                switch (ch)
                {
                    case 'a': sb.Append("l"); break;
                    case 'A': sb.Append("L"); break;

                    case 'b': sb.Append("f"); break;
                    case 'B': sb.Append("F"); break;

                    case 'c': sb.Append("l"); break;
                    case 'C': sb.Append("L"); break;

                    case 'd': sb.Append("wy"); break;
                    case 'D': sb.Append("W"); break;

                    case 'e': sb.Append("e"); break;
                    case 'E': sb.Append("E"); break;

                    case 'f': sb.Append("j"); break;
                    case 'F': sb.Append("J"); break;

                    case 'g': sb.Append("p"); break;
                    case 'G': sb.Append("P"); break;

                    case 'h': sb.Append("d"); break;
                    case 'H': sb.Append("D"); break;

                    case 'i': sb.Append("x"); break;
                    case 'I': sb.Append("X"); break;

                    case 'j': sb.Append("e"); break;
                    case 'J': sb.Append("E"); break;

                    case 'k': sb.Append("d"); break;
                    case 'K': sb.Append("D"); break;

                    case 'l': sb.Append("a"); break;
                    case 'L': sb.Append("A"); break;

                    case 'm': sb.Append("z"); break;
                    case 'M': sb.Append("Z"); break;

                    case 'n': sb.Append("r"); break;
                    case 'N': sb.Append("R"); break;

                    case 'o': sb.Append("i"); break;
                    case 'O': sb.Append("I"); break;

                    case 'p': sb.Append("l"); break;
                    case 'P': sb.Append("L"); break;

                    case 'q': sb.Append("'v"); break;
                    case 'Q': sb.Append("'"); break;

                    case 'r': sb.Append("t"); break;
                    case 'R': sb.Append("T"); break;

                    case 's': sb.Append("R"); break;
                    case 'S': sb.Append("r"); break;

                    case 't': sb.Append("t"); break;
                    case 'T': sb.Append("T"); break;

                    case 'u': sb.Append("z"); break;
                    case 'U': sb.Append("Z"); break;

                    case 'v': sb.Append("q"); break;
                    case 'V': sb.Append("Q"); break;

                    case 'w': sb.Append("i"); break;
                    case 'W': sb.Append("I"); break;

                    case 'x': sb.Append("k"); break;
                    case 'X': sb.Append("K"); break;

                    case 'y': sb.Append("b"); break;
                    case 'Y': sb.Append("B"); break;

                    case 'z': sb.Append("s"); break;
                    case 'Z': sb.Append("S"); break;

                    default: sb.Append(ch); break;
                }
            }

            return sb.ToString();
        }
    }
}
