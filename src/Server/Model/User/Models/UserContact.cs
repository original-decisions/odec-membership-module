using odec.Server.Model.User.Generic.UnifiedKey;
using Cont =odec.Server.Model.Contact.Contact;
namespace odec.Server.Model.User
{
    /// <summary>
    /// Серверный объект - связь пользователя и контакта
    /// </summary>
    public class UserContact :UserContactGeneric<int, Cont>
    {
    }
}
