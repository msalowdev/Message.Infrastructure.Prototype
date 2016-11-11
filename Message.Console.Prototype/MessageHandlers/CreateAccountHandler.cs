using Message.Domain.Prototype;
using Message.Events.Prototype;

namespace Message.Console.Prototype.MessageHandlers
{
    public class CreateAccountHandler : DomainMessageHandlerBase<CreateAccount>
    {
        protected override void HandleMessage(CreateAccount message)
        {
            var account = new Account
            {
                Id = 1,
                Name = message.AccountName,
                Balance = message.Amount
            };

            account.DeleteAccount();
        }
    }
}
