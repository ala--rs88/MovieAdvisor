namespace MovieAdvisor.Core.DataAccess.Interfaces
{
    using Entities;

    public interface IUserDataManager
    {
        void CreateUser(UserData userData); 

        //// toto: add overload for *Draft
    }
}