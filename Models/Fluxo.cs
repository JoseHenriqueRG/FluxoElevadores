using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoElevadores.Models
{
	internal class Fluxo
	{
		public int Andar { get; set; }
		public char Elevador { get; set; }
		public char Turno { get; set; }
	}
}
