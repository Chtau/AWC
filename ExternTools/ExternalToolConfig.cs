
namespace AWC.ExternTools
{
    public class ExternalToolConfig
    {
        private string myProcessName;
        private AWC.ExternTools.ExternTool.ProcessEventTyp myProcessEventTyp;
        private string myProcessStartParameter;
        private bool myEnable;

        public bool Enable
        {
            get { return myEnable; }
            set { myEnable = value; }
        }

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

        public ExternalToolConfig(string _ProcessName, AWC.ExternTools.ExternTool.ProcessEventTyp _PrcoessEventTyp, string _ProcessStartParameter, bool _Enable) : this(_ProcessName, _PrcoessEventTyp, _ProcessStartParameter)
        {
            myProcessName = _ProcessName;
            myProcessEventTyp = _PrcoessEventTyp;
            myProcessStartParameter = _ProcessStartParameter;
            myEnable = _Enable;
        }


    }
}
