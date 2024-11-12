using irede.shared.Notifications;

namespace irede.core.Entities
{
    public class Categoria: Notifiable
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        
        public Categoria(string nome)
        {
            Nome = nome;
            Validate();
        }

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void SetId(int id)
        {
            Id = id;
        }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome))
                AddNotification("O nome da categoria é obrigatório nem possuir somente espaços vazios.");

            if (Nome.Length > 100)
                AddNotification("O nome da categoria não pode exceder 100 caracteres.");
        }
    }
}
