using SYS.BLL.Base;
using SYS.DAL.Base;
using SYS.DAL.ChatBot;
using SYS.Model;
using SYS.Model.SQL.ChatBot;

namespace SYS.BLL.Domain
{
    public interface IAccountRegistLogic : IDataDrivenLogic
    {
        // Logic
        IAccountRegistRepository _AccountRegistRepository { get; set; }
        
        // Function
        void CreateLineAcc(AccountRegist input);
        AccountRegist GetAccountByEmpNo(string empNo);
        AccountRegist GetAccountByLineId(string lineId);
        void UpdateAcc(AccountRegist input);
    }
    internal class AccountRegistLogic : DataDrivenLogic, IAccountRegistLogic
    {
        public IAccountRegistRepository _AccountRegistRepository { get; set; }

        public AccountRegistLogic(IBusinessLogicFactory BusinessLogicFactory, IRepositoryFactory RepositoryFactory = null) : base(BusinessLogicFactory, RepositoryFactory)
        {
            _AccountRegistRepository = CreateSqlRepository<IAccountRegistRepository>(Database.Default);
        }
        public void CreateLineAcc(AccountRegist input)
        {
            var exists = _AccountRegistRepository.GetAccountByLineId(input.Line_Id);
            if (exists != null && exists.Active)
            {
                exists.Active = false;
                _AccountRegistRepository.Update(exists);
            }
            else
            {
                _AccountRegistRepository.Create(input);
            }
        }
        public AccountRegist GetAccountByEmpNo(string empNo)
        {
            return _AccountRegistRepository.GetAccountByEmpNo(empNo);
        }

        public AccountRegist GetAccountByLineId(string lineId)
        {
            return _AccountRegistRepository.GetAccountByLineId(lineId);
        }
        
        public void UpdateAcc(AccountRegist input)
        {
            _AccountRegistRepository.Update(input);
        }
    }
}
