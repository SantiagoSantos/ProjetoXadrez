using ProjetoXadrez;
using ProjetoXadrez.TabuleiroConfig;
using ProjetoXadrez.Xadrez;

try
{
    Partida partida = new();

    while (!partida.PartidaTerminada)
    {
        try
        {
            Console.Clear();

            View.ImprimePartida(partida);

            Console.Write("Origem: ");
            Posicao origem = View.LerPosicaoXadrez().ToPosicao();

            partida.ValidarPosicaoOrigem(origem);

            bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();

            Console.Clear();

            View.GeraTabuleiro(partida.Tabuleiro, posicoesPossiveis);

            Console.Write("Destino: ");
            Posicao destino = View.LerPosicaoXadrez().ToPosicao();

            partida.ValidarPosicaDestino(origem, destino);

            partida.RealizaJogada(origem, destino);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Pressione 'Enter'");
            Console.ReadKey();
        }
        
    }

    
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
