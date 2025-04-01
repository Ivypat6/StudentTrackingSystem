using Microsoft.ML.Data;

namespace StudentTrackingSystem.API.Prediction_Model
{
    public class StudentPerformancePrediction
    {
        [ColumnName("PredictedLabel")]
        public string PerformanceCategory;

        [ColumnName("Score")]
        public float[] Score;
    }
}
