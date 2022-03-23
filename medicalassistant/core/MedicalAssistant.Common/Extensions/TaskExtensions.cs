using System.Threading.Tasks;

namespace MedicalAssistant.Common.Extensions
{
    public static class TaskExtensions
    {
        public static TResultType WaitAndUnwrapException<TResultType>(this Task<TResultType> task)
        {
            return task.GetAwaiter().GetResult();
        }

        public static void WaitAndUnwrapException(this Task task)
        {
            task.GetAwaiter().GetResult();
        }
    }
}
