using System.Threading.Tasks;
using System.Reflection;

namespace SyntaxParser.Tests.Unit.Extensions
{
    public static class ExtensionMethods
    {
        public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }

}


