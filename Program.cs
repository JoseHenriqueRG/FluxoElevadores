using FluxoElevadores.Models;
using FluxoElevadores.Services;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

int qtdeAndares = 16;
List<Fluxo> fluxos = [];

using (StreamReader r = new("../../../input.json"))
{
	var json = r.ReadToEnd();
	if(!string.IsNullOrWhiteSpace(json))
		fluxos = JsonConvert.DeserializeObject<List<Fluxo>>(json);	
}

var service = new ElevadorService(fluxos, qtdeAndares);

var andares = service.andarMenosUtilizado();
Console.WriteLine("a. Qual é o andar menos utilizado pelos usuários: " + string.Join(", ", andares));

var elevadoresMaisFrequentados = service.elevadorMaisFrequentado();
var periodoMaiorFluxo = service.periodoMaiorFluxoElevadorMaisFrequentado();
Console.WriteLine("b. Qual é o elevador mais frequentado e o período que se encontra maior fluxo: Elevadores " + string.Join(", ", elevadoresMaisFrequentados) + " e períodos " + string.Join(", ", periodoMaiorFluxo));

var elevadoresMenosFrequentado = service.elevadorMenosFrequentado();
var periodoMenorFluxo = service.periodoMenorFluxoElevadorMenosFrequentado();
Console.WriteLine("c. Qual é o elevador menos frequentado e o período que se encontra menor fluxo: Elevadores " + string.Join(", ", elevadoresMenosFrequentado) + " e períodos " + string.Join(", ", periodoMenorFluxo));

var conjuntoPeriodo = service.periodoMaiorUtilizacaoConjuntoElevadores();
Console.WriteLine("d. Qual o período de maior utilização do conjunto de elevadores: " + string.Join(", ", conjuntoPeriodo));

var elevadorA = service.percentualDeUsoElevadorA();
var elevadorB = service.percentualDeUsoElevadorB();
var elevadorC = service.percentualDeUsoElevadorC();
var elevadorD = service.percentualDeUsoElevadorD();
var elevadorE = service.percentualDeUsoElevadorE();
Console.WriteLine($"e. Qual o percentual de uso de cada elevador com relação a todos os serviços prestados: " +
	$"\r\nElevador A: {elevadorA:0.##\\%}" +
	$"\r\nElevador B: {elevadorB:0.##\\%}" +
	$"\r\nElevador C: {elevadorC:0.##\\%}" +
	$"\r\nElevador D: {elevadorD:0.##\\%}" +
	$"\r\nElevador E: {elevadorE:0.##\\%}");



