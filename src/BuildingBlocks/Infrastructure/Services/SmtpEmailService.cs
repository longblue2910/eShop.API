using Contracts.Services;
using Infrastructure.Configurations;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using Shared.Services.Email;

namespace Infrastructure.Services;

public class SmtpEmailService : ISmtpEmailService
{
    private readonly ILogger _logger;
    private readonly SMTPEmailSetting _setting;
    private readonly SmtpClient _smtpClient;

    public SmtpEmailService(ILogger logger, SMTPEmailSetting setting)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _setting = setting ?? throw new ArgumentNullException(nameof(setting));
        _smtpClient = new SmtpClient();
    }

    public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
    {
        var emailMessage = GetMimeMessage(request);

        try
        {
            await _smtpClient.ConnectAsync(_setting.SMTPServer, _setting.Port, _setting.UseSsl, cancellationToken);
            await _smtpClient.AuthenticateAsync(_setting.UserName, _setting.Password, cancellationToken);
            await _smtpClient.SendAsync(emailMessage, cancellationToken);
            await _smtpClient.DisconnectAsync(true, cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }
        finally
        {
            await _smtpClient.DisconnectAsync(true, cancellationToken);
            _smtpClient.Dispose();
        }
    }

    public void SendMail(MailRequest request)
    {
        var emailMessage = GetMimeMessage(request);

        try
        {
            _smtpClient.Connect(_setting.SMTPServer, _setting.Port, _setting.UseSsl);
            _smtpClient.Authenticate(_setting.UserName, _setting.Password);
            _smtpClient.Send(emailMessage);
            _smtpClient.Disconnect(true);

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }
        finally
        {
            _smtpClient.Disconnect(true);
            _smtpClient.Dispose();
        }
    }

    private MimeMessage GetMimeMessage(MailRequest request)
    {
        var emailMessage = new MimeMessage
        {
            Sender = new MailboxAddress(_setting.DisplayName, request.From ?? _setting.From),
            Subject = request.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = request.Body
            }.ToMessageBody()
        };

        if (request.ToAddresses.Any())
        {
            foreach (var toAddress in request.ToAddresses)
            {
                emailMessage.To.Add(MailboxAddress.Parse(toAddress));
            }
        }
        else
        {
            var toAddress = request.ToAddress;
            emailMessage.To.Add(MailboxAddress.Parse(toAddress));
        }
        return emailMessage;
        
    }
}
