using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public static class WhatsAppService
{

    static WhatsAppService()
    {
    }

    public static void SendSecretSantaMessages(List<SecretSantaPair> pairs)
    {
        foreach (var pair in pairs)
        {
            var teste = CreateWhatsAppLink(pair.Telefone, pair.Message);
        }
    }

    public static string CreateWhatsAppLink(string phoneNumber, string message)
    {
        // Remove any non-digit characters from the phone number
        var cleanedPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

        // Encode the message to be URL-safe
        var encodedMessage = Uri.EscapeDataString(message);

        // Create the WhatsApp link
        var whatsappLink = $"https://wa.me/{cleanedPhoneNumber}?text={encodedMessage}";

        return whatsappLink;
    }

}

