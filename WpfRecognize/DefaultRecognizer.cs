using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Ink;

namespace WpfRecognize
{
    /// <summary>
    /// 缺省识别器
    /// </summary>
    public class DefaultRecognizer : ICharactorRecognizer
    {
        /// <summary>
        /// Get 识别器名称
        /// </summary>
        public string Name
        {
            get { return "缺省识别器"; }
        }

        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="strokes">笔迹集合</param>
        /// <returns>候选词数组</returns>
        public string[] Recognize(StrokeCollection strokes)
        {
            if (strokes == null || strokes.Count == 0)
                return Constants.EmptyAlternates;

            var analyzer = new InkAnalyzer();
            analyzer.AddStrokes(strokes, Constants.ChsLanguageId);
            analyzer.SetStrokesType(strokes, StrokeType.Writing);

            var status = analyzer.Analyze();
            if (status.Successful)
            {
                return analyzer.GetAlternates()
                    .OfType<AnalysisAlternate>()
                    .Select(x => x.RecognizedString)
                    .ToArray();
            }

            analyzer.Dispose();

            return Constants.EmptyAlternates;
        }
    }
}
