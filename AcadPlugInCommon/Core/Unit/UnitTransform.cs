using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPlugInCommon
{
    public static class UnitTransform
    {
        /// <summary>
        /// 角度转换成弧度，60°将转换成 Math.Pi/3
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>弧度</returns>
        public static double AngleToRadian(this double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// 弧度转换成角度，Math.Pi/4 将转换成45°
        /// </summary>
        /// <param name="radian">弧度</param>
        /// <returns>角度</returns>
        public static double RadianToAngle(this double radian)
        {
            return radian * 180 / Math.PI;
        }

        /// <summary>
        /// 英寸转毫米
        /// </summary>
        /// <param name="inch">英寸</param>
        /// <returns>毫米</returns>
        public static double InchToMM(this double inch)
        {
            return inch * 25.4;
        }

        /// <summary>
        /// 毫米转英寸
        /// </summary>
        /// <param name="mm">毫米</param>
        /// <returns>英寸</returns>
        public static double MMToInch(this double mm)
        {
            return mm / 25.4;
        }

        /// <summary>
        /// 英尺转毫米
        /// </summary>
        /// <param name="feet">英尺</param>
        /// <returns>毫米</returns>
        public static double FeetToMM(this double feet)
        {
            return feet * 308.4;
        }

        /// <summary>
        /// 毫米转英尺
        /// </summary>
        /// <param name="mm">毫米</param>
        /// <returns>英尺</returns>
        public static double MMToFeet(this double mm)
        {
            return mm / 308.4;
        }

        /// <summary>
        /// 摄氏度转华氏度
        /// </summary>
        /// <param name="c">摄氏度</param>
        /// <returns>华氏度</returns>
        public static double CelsiusToFahrenheit(this double c)
        {
            return (c - 32) / 1.8;
        }

        /// <summary>
        /// 华氏度转摄氏度
        /// </summary>
        /// <param name="f">华氏度</param>
        /// <returns>摄氏度</returns>
        public static double FahrenheitToCelsius(this double f)
        {
            return 1.8 * f - 32;
        }
    }
}
