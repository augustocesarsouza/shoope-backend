using Microsoft.Extensions.Configuration;
using Shoope.Domain.Entities;
using Shoope.Domain.InfoErrors;
using Shoope.Infra.Data.UtilityExternal.Interface;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Shoope.Infra.Data.UtilityExternal
{
    public class SendEmailBrevo : ISendEmailBrevo
    {
        private readonly ITransactionalEmailApiUti _transactionalEmailApiUti;
        private readonly IConfiguration _configuration;

        public SendEmailBrevo(ITransactionalEmailApiUti transactionalEmailApiUti, IConfiguration configuration)
        {
            _transactionalEmailApiUti = transactionalEmailApiUti;
            _configuration = configuration;
        }

        public InfoErrors SendEmail(User user, string url)
        {
            try
            {
                var keyApi = _configuration["Brevo:KeyApi"];

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "augusto";
                string SenderEmail = "augustocesarsantana90@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("Erro name ou email invalido");

                string ToName = user.Name;
                string ToEmail = user.Email;
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Clique no token disponivel: " + url;
                string Subject = "Seu token de confirmação";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }

        public InfoErrors SendCode(User user, int codeRandon)
        {
            try
            {
                var keyApi = _configuration["Brevo:KeyApi"];

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "augusto";
                string SenderEmail = "augustocesarsantana53@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("Erro name ou email invalido");

                string ToName = user.Name ?? "";
                string ToEmail = user.Email ?? "";
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Seu numero de Confirmação: " + codeRandon.ToString();
                string Subject = "SEU NUMERO ALEATORIO DE CONFIRMAÇÃO";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }
    }
}
