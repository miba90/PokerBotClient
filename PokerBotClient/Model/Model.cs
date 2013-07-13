using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tesseract;

namespace PokerBotClient.Model
{
    public class Model : IModel
    {
        public void Decide(object sender, PreFlopArgs e)
        {
            List<Word> lista;
            TesseractProcessor tp = new TesseractProcessor();
            tp.Apply(@"C:\Users\Michal\Desktop\OCR_test.bmp");
            tp.Init();
            lista = tp.RetriveResultDetail();
        }
    }
}
