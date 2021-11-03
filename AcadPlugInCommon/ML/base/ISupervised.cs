using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;

namespace AcadPlugInCommon.ML
{
    interface ISupervised
    {
        NDArray TrainX { get; set; }
        NDArray TrainY { get; set; }
        void Fit(NDArray trainX,NDArray trainY);
        NDArray Predict(NDArray predictX);
        NDArray Score(NDArray predictX,NDArray predictY);
    }
}
