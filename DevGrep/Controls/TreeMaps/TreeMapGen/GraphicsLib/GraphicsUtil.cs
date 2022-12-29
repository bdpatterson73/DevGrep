using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib
{
    internal class GraphicsUtil
    {
        private GraphicsUtil()
        {
        }

        public static void PixelsToPoints(Graphics oGraphics, float fWidthPx, float fHeightPx, out float fWidthPt,
                                          out float fHeightPt)
        {
            ConvertPixelsAndPoints(true, oGraphics, fWidthPx, fHeightPx, out fWidthPt, out fHeightPt);
        }

        public static void PointsToPixels(Graphics oGraphics, float fWidthPt, float fHeightPt, out float fWidthPx,
                                          out float fHeightPx)
        {
            ConvertPixelsAndPoints(false, oGraphics, fWidthPt, fHeightPt, out fWidthPx, out fHeightPx);
        }

        public static void PointsToPixels(Graphics oGraphics, float fWidthPt, float fHeightPt, out int iWidthPx,
                                          out int iHeightPx)
        {
            float num;
            float num2;
            PointsToPixels(oGraphics, fWidthPt, fHeightPt, out num, out num2);
            iWidthPx = (int) (num + 0.5);
            iHeightPx = (int) (num2 + 0.5);
        }

        public static void DrawCircle(Graphics oGraphics, Pen oPen, float fXCenter, float fYCenter, float fRadius)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oPen != null);
            Debug.Assert(fRadius >= 0f);
            float num = 2f*fRadius;
            oGraphics.DrawEllipse(oPen, fXCenter - fRadius, fYCenter - fRadius, num, num);
        }

        public static void FillCircle(Graphics oGraphics, Brush oBrush, float fXCenter, float fYCenter, float fRadius)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(fRadius >= 0f);
            float num = 2f*fRadius;
            oGraphics.FillEllipse(oBrush, fXCenter - fRadius, fYCenter - fRadius, num, num);
        }

        public static void FillCircle3D(Graphics oGraphics, Color oColor, float fXCenter, float fYCenter, float fRadius)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(fRadius >= 0f);
            var graphicsPath = new GraphicsPath();
            RectangleF rectangleF = SquareFromCenterAndHalfWidth(fXCenter, fYCenter, fRadius);
            graphicsPath.AddEllipse(rectangleF);
            var pathGradientBrush = new PathGradientBrush(graphicsPath);
            pathGradientBrush.CenterPoint =
                (new PointF(rectangleF.Left + rectangleF.Width/3f, rectangleF.Top + rectangleF.Height/3f));
            pathGradientBrush.CenterColor = (Color.White);
            pathGradientBrush.SurroundColors = (new[]
                {
                    oColor
                });
            oGraphics.FillRectangle(pathGradientBrush, rectangleF);
            pathGradientBrush.Dispose();
            graphicsPath.Dispose();
        }

        public static void FillSquare(Graphics oGraphics, Brush oBrush, float fXCenter, float fYCenter, float fHalfWidth)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(fHalfWidth >= 0f);
            RectangleF oRectangle = SquareFromCenterAndHalfWidth(fXCenter, fYCenter, fHalfWidth);
            Rectangle rectangle = RectangleFToRectangle(oRectangle, 1);
            oGraphics.FillRectangle(oBrush, rectangle);
        }

        public static void FillSquare3D(Graphics oGraphics, Color oColor, float fXCenter, float fYCenter,
                                        float fHalfWidth)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(fHalfWidth >= 0f);
            var graphicsPath = new GraphicsPath();
            RectangleF rectangleF = SquareFromCenterAndHalfWidth(fXCenter, fYCenter, fHalfWidth);
            graphicsPath.AddRectangle(rectangleF);
            var pathGradientBrush = new PathGradientBrush(graphicsPath);
            pathGradientBrush.CenterPoint = (new PointF(fXCenter, fYCenter));
            pathGradientBrush.CenterColor = (Color.White);
            pathGradientBrush.SurroundColors = (new[]
                {
                    oColor
                });
            oGraphics.FillRectangle(pathGradientBrush, rectangleF);
            pathGradientBrush.Dispose();
            graphicsPath.Dispose();
        }

        public static GraphicsPath CreateRoundedRectangleGraphicsPath(Rectangle oRectangle, int iCornerRadius)
        {
            Debug.Assert(iCornerRadius > 0);
            var graphicsPath = new GraphicsPath();
            int num = 2*iCornerRadius;
            var rectangle = new Rectangle(oRectangle.Location, new Size(num, num));
            graphicsPath.AddArc(rectangle, 180f, 90f);
            rectangle.X = (oRectangle.Right - num);
            graphicsPath.AddArc(rectangle, 270f, 90f);
            rectangle.Y = (oRectangle.Bottom - num);
            graphicsPath.AddArc(rectangle, 0f, 90f);
            rectangle.X = (oRectangle.Left);
            graphicsPath.AddArc(rectangle, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public static void FillTextRectangle(Graphics oGraphics, Rectangle oRectangle, bool bTextIsSelected)
        {
            Debug.Assert(oGraphics != null);
            if (oRectangle.Width > 0 && oRectangle.Height > 0)
            {
                oGraphics.FillRectangle(bTextIsSelected ? SystemBrushes.Highlight : SystemBrushes.Window, oRectangle);
            }
        }

        public static double RadiusToArea(double dRadius)
        {
            Debug.Assert(dRadius >= 0.0);
            return 3.1415926535897931*dRadius*dRadius;
        }

        public static double AreaToRadius(double dArea)
        {
            Debug.Assert(dArea >= 0.0);
            return Math.Sqrt(dArea/3.1415926535897931);
        }

        public static RectangleF SquareFromCenterAndHalfWidth(float fXCenter, float fYCenter, float fHalfWidth)
        {
            Debug.Assert(fHalfWidth >= 0f);
            return RectangleF.FromLTRB(fXCenter - fHalfWidth, fYCenter - fHalfWidth, fXCenter + fHalfWidth,
                                       fYCenter + fHalfWidth);
        }

        public static Rectangle RectangleFToRectangle(RectangleF oRectangle, int iPenWidthPx)
        {
            var num = (int) (oRectangle.Left + 0.5f);
            var num2 = (int) (oRectangle.Right + 0.5f);
            var num3 = (int) (oRectangle.Top + 0.5f);
            var num4 = (int) (oRectangle.Bottom + 0.5f);
            if (iPenWidthPx > 1)
            {
                num2++;
                num4++;
            }
            return Rectangle.FromLTRB(num, num3, num2, num4);
        }

        public static void SaveHighQualityImage(Image oImage, string sFileName, ImageFormat eImageFormat)
        {
            Debug.Assert(oImage != null);
            Debug.Assert(sFileName != null);
            Debug.Assert(sFileName != "");
            if (eImageFormat == ImageFormat.Jpeg)
            {
                SaveJpegImage(oImage, sFileName, 100);
            }
            else
            {
                oImage.Save(sFileName, eImageFormat);
            }
        }

        public static void SaveJpegImage(Image oImage, string sFileName, int iQuality)
        {
            Debug.Assert(oImage != null);
            Debug.Assert(sFileName != null);
            Debug.Assert(sFileName != "");
            Debug.Assert(iQuality >= 1);
            Debug.Assert(iQuality <= 100);
            ImageCodecInfo imageCodecInfoForMimeType = GetImageCodecInfoForMimeType("image/jpeg");
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, iQuality);
            oImage.Save(sFileName, imageCodecInfoForMimeType, encoderParameters);
        }

        public static void DrawErrorStringOnGraphics(Graphics oGraphics, Rectangle oRectangle, string sString)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(sString != null);
            oGraphics.DrawString(sString, new Font("Arial", 11f), Brushes.Black, oRectangle);
        }

        public static ImageCodecInfo GetImageCodecInfoForMimeType(string sMimeType)
        {
            Debug.Assert(sMimeType != null);
            Debug.Assert(sMimeType != "");
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                ImageCodecInfo imageCodecInfo = imageEncoders[i];
                if (imageCodecInfo.MimeType == sMimeType)
                {
                    return imageCodecInfo;
                }
            }
            throw new Exception("GraphicsUtil.GetImageCodecInfoForMimeType: Can't find " + sMimeType + ".");
        }

        protected static void ConvertPixelsAndPoints(bool bPixelsToPoints, Graphics oGraphics, float fWidthIn,
                                                     float fHeightIn, out float fWidthOut, out float fHeightOut)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(fWidthIn >= 0f);
            Debug.Assert(fHeightIn >= 0f);
            GraphicsUnit pageUnit = oGraphics.PageUnit;
            oGraphics.PageUnit = GraphicsUnit.Point;
            var array = new[]
                {
                    new PointF(fWidthIn, fHeightIn)
                };
            if (bPixelsToPoints)
            {
                oGraphics.TransformPoints(CoordinateSpace.Page, CoordinateSpace.Device, array);
            }
            else
            {
                oGraphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, array);
            }
            fWidthOut = array[0].X;
            fHeightOut = array[0].Y;
            oGraphics.PageUnit = (pageUnit);
        }

        public static void DisposePen(ref Pen oPen)
        {
            if (oPen != null)
            {
                oPen.Dispose();
                oPen = null;
            }
        }

        public static void DisposeBrush(ref Brush oBrush)
        {
            if (oBrush != null)
            {
                oBrush.Dispose();
                oBrush = null;
            }
        }
    }
}