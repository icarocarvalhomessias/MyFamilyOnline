using System.Text.Json.Serialization;
public class SecretSantaPair
{
    [JsonIgnore]
    public Parente Parente { get; set; }
    [JsonIgnore]
    public Parente HiddenFriend { get; set; }
    public string MeuNome => Parente?.Nome;
    public string MeuAmigoOculto => HiddenFriend?.Nome;

    public string Telefone => Parente?.Telefone;
    public string LinkNome => HiddenFriend?.LinkNome;

    public string Message => $@"
    Olá, {Parente?.Nome}! {(Parente?.Genero == "Masculino" ? "Seja bem-vindo" : "Seja bem-vinda")} ao Amigo Oculto da Família Carvalho 2024! 🎁  

    📌 **Informações importantes:**  
    - **Valor mínimo do presente:** R$80,00  
    - **Data da revelação:** 25/12/2024, após o almoço de Natal.  
    - **Confira aqui com quem você saiu:** [Clique neste link]({LinkNome}).  

    Em breve, vou compartilhar a lista de desejos de cada participante para facilitar a escolha do presente. Fique de olho! 😉🎄✨
    ";

}
