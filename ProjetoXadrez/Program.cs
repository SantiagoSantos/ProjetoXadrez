using ProjetoXadrez;
using ProjetoXadrez.TabuleiroConfig;
using ProjetoXadrez.Xadrez;

try
{
    Partida partida = new Partida();

    while (!partida.PartidaTerminada)
    {
        Console.Clear();

        View.GeraTabuleiro(partida.Tabuleiro);

        Console.Write("Origem: ");
        Posicao origem = View.LerPosicaoXadrez().ToPosicao();
                
        bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();

        Console.Clear();

        View.GeraTabuleiro(partida.Tabuleiro, posicoesPossiveis);

        Console.Write("Destino: ");
        Posicao destino = View.LerPosicaoXadrez().ToPosicao();

        partida.ExecutaMovimento(origem, destino);
    }

    


    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
