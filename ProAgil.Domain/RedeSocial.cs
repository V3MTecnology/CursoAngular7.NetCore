namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; } //Quando faz o post se deixar set vai gravar mais não vai conseguir retornar porque entra no loop. com readOnly   "evento": null
        public int? PalestranteId { get; set; }
        public Palestrante Palestrante { get; } //Quando faz o post se deixar set vai gravar mais não vai conseguir retornar porque entra no loop. com readOnly   "evento": null
        

    }
}