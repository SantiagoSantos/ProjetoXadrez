using ProjetoXadrez.TabuleiroConfig;

namespace ProjetoXadrez
{
    public static class View
    {
        private static string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string linhaLetras = "  ";

        public static void GeraTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{tab.Linhas - i} ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        //Console.Write($"{tab.Peca(i, j)} ");
                        ImprimePeca(tab.Peca(i, j));
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < tab.Colunas; i++)
            {
                linhaLetras += $"{letras.Substring(i,1)} ";
            }

            Console.Write(linhaLetras.TrimEnd());
        }

        public static void ImprimePeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write($"{peca}");                
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write($"{peca}");

                Console.ForegroundColor = aux;
            }

            Console.Write(" ");
        }
    }
}
