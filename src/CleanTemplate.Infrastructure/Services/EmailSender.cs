using CleanTemplate.Application.Options;
using CleanTemplate.Application.Services;
using CleanTemplate.Infrastructure.Core;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace CleanTemplate.Infrastructure.Services;

[Inject]
public class EmailSender : IEmailSender
{
	private readonly ILogger<EmailSender> _logger;
	private readonly SmtpOptions _options;

	public EmailSender(ILogger<EmailSender> logger, IOptions<SmtpOptions> optionsAccessor)
	{
		_logger = logger;
		_options = optionsAccessor.Value;
	}


	public async Task SendAsync(string to, string subject, string body)
	{
		try
		{
			var message = CreateMimeMessage(to, subject, body);

			using var client = await ConnectToSmtpClientAsync();
			await client.SendAsync(message);
			await client.DisconnectAsync(true);

			_logger.LogInformation("Email sent to recipient: {To}", to);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Email send failed");
		}
	}


	private async Task<SmtpClient> ConnectToSmtpClientAsync()
	{
		var client = new SmtpClient();
		await client.ConnectAsync(_options.ServiceHost, _options.ServicePort, _options.ServiceSSL);
		await client.AuthenticateAsync(_options.SenderEmail, _options.SenderPassword);
		return client;
	}

	private MimeMessage CreateMimeMessage(string to, string subject, string body)
	{
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
		message.To.Add(new MailboxAddress("", to));
		message.Subject = subject;

		message.Body = new TextPart(TextFormat.Html) { Text = body };
		return message;
	}
}