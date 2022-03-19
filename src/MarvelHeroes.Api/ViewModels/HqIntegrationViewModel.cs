namespace MarvelHeroes.Api.ViewModels
{
    public class HqIntegrationViewModel
    {
        //ID no BD API Marvel
        public int IdMarvel { get; set; }

        //Titulo da comic
        public string Title { get; set; }

        //Descrição da comic
        public string Description { get; set; }

        //Preço de venda da comic
        public decimal? Price { get; set; }

        //Link para imagem da comic
        public string ImageLink { get; set; }

        //Link para wiki da comic
        public string WikiLink { get; set; }
    }
}