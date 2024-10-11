namespace Infrastructure.DatabaseSeed;
public interface ISeeder
{
    Task Initialize();
    Task SeedPrimeUser();

}
