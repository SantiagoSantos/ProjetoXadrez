using ProjetoXadrez.TabuleiroConfig.Exceptions;

namespace ProjetoXadrez.TabuleiroConfig
{
    public class Tabuleiro
    {
        public int Linhas { get; private set; }
        public int Colunas { get; private set; }
        private readonly Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca(Peca pc, Posicao pos)
        {
            ValidarPosicao(pos);

            _pecas[pos.Linha, pos.Coluna] = pc;
            pc.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
            {
                return null;
            }

            Peca pecaAux = Peca(pos);
            pecaAux.Posicao = null;

            _pecas[pos.Linha, pos.Coluna] = null;

            return pecaAux;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }

            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }

            if (PosicaoOcupada(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição...");
            }
        }

        private bool PosicaoOcupada(Posicao pos)
        {
            //ValidarPosicao(pos);

            return Peca(pos) != null;
        }
    }
}
