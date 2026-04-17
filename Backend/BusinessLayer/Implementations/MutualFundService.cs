using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Models.Entity;

namespace BusinessLayer.Implementations
{
    public class MutualFundService : IMutualFundService
    {
        private readonly IMutualFundRepo _repo;

        public MutualFundService(IMutualFundRepo repo)
        {
            _repo = repo;
        }

        public List<MutualFund> GetAllMutualFunds()
        {
            return _repo.GetAllMutualFunds();
        }
    }
}
