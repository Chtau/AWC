
namespace AWC.ExternTools
{
    public class ExternalToolConfig
    {
        private string myProcessName;
        private AWC.ExternTools.ExternTool.ProcessEventTyp myProcessEventTyp;
        private string myProcessStartParameter;

        public string ProcessName
        {
            get { return myProcessName; }
            set { myProcessName = value; }
        }

        public string ProcessStartParameter
        {
            get { return myProcessStartParameter; }
            set { myProcessStartParameter = value; }
        }

        public AWC.ExternTools.ExternTool.ProcessEventTyp ProcessEventTyp
        {
            get { return myProcessEventTyp; }
            set { myProcessEventTyp = value; }
        }

        public ExternalToolConfig(string _ProcessName, AWC.ExternTools.ExternTool.ProcessEventTyp _PrcoessEventTyp, string _ProcessStartParameter)
        {
            myProcessName = _ProcessName;
            myProcessEventTyp = _PrcoessEventTyp;
            myProcessStartParameter = _ProcessStartParameter;
        }


    }
}
