using ProjetoXadrez;
using ProjetoXadrez.TabuleiroConfig;
using ProjetoXadrez.Xadrez;

try
{
    Tabuleiro tab = new(8, 8);

    tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
    tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
    tab.ColocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

    tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(6, 6));
    tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(2, 7));
    tab.ColocarPeca(new Rei(Cor.Branca, tab), new Posicao(3, 5));

    View.GeraTabuleiro(tab);

    //PosicaoXadrez pos = new PosicaoXadrez('C', 7);
    //Console.WriteLine(pos.ToPosicao());
    //Console.WriteLine(pos);

    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
