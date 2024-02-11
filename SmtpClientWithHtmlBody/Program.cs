// See https://aka.ms/new-console-template for more information
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Runtime;

Console.WriteLine("Hello, World!");

using var smtp = new SmtpClient();

CancellationToken ct = default;

//if (_settings.UseSSL)
//{
await smtp.ConnectAsync("smtp.m1.websupport.sk", 465, SecureSocketOptions.SslOnConnect, ct);
//}
//else if (_settings.UseStartTls)
//{
//    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
//}
await smtp.AuthenticateAsync("bendea@bendea.sk", "Qq0,(MR+`*", ct);

var mail = new MimeMessage();

mail.From.Add(MailboxAddress.Parse("bendea@bendea.sk"));

mail.Subject = "Registrácia Endokrinologická Ambulancia Bendea";

var body = new BodyBuilder();
using StreamReader sr = new("registration-template.html");
body.HtmlBody = sr.ReadToEnd();
mail.Body = body.ToMessageBody();

mail.To.Add(MailboxAddress.Parse("and.simkova@gmail.com"));

await smtp.SendAsync(mail, ct);
await smtp.DisconnectAsync(true, ct);