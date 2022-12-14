

using ProjetoXadrez.TabuleiroConfig;

namespace ProjetoXadrez.Xadrez
{
    public class Peao : Peca
    {
        private Partida _partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca peca = Tabuleiro.Peca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool EstaLivre(Posicao pos)
        {
            return Tabuleiro.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && EstaLivre(p2) && Tabuleiro.PosicaoValida(pos) && EstaLivre(pos) && QuantidadeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao pecaEsquerda = new(Posicao.Linha, Posicao.Coluna - 1);

                    if (Tabuleiro.PosicaoValida(pecaEsquerda) && ExisteInimigo(pecaEsquerda) && Tabuleiro.Peca(pecaEsquerda) == _partida.PecaVulneravelEnPassant)
                    {
                        mat[pecaEsquerda.Linha - 1, pecaEsquerda.Coluna] = true;
                    }

                    Posicao pecaDireita = new(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tabuleiro.PosicaoValida(pecaDireita) && ExisteInimigo(pecaDireita) && Tabuleiro.Peca(pecaDireita) == _partida.PecaVulneravelEnPassant)
                    {
                        mat[pecaDireita.Linha - 1, pecaDireita.Coluna] = true;
                    }
                }                
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(p2) && EstaLivre(p2) && Tabuleiro.PosicaoValida(pos) && EstaLivre(pos) && QuantidadeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao pecaEsquerda = new(Posicao.Linha, Posicao.Coluna - 1);

                    if (Tabuleiro.PosicaoValida(pecaEsquerda) && ExisteInimigo(pecaEsquerda) && Tabuleiro.Peca(pecaEsquerda) == _partida.PecaVulneravelEnPassant)
                    {
                        mat[pecaEsquerda.Linha + 1, pecaEsquerda.Coluna] = true;
                    }

                    Posicao pecaDireita = new(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tabuleiro.PosicaoValida(pecaDireita) && ExisteInimigo(pecaDireita) && Tabuleiro.Peca(pecaDireita) == _partida.PecaVulneravelEnPassant)
                    {
                        mat[pecaDireita.Linha + 1, pecaDireita.Coluna] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
