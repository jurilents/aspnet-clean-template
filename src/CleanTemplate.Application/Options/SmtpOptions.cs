namespace CleanTemplate.Application.Options;

public record SmtpOptions
{
	public string ServiceHost { get; set; } = default!;
	public int ServicePort { get; set; }
	public bool ServiceSSL { get; set; }
	public string SenderEmail { get; set; } = default!;
	public string SenderPassword { get; set; } = default!;
	public string SenderName { get; set; } = default!;
}