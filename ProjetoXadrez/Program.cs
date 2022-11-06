using ProjetoXadrez;
using ProjetoXadrez.TabuleiroConfig;
using ProjetoXadrez.Xadrez;

try
{
    Partida partida = new Partida();

    View.GeraTabuleiro(partida.Tabuleiro);


    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
