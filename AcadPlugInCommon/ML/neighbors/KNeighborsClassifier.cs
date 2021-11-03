using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPlugInCommon.MVVM;
using NumSharp;

namespace AcadPlugInCommon.ML.neighbors
{
    public class KNeighborsClassifier : ISupervised
    {
        private NDArray _trainX;
        private NDArray _trainY;
        private readonly int _neighbors;
        private readonly int _p;

        public NDArray TrainX { get => _trainX; set => _trainX = value; }
        public NDArray TrainY { get => _trainY; set => _trainY = value; }

        public KNeighborsClassifier(int n_neighbors,int p)
        {
            this._neighbors = n_neighbors;
            this._p = p;
        }

        public void Fit(NDArray trainX, NDArray trainY)
        {
            this._trainX = trainX;
            this._trainY = trainY;
        }

        public NDArray Predict(NDArray predictX)
        {
            NDArray predict = new NDArray(typeof(double));
            foreach (var row in predictX)
            {

            }
            return predict;
        }

        public NDArray Score(NDArray predictX, NDArray predictY)
        {
            throw new NotImplementedException();
        }

        double _getnear(NDArray array)
        {
            var dists = np.power(np.sum(_trainX - array), _p);
            double[] res = new double[_neighbors];
            double temp = double.MaxValue;
            for (int i = 0; i < _neighbors; i++)
            {
                for (int j = 0; j < dists.shape[0]; j++)
                {

                }
            }
            return 0.0;
        }
    }
}
