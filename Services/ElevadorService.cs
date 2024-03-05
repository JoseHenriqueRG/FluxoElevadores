using FluxoElevadores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FluxoElevadores.Services
{
	internal class ElevadorService(List<Fluxo> fluxos, int qtdeAndares) : IElevadorService
	{
		public List<int> andarMenosUtilizado()
		{
			List<int> andares = [];

			// Verificar se há algum andar que não foi utilizado.
			for (var i = 0; i < qtdeAndares; i++)
			{
				if (!fluxos.Any(f => f.Andar == i))
					andares.Add(i);
			}

			// Caso todos os andares tenham sido utilizados
			// Verifique qual ou quais foram menos utilizados.
			if (andares.Count == 0)
			{
				// Agrupa e conta.
				var fluxoGroupBy = fluxos.GroupBy(f => f.Andar)
											.Select(f => new { f.Key, Total = f.Count() })
											.ToList();

				// Obtém o menor número de utilização.
				int menos = fluxoGroupBy.Select(f => f.Total).Min();

				// Adiciona os andares que têm o menor valor de utilização.
				andares.AddRange([.. fluxoGroupBy.Where(f => f.Total == menos).Select(f => f.Key).Order()]);
			}

			return andares;
		}

		public List<char> elevadorMaisFrequentado()
		{
			// Agrupa e conta
			var fluxoGroupBy =	fluxos.GroupBy(f => f.Elevador)
										.Select(f => new { f.Key, Total = f.Count() })
										.ToList();

			// Seleciona o maior número de utilização
			int mais = fluxoGroupBy.Select(f => f.Total).Max();

			// Retorna todos elevadores que foram utilizados mais vezes
			return fluxoGroupBy.Where(f => f.Total == mais).Select(f => f.Key).ToList();
		}

		public List<char> elevadorMenosFrequentado()
		{
			var fluxoGroupBy = fluxos.GroupBy(f => f.Elevador)
							.Select(f => new { f.Key, Total = f.Count() })
							.ToList();

			int menos = fluxoGroupBy.Select(f => f.Total).Min();

			return fluxoGroupBy.Where(f => f.Total == menos).Select(f => f.Key).ToList();
		}

		public float percentualDeUsoElevadorA()
		{
			var fluxoElevadorA = fluxos.Where(f => f.Elevador == 'A').ToList();

			return ((float)fluxoElevadorA.Count / (float)fluxos.Count) * 100;
		}

		public float percentualDeUsoElevadorB()
		{
			var fluxoElevadorB = fluxos.Where(f => f.Elevador == 'B').ToList();

			return ((float)fluxoElevadorB.Count / (float)fluxos.Count) * 100;
		}

		public float percentualDeUsoElevadorC()
		{
			var fluxoElevadorC = fluxos.Where(f => f.Elevador == 'C').ToList();

			return ((float)fluxoElevadorC.Count / (float)fluxos.Count) * 100;
		}

		public float percentualDeUsoElevadorD()
		{
			var fluxoElevadorD = fluxos.Where(f => f.Elevador == 'D').ToList();

			return ((float)fluxoElevadorD.Count / (float)fluxos.Count) * 100; 
		}

		public float percentualDeUsoElevadorE()
		{
			var fluxoElevadorE = fluxos.Where(f => f.Elevador == 'E').ToList();

			return ((float)fluxoElevadorE.Count / (float)fluxos.Count) * 100;
		}

		/// Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). 
		public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
		{
			var elevadores = elevadorMaisFrequentado();

			var fluxoGroupBy = fluxos.Where(f => elevadores.Contains(f.Elevador))
										.GroupBy(f => f.Turno)
										.Select(f => new { f.Key, Total = f.Count() })
										.ToList();

			int mais = fluxoGroupBy.Select(f => f.Total).Max();

			return fluxoGroupBy.Where(f => f.Total == mais).Select(f => f.Key).ToList();
		}

		/// Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). 
		public List<char> periodoMenorFluxoElevadorMenosFrequentado()
		{
			var elevadores = elevadorMenosFrequentado();

			var fluxoGroupBy = fluxos.Where(f => elevadores.Contains(f.Elevador))
										.GroupBy(f => f.Turno)
										.Select(f => new { f.Key, Total = f.Count() })
										.ToList();
			
			// Obtém o menor número de utilização.
			int menos = fluxoGroupBy.Select(f => f.Total).Min();

			return fluxoGroupBy.Where(f => f.Total == menos).Select(f => f.Key).ToList();
		}

		/// Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. 
		public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
		{
			var fluxoGroupBy = fluxos.GroupBy(f => f.Turno)
									.Select(f => new { f.Key, Total = f.Count() })
									.ToList();

			var mais = fluxoGroupBy.Select(f => f.Total).Max();

			return fluxoGroupBy.Where(f => f.Total == mais).Select(f => f.Key).ToList();
		}
	}
}
