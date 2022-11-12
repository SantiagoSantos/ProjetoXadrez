using ProjetoXadrez.TabuleiroConfig;

namespace ProjetoXadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaTerminada { get; }

        public Partida()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
            PartidaTerminada = false;
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca pc = Tabuleiro.RetirarPeca(origem);
            pc.IncrementarMovimentos();
            var pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(pc, destino);
        }

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('C', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('C', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('D', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('E', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('E', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez('D', 1).ToPosicao());

            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('C', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('C', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('D', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('E', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('E', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez('D', 8).ToPosicao());
        }
    }
}
