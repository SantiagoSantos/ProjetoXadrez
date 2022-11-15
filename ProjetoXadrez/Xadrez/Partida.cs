using ProjetoXadrez.TabuleiroConfig;
using ProjetoXadrez.TabuleiroConfig.Exceptions;

namespace ProjetoXadrez.Xadrez
{
    public class Partida
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool PartidaTerminada { get; private set; }
        public HashSet<Peca> Pecas { get; set; }
        public HashSet<Peca> PecasCapturadas { get; private set; }
        public bool Xeque { get; private set; }

        public Partida()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            PartidaTerminada = false;

            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();

            ColocarPecas();
            
        }

        private Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca pc = Tabuleiro.RetirarPeca(origem);
            pc.IncrementarMovimentos();
            var pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(pc, destino);

            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public HashSet<Peca> RecuperaPecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = PecasCapturadas.Where(e => e.Cor == cor).ToHashSet();

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = Pecas.Where(e => e.Cor == cor).ToHashSet();

            aux.ExceptWith(RecuperaPecasCapturadas(cor));

            return aux;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            var pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque.");
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                PartidaTerminada = true;
            }
            else
            {
                Turno++;
                ProximoJogador();
            }

            //Xeque = EstaEmXeque(Adversaria(JogadorAtual));
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarMovimentos();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(peca, origem);
        }

        private void ProximoJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não há uma peça na posição de origem escolhida.");
            }
            if (JogadorAtual != Tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua ...");
            }
            if (!Tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça selecionada.");
            }
        }

        public void ValidarPosicaDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida.");
            }
        }

        private void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private Cor Adversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            var peca = PecasEmJogo(cor).FirstOrDefault(e => e is Rei);            

            return peca;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
            {
                throw new TabuleiroException($"Não há Rei da cor {cor} no tabuleiro.");
            }

            foreach (var peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();

                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (var item in PecasEmJogo(cor))
            {
                bool[,] mat = item.MovimentosPossiveis();

                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (mat[i,j])
                        {
                            Posicao origem = item.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }                
            }
            return true;
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('C', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('C', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 1, new Rei(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('C', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('C', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 8, new Rei(Tabuleiro, Cor.Preta));

        }
    }
}
