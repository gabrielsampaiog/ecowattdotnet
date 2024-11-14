using EcoWatt.Model;
using Microsoft.ML;


namespace EcoWatt.Service.Recommendation
{
    public class RecommendationEngine
    {
        private readonly MLContext _mlContext = new MLContext();
        private ITransformer _model;


        public void PrepareTrainModel(IEnumerable<UsuarioUse> allusuarioUses)
        {
            var batteryUsing = new List<UsedBattery>();
            foreach (var usuarioUse in allusuarioUses)
            {
                batteryUsing.Add(new UsedBattery
                {
                    IdUsuario = usuarioUse.IdUsuario,
                    IdBateria = usuarioUse.IdBateria,
                    UseRelevance = 1
                });
            }

            Train(batteryUsing);
        }

        private void Train(List<UsedBattery> batteryUsing)
        {
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable(batteryUsing);

            var pipeline = _mlContext
                            .Transforms
                            .Conversion
                            .MapValueToKey(outputColumnName: "usuarioIdEncoded", inputColumnName: nameof(UsedBattery.IdUsuario))
                            .Append(
                                _mlContext
                                .Transforms
                                .Conversion
                                .MapValueToKey(outputColumnName: "bateriaIdEncoded", inputColumnName: nameof(UsedBattery.IdBateria)))
                            .Append(_mlContext
                                    .Recommendation()
                                    .Trainers
                                    .MatrixFactorization(
                                        labelColumnName: nameof(UsedBattery.UseRelevance),
                                        matrixColumnIndexColumnName: "usuarioIdEncoded",
                                        matrixRowIndexColumnName: "bateriaIdEncoded"));
            _model = pipeline.Fit(trainingData);
        }

        public float Predict(int clienteId, int batteryId)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UsedBattery, BatteryPrediction>(_model);

            var prediction = predictionEngine.Predict(new UsedBattery
            {
                IdUsuario = clienteId,
                IdBateria = batteryId
            });

            return prediction.Score;
        }

        class BatteryPrediction
        {
            public float Score { get; set; }
        }
    }
}
