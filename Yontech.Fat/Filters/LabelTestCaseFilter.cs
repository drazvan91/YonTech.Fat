using System.Linq;
using Yontech.Fat.Discoverer;

namespace Yontech.Fat.Filters
{
    public class LabelTestCaseFilter : ITestCaseFilter
    {
        public string[] Labels { get; private set; }

        public LabelTestCaseFilter(params string[] labelsToExecute)
        {
            this.Labels = labelsToExecute;
        }

        public bool ShouldExecuteTestCase(FatTestCase testCase)
        {
            var labels = System.Attribute.GetCustomAttributes(testCase.Method).OfType<FatLabel>();
            if (labels.Any(label => this.Labels.Contains(label.Name)))
            {
                return true;
            }

            var classLabels = System.Attribute.GetCustomAttributes(testCase.Method.ReflectedType).OfType<FatLabel>();
            if (classLabels.Any(label => this.Labels.Contains(label.Name)))
            {
                return true;
            }

            return false;
        }
    }
}
