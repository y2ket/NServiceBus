namespace NServiceBus.Core.Tests.API
{
    using NUnit.Framework;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    class CancellationToken
    {
        [Test]
        public void AllPublicAsyncMethodsAcceptACancellationToken()
        {
            var allTypes = typeof(IMessage).Assembly.GetTypes();

            var publicTypes = allTypes
                .Where(type => type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamORAssem).ToList();

            var allMethods = publicTypes
                .SelectMany(type => type.GetMethods()).ToList();

            var allPublicMethods = allMethods
                .Where(method => method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly).ToList();

            var taskReturningMethods = allPublicMethods
                .Where(method => typeof(Task).IsAssignableFrom(method.ReturnType)).ToList();

            var taskReturningMethodsMissingCancellationTokenParameter = taskReturningMethods
                .Where(method => !method.GetParameters().Any(param => param.ParameterType == typeof(CancellationToken))).ToList();

            Assert.IsEmpty(taskReturningMethodsMissingCancellationTokenParameter);
        }
    }
}
