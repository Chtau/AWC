﻿
namespace AWC.ExternTools
{
    public class ExternalToolConfig
    {
        private string myProcessName;
        private AWC.ExternTools.ExternTool.ProcessEventTyp myProcessEventTyp;
        private string myProcessStartParameter;
        private bool myEnable;
        private AWC.ExternTools.ExternTool.ProcessEventExecuteTyp myProcessEventExecuteTyp;

        public AWC.ExternTools.ExternTool.ProcessEventExecuteTyp ProcessEventExecuteTyp
        {
            get { return myProcessEventExecuteTyp; }
            set { myProcessEventExecuteTyp = value; }
        }

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

        public ExternalToolConfig(string _ProcessName, AWC.ExternTools.ExternTool.ProcessEventTyp _PrcoessEventTyp, string _ProcessStartParameter, bool _Enable, AWC.ExternTools.ExternTool.ProcessEventExecuteTyp _ProcessEventExecuteTyp)
        {
            myProcessName = _ProcessName;
            myProcessEventTyp = _PrcoessEventTyp;
            myProcessStartParameter = _ProcessStartParameter;
            myEnable = _Enable;
            myProcessEventExecuteTyp = _ProcessEventExecuteTyp;
        }

        public static string GetStringEventTypValue(ExternTool.ProcessEventTyp _ProcessEventTyp)
        {
            switch (_ProcessEventTyp)
            {
                case ExternTool.ProcessEventTyp.ProcessStart:
                    return "Start";
                case ExternTool.ProcessEventTyp.ProcessEnd:
                    return "End";
                case ExternTool.ProcessEventTyp.BasicData:
                    return "Basicdata";
                case ExternTool.ProcessEventTyp.WindowTitle:
                    return "Title";
                case ExternTool.ProcessEventTyp.WindowStyle:
                    return "Style";
                case ExternTool.ProcessEventTyp.PositionSize:
                    return "PositionSize";
                case ExternTool.ProcessEventTyp.WindowExStyle:
                    return "ExStyle";
                case ExternTool.ProcessEventTyp.DataText:
                    return "Datatext";
                default:
                    return "End";
            }
        }

        public static ExternTool.ProcessEventTyp GetEnumEventTypValue(string strPrcEventType)
        {
            switch (strPrcEventType)
            {
                case "Start":
                    return ExternTool.ProcessEventTyp.ProcessStart;
                case "End":
                    return ExternTool.ProcessEventTyp.ProcessEnd;
                case "Basicdata":
                    return ExternTool.ProcessEventTyp.BasicData;
                case "Title":
                    return ExternTool.ProcessEventTyp.WindowTitle;
                case "Style":
                    return ExternTool.ProcessEventTyp.WindowStyle;
                case "PositionSize":
                    return ExternTool.ProcessEventTyp.PositionSize;
                case "ExStyle":
                    return ExternTool.ProcessEventTyp.WindowExStyle;
                case "Datatext":
                    return ExternTool.ProcessEventTyp.DataText;
                default:
                    return ExternTool.ProcessEventTyp.ProcessStart;
            }
        }

        public static ExternTool.ProcessEventExecuteTyp GetEnumEventExecuteTypValue(string strPrcExeEventTyp)
        {
            switch (strPrcExeEventTyp)
            {
                case "Command":
                    return ExternTool.ProcessEventExecuteTyp.Command;
                case "Position":
                    return ExternTool.ProcessEventExecuteTyp.Position;
                case "Size":
                    return ExternTool.ProcessEventExecuteTyp.Size;
                case "Border":
                    return ExternTool.ProcessEventExecuteTyp.Border;
                default:
                    return ExternTool.ProcessEventExecuteTyp.Command;
            }
        }

        public static string GetStringEventExecuteTypValue(ExternTool.ProcessEventExecuteTyp _ProcessEventExecuteTyp)
        {
            switch (_ProcessEventExecuteTyp)
            {
                case ExternTool.ProcessEventExecuteTyp.Command:
                    return "Command";
                case ExternTool.ProcessEventExecuteTyp.Position:
                    return "Position";
                case ExternTool.ProcessEventExecuteTyp.Size:
                    return "Size";
                case ExternTool.ProcessEventExecuteTyp.Border:
                    return "Border";
                default:
                    return "Command";
            }
        }
    }
}
