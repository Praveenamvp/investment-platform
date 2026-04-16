namespace DataLayer.Interfaces
{
    public interface ITokenGenerate
    {
        public Task<string> GenerateToken(string email);

    }
}
