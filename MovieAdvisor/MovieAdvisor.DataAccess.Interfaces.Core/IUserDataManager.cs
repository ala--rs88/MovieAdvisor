namespace MovieAdvisor.DataAccess.Interfaces.Core
{
    using Entities.Core;

    public interface IUserDataManager
    {
        void CreateUser(UserData userData); 

        //// toto: add overload for *Draft
    }
}