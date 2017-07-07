using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Acrobat; //Add a Com Object ref. to "Adobe Acrobat 10.0 Type Library" => Program Files\Adobe\Acrobat 10.0\Acrobat\acrobat.tlb
using Microsoft.Win32;
namespace Reportig
{
	public static class Pdf2Image
	{
		const string AdobeObjectsErrorMessage = "Failed to create the PDF object.";
		const string BadFileErrorMessage = "Failed to open the PDF file.";
		const string ClipboardError = "Failed to get the image from clipboard.";
		const string SdkError = "This operation needs the Acrobat SDK(http://www.adobe.com/devnet/acrobat/downloads.html), which is combined with the full version of Adobe Acrobat.";

		public static byte[] PdfPageToPng(string pdfFilePath, int thumbWidth = 600, int thumbHeight = 750, int pageNumber = 0)
		{
			byte[] imageData = null;
			runJob((pdfDoc, pdfRect) =>
			{
				imageData = pdfPageToPng(thumbWidth, thumbHeight, pageNumber, pdfDoc, pdfRect);
			}, pdfFilePath);
			return imageData;
		}

		public static void AllPdfPagesToPng(Action<byte[], int, int> dataCallback, string pdfFilePath, int thumbWidth = 600, int thumbHeight = 750)
		{
			runJob((pdfDoc, pdfRect) =>
			{
				var numPages = pdfDoc.GetNumPages();
				for (var pageNumber = 0; pageNumber < numPages; pageNumber++)
				{
					var imageData = pdfPageToPng(thumbWidth, thumbHeight, pageNumber, pdfDoc, pdfRect);
					dataCallback(imageData, pageNumber + 1, numPages);
				}
			}, pdfFilePath);
		}

		static void runJob(Action<CAcroPDDoc, CAcroRect> job, string pdfFilePath)
		{
			if (!File.Exists(pdfFilePath))
				throw new InvalidOperationException(BadFileErrorMessage);

			var acrobatPdfDocType = Type.GetTypeFromProgID("AcroExch.PDDoc");
			if (acrobatPdfDocType == null || !isAdobeSdkInstalled)
				throw new InvalidOperationException(SdkError);

			var pdfDoc = (CAcroPDDoc)Activator.CreateInstance(acrobatPdfDocType);
			if (pdfDoc == null)
				throw new InvalidOperationException(AdobeObjectsErrorMessage);

			var acrobatPdfRectType = Type.GetTypeFromProgID("AcroExch.Rect");
			var pdfRect = (CAcroRect)Activator.CreateInstance(acrobatPdfRectType);

			var result = pdfDoc.Open(pdfFilePath);
			if (!result)
				throw new InvalidOperationException(BadFileErrorMessage);

			job(pdfDoc, pdfRect);

			releaseComObjects(pdfDoc, pdfRect);
		}

		public static byte[] ResizeImage(this Image image, int thumbWidth, int thumbHeight)
		{
			var srcWidth = image.Width;
			var srcHeight = image.Height;
			using (var bmp = new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppArgb))
			{
				using (var gr = Graphics.FromImage(bmp))
				{
					gr.SmoothingMode = SmoothingMode.HighQuality;
					gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
					gr.CompositingQuality = CompositingQuality.HighQuality;
					gr.InterpolationMode = InterpolationMode.High;

					var rectDestination = new Rectangle(0, 0, thumbWidth, thumbHeight);
					gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

					using (var memStream = new MemoryStream())
					{
						bmp.Save(memStream, ImageFormat.Png);
						return memStream.ToArray();
					}
				}
			}
		}

		static bool isAdobeSdkInstalled
		{
			get
			{
				return Registry.ClassesRoot.OpenSubKey("AcroExch.PDDoc", writable: false) != null;
			}
		}

		private static Bitmap pdfPageToBitmap(int pageNumber, CAcroPDDoc pdfDoc, CAcroRect pdfRect)
		{
			var pdfPage = (CAcroPDPage)pdfDoc.AcquirePage(pageNumber);
			if (pdfPage == null)
				throw new InvalidOperationException(BadFileErrorMessage);

			var pdfPoint = (CAcroPoint)pdfPage.GetSize();

			pdfRect.Left = 0;
			pdfRect.right = pdfPoint.x;
			pdfRect.Top = 0;
			pdfRect.bottom = pdfPoint.y;

			pdfPage.CopyToClipboard(pdfRect, 0, 0, 100);

			Bitmap pdfBitmap = null;
			var thread = new Thread(() =>
			{
				var data = Clipboard.GetDataObject();
				if (data != null && data.GetDataPresent(DataFormats.Bitmap))
					pdfBitmap = (Bitmap)data.GetData(DataFormats.Bitmap);
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();

			Marshal.ReleaseComObject(pdfPage);

			return pdfBitmap;
		}

		private static byte[] pdfPageToPng(int thumbWidth, int thumbHeight, int pageNumber, CAcroPDDoc pdfDoc, CAcroRect pdfRect)
		{
			var pdfBitmap = pdfPageToBitmap(pageNumber, pdfDoc, pdfRect);
			if (pdfBitmap == null)
				throw new InvalidOperationException(ClipboardError);

			var pdfImage = pdfBitmap.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
			// (+ 7 for template border)
			var imageData = pdfImage.ResizeImage(thumbWidth + 7, thumbHeight + 7);
			return imageData;
		}

		private static void releaseComObjects(CAcroPDDoc pdfDoc, CAcroRect pdfRect)
		{
			pdfDoc.Close();
			Marshal.ReleaseComObject(pdfRect);
			Marshal.ReleaseComObject(pdfDoc);
		}
	}
}
