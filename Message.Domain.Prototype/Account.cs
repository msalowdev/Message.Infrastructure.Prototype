using Message.Domain.Prototype.DomainEvents;

namespace Message.Domain.Prototype
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public void CreateNewAccount()
        {
            DomainEventsHandler.Raise(new AccountCreated {Id = this.Id, Name = this.Name});
        }

        public void DeleteAccount()
        {
            DomainEventsHandler.Raise(new AccountDeleted {Id = this.Id});
        }

    }
}
