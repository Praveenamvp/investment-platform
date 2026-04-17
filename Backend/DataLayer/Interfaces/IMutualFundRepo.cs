using Models.Entity;

namespace DataLayer.Interfaces
{
    public interface IMutualFundRepo
    {
        List<MutualFund> GetAllMutualFunds();
    }
}
