namespace irede.core.Dtos.Core
{
    public class ProdutFiltroDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
    }
}
