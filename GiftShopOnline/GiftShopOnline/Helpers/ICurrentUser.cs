namespace GiftShopOnline.Helpers;

public interface ICurrentUser
{
    Guid Id { get;}
    string Name { get; }
    string Email { get; }  
    string Address { get; }
}

