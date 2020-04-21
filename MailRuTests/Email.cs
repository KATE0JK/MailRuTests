namespace MailRuTests
{
    class Email
    {
        public Email(string sender, string subject)
        {
            Sender = sender;
            Subject = subject;
        }

        public string Sender { get;}
        public string Subject { get; }
    }
}
