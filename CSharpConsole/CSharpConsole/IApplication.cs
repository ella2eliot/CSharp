using HealthCareConsole;

namespace CSharpConsole
{
    public interface IApplication
    {
        IBusinessLogic BusinessLogic { get; set; }

        void Run();
    }
}