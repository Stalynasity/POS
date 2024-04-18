namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        //declaracion o matricula de nuestra interfaces a nivel de repositoy

        ICategoryRepositoy Category {  get; }
        void SaveChange();
        Task SaveChangesAsync();
    }
}
