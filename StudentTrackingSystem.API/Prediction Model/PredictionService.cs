using Microsoft.ML;
using StudentTrackingSystem.Data.Models;
using StudentTrackingSystem.Data.Reositories.Interface;
using StudentTrackingSystem.Shared.Dto;

namespace StudentTrackingSystem.API.Prediction_Model
{
    public class PredictionService : IPredictionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private MLContext _mlContext;
        private ITransformer _trainedModel;
        private PredictionEngine<StudentPerformanceData,
    StudentPerformancePrediction> _predictionEngine;
        private string _modelPath;

        public PredictionService(IUnitOfWork unitOfWork,
    IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mlContext = new MLContext();
            _modelPath = Path.Combine(_env.ContentRootPath, "Models",
    "StudentPerformanceModel.zip");

            // Load model if exists 
            if (File.Exists(_modelPath))
            {
                _trainedModel = _mlContext.Model.Load(_modelPath, out _);
                _predictionEngine =
    _mlContext.Model.CreatePredictionEngine<StudentPerformanceData,
    StudentPerformancePrediction>(_trainedModel);
            }
        }

        public async Task<PredictionResultDto>
    PredictStudentPerformanceAsync(Student student, IEnumerable<Grade>
    grades, IEnumerable<Attendance> attendances,
    IEnumerable<BehaviorRecord> behaviorRecords)
        {
            // If model doesn't exist, train it first 
            if (_trainedModel == null)
            {
                await TrainModelAsync();
            }

            // Calculate features 
            var averageGrade = grades.Any() ? grades.Average(g => g.Score
    / g.MaxScore * 100) : 0;
            var totalClasses = attendances.Select(a =>
    a.Date).Distinct().Count();
            var attendedClasses = attendances.Count(a => a.IsPresent);
            var attendanceRate = totalClasses > 0 ? (float)attendedClasses
    / totalClasses * 100 : 0;
            var behaviorIncidents = behaviorRecords.Count(b =>
    b.BehaviorType == "Negative");

            // Create input data 
            var input = new StudentPerformanceData
            {
                AverageGrade = averageGrade,
                AttendanceRate = attendanceRate,
                BehaviorIncidents = behaviorIncidents
            };

            // Make prediction 
            var prediction = _predictionEngine.Predict(input);

            // Map to DTO 
            var result = new PredictionResultDto
            {
                StudentId = student.Id,
                StudentName = $"{student.FirstName} {student.LastName}",
                Prediction = prediction.PerformanceCategory,
                Probability = prediction.Score.Max(),
                RecommendedActions =
    GetRecommendedActions(prediction.PerformanceCategory)
            };

            return result;
        }

        public async Task TrainModelAsync()
        {
            // Get historical data for training 
            var trainingData = await GetTrainingData();

            // Convert to IDataView 
            var dataView =
    _mlContext.Data.LoadFromEnumerable(trainingData);

            // Data preprocessing pipeline 
            var pipeline =
    _mlContext.Transforms.Conversion.MapValueToKey("Label",
    nameof(StudentPerformanceData.PerformanceCategory))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    nameof(StudentPerformanceData.AverageGrade),
                    nameof(StudentPerformanceData.AttendanceRate),
                    nameof(StudentPerformanceData.BehaviorIncidents)))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Train the model 
            _trainedModel = pipeline.Fit(dataView);

            // Save the model 
            _mlContext.Model.Save(_trainedModel, dataView.Schema,
    _modelPath);

            // Create prediction engine 
            _predictionEngine =
    _mlContext.Model.CreatePredictionEngine<StudentPerformanceData,
    StudentPerformancePrediction>(_trainedModel);
        }

        private async Task<List<StudentPerformanceData>> GetTrainingData()
        {
            // This would ideally come from historical data 
            // For demo purposes, we'll create some synthetic data 

            return new List<StudentPerformanceData>
        {
            new StudentPerformanceData { AverageGrade = 90,AttendanceRate = 95, BehaviorIncidents = 0, PerformanceCategory ="Excelling" },
            new StudentPerformanceData { AverageGrade = 85,AttendanceRate = 90, BehaviorIncidents = 1, PerformanceCategory ="Excelling" },
            new StudentPerformanceData { AverageGrade = 75,AttendanceRate = 80, BehaviorIncidents = 2, PerformanceCategory = "On Track" },
            new StudentPerformanceData { AverageGrade = 65,AttendanceRate = 70, BehaviorIncidents = 3, PerformanceCategory = "On Track" },
            new StudentPerformanceData { AverageGrade = 55,AttendanceRate = 60, BehaviorIncidents = 5, PerformanceCategory = "At Risk" },
            new StudentPerformanceData { AverageGrade = 45,AttendanceRate = 50, BehaviorIncidents = 8, PerformanceCategory = "At Risk" }, 
            // Add more training examples 
        };
        }


        private string[] GetRecommendedActions(string performanceCategory)
        {
            return performanceCategory;
    }
    }
}