using Common.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public class Communication
	{
		public EType Type { get; set; }
		public ETarget Target { get; set; }
		public dynamic Content { get; set; }
		public bool Success { get; set; }
		public Communication(EType type, ETarget target, dynamic content, bool success)
		{
			Type = type;
			Target = target;
			Content = content;
			Success = success;
		}

		public Communication()
		{
		}

		public override string ToString()
		{
			Content = Content ?? "null";
			return $"Communication = \n" +
				$"Type : {Type},\n" +
				$"Target : {Target}, \n" +
				$"Content : {(Content as object).ToString()}, \n" +
				$"Success : {Success.ToString()}";
		}
	}
}
