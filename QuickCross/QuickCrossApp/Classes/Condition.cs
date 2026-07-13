using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qc4Launcher.Classes
{
	internal class Condition
	{
		public int Id { get; set; }
		public string Variable { get; set; }
		public string Operator;
		public List<int> Values;
		public string AndOr;

		internal Condition(int id,string var, string op, List<int> list,string ao)
		{
			Id = id;
			Variable = var;
			Operator = op;
			Values = list;
			AndOr = ao;
		}
	}
}
