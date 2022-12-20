using System.Diagnostics;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Domain.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        private Guid _firstSimpleEntityId;
        private Guid _firstComplexEntityId;
        private int _numberOfEntities;

        private const string _databaseType = "EFCore";

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public string RunTests()
        {
            List<string> formattedResults = new();

            _numberOfEntities = _testRepository.GetEntitiesCount();
            _firstSimpleEntityId = _testRepository.GetFirstSimpleEntityId();
            _firstComplexEntityId = _testRepository.GetFirstComplexEntityId();

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetSimpleEntity(_firstSimpleEntityId), "GetSimpleEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetComplexEntity(_firstComplexEntityId), "GetComplexEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetSimpleEntityWithIndex(_firstSimpleEntityId), "GetSimpleEntityWithIndex")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetComplexEntityWithIndex(_firstComplexEntityId), "GetComplexEntityWithIndex")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetAllSimpleEntities(), "GetAllSimpleEntities")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.GetAllComplexEntities(), "GetAllComplexEntities")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.AddSimpleEntity(), "AddSimpleEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.AddComplexEntity(), "AddComplexEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.UpdateSimpleEntity(_firstSimpleEntityId), "UpdateSimpleEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.UpdateComplexEntity(_firstComplexEntityId), "UpdateComplexEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.DeleteSimpleEntity(_firstSimpleEntityId), "DeleteSimpleEntity")
            );

            formattedResults.Add(
                RunWithTimeMeasurement(() => _testRepository.DeleteComplexEntity(_firstComplexEntityId), "DeleteComplexEntity")
            );

            return string.Join('\n', formattedResults);
        }

        private string RunWithTimeMeasurement(Action func, string actionName)
        {
            var stopwatch = Stopwatch.StartNew();
            func.Invoke();
            stopwatch.Stop();
            return FormatTheStopwatchResult(stopwatch, actionName);
        }

        private string FormatTheStopwatchResult(Stopwatch stopwatch, string actionName)
            => $"{_databaseType}, {_numberOfEntities}, {actionName}, {stopwatch.Elapsed.ToString("g")}";
    }
}
