namespace MarvelHeroes.Api.ViewModels
{
    public class QuadrinhontegracaoViewModel
    {
        //ID no BD API Marvel
        public int IdMarvel { get; set; }

        //Titulo da comic
        public string Titulo { get; set; }

        //Descrição da comic
        public string Descricao { get; set; }

        //Preço de venda da comic
        public decimal? Preco { get; set; }

        //Link para imagem da comic
        public string LinkImagem { get; set; }

        //Link para wiki da comic
        public string LinkWiki { get; set; }
    }
}